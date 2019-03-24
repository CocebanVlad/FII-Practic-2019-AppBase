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
        public IList<ModelEntity> Entities { get; set; }

        /// <summary>
        /// Get entity type
        /// </summary>
        /// <param name="relation">Relation name</param>
        /// <param name="end1">Entity relation END 1</param>
        /// <returns>Type</returns>
        public string GetEntityType(string relation, ModelEntity end1)
        {
            var chain = GetRelationEntityChain(
                relation,
                end1
                );
            return (chain.End1 == chain.End2 || chain.Parent == chain.End2 ||
                (end1 != chain.Parent && chain.Parent != chain.End2 && chain.End2 != chain.End1))
                    ? "List<" + chain.End2.Name + ">"
                    : chain.End2.Name;
        }

        /// <summary>
        /// Get relation entity chain
        /// </summary>
        /// <param name="relation">Relation name</param>
        /// <param name="end1">Entity relation END 1</param>
        /// <returns>Entity chain</returns>
        public ModelRelationEntityChain GetRelationEntityChain(string relation, ModelEntity end1)
        {
            var chain = new ModelRelationEntityChain();
            chain.End1 = end1;

            #region Seek chain
            var path = relation
                .Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (path.Length != 2)
                throw new Exception("Malformed relation");
            chain.Parent = Entities.FirstOrDefault(
                x => x.Name.Equals(path[0], StringComparison.InvariantCultureIgnoreCase));
            if (chain.Parent == null)
                throw new Exception("Unknown entity \"" + path[0] + "\"");
            chain.Relation = chain.Parent.Relations.FirstOrDefault(
                x => x.Name.Equals(path[1], StringComparison.InvariantCultureIgnoreCase));
            if (chain.Relation == null)
                throw new Exception("Relation \"" + path[1] + "\" cannot be found on \"" + path[0] + "\"");
            chain.End2 = Entities.FirstOrDefault(
                x => x.Name.Equals(chain.Relation.EntityName, StringComparison.InvariantCultureIgnoreCase));
            if (chain.End2 == null)
                throw new Exception("Referenced entity \"" + chain.Relation.EntityName +
                    "\" cannot be found by \"" + chain.Relation.Name + "\"");
            if (chain.End2.Equals(chain.End1))
                chain.End2 =
                    chain.Parent;
            #endregion

            return chain;
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
