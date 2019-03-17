using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBase.ORM
{
    public class Field
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("relation")]
        public string Relation { get; set; }

        [JsonProperty("columnName")]
        public string ColumnName { get; set; }

        [JsonProperty("columnType")]
        public string ColumnType { get; set; }

        [JsonProperty("isNullable")]
        public bool? IsNullable { get; set; }

        [JsonProperty("isKey")]
        public bool? IsKey { get; set; }

        [JsonProperty("columnSize")]
        public int? ColumnSize { get; set; }
    }

    public class RelationField
    {
        [JsonProperty("parentColumnName")]
        public string ParentColumnName { get; set; }

        [JsonProperty("childColumnName")]
        public string ChildColumnName { get; set; }
    }

    public class Relation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tableName")]
        public string TableName { get; set; }

        [JsonProperty("fields")]
        public IList<RelationField> Fields { get; set; }
    }

    public class Entity
    {
        [JsonProperty("tableName")]
        public string TableName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fields")]
        public IList<Field> Fields { get; set; }

        [JsonProperty("key")]
        public IList<string> Key { get; set; }

        [JsonProperty("relations")]
        public IList<Relation> Relations { get; set; }
    }

    public class Model
    {
        [JsonProperty("entities")]
        public IList<Entity> Entities { get; set; }
    }
}
