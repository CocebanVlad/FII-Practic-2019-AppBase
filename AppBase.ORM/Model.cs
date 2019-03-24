using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppBase.ORM
{
    public class Model
    {
        /// <summary>
        /// Get or set entities
        /// </summary>
        [JsonProperty("entities")]
        public IList<Entity> Entities { get; set; }

        /// <summary>
        /// Get entity type
        /// </summary>
        /// <param name="relation">Relation</param>
        /// <param name="end1">Entity END 1</param>
        /// <returns>Type</returns>
        public string GetEntityType(string relation, Entity end1)
        {
            var path = relation
                .Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (path.Length != 2)
                throw new Exception("Malformed relation");
            var parent = Entities.FirstOrDefault(
                x => x.Name.Equals(path[0], StringComparison.InvariantCultureIgnoreCase));
            if (parent == null)
                throw new Exception("Unknown entity \"" + path[0] + "\"");
            var rel = parent.Relations.FirstOrDefault(
                x => x.Name.Equals(path[1], StringComparison.InvariantCultureIgnoreCase));
            if (rel == null)
                throw new Exception("Relation \"" + path[1] + "\" cannot be found on \"" + path[0] + "\"");
            var end2 = Entities.FirstOrDefault(
                x => x.Name.Equals(rel.EntityName, StringComparison.InvariantCultureIgnoreCase));
            if (end2 == null)
                throw new Exception("Referenced entity \"" + rel.EntityName +
                    "\" cannot be found by \"" + rel.Name + "\"");

            return (end1 == end2 || parent == end2 ||
                (end1 != parent && parent != end2 && end2 != end1))
                    ? "List<" + end2.Name + ">"
                    : end2.Name;
        }

        /// <summary>
        /// Serialize object
        /// </summary>
        /// <returns>A JSON</returns>
        public string SerializeObject()
        {
            return JsonConvert.SerializeObject(this,
                Formatting.Indented,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
                );
        }

        /// <summary>
        /// Deserialize object
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <returns>Model</returns>
        public static Model DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject<Model>(json);
        }

        /// <summary>
        /// Save JSON model to file
        /// </summary>
        /// <param name="path">Path</param>
        public void Save(string path)
        {
            using (var file = new StreamWriter(path, false, new UTF8Encoding(false)))
                file.Write(SerializeObject());
        }

        /// <summary>
        /// Load model from a JSON file
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Model</returns>
        public static Model Load(string path)
        {
            using (var file = new StreamReader(path, new UTF8Encoding(false)))
                return DeserializeObject(file.ReadToEnd());
        }
    }
}
