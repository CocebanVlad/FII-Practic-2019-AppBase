using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppBase.ORM
{
    public class Entity
    {
        /// <summary>
        /// Get or set table name
        /// </summary>
        [JsonProperty("tableName")]
        public string TableName { get; set; }

        /// <summary>
        /// Get or set name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set fields
        /// </summary>
        [JsonProperty("fields")]
        public IList<ModelField> Fields { get; set; }

        /// <summary>
        /// Get or set relations
        /// </summary>
        [JsonProperty("relations")]
        public IList<ModelRelation> Relations { get; set; }
    }
}
