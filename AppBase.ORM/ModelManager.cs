using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AppBase.ORM
{
    public class ModelManager
    {
        /// <summary>
        /// Complete model with missing data
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="connStr">Connection string</param>
        public static void Complete(Model model, string connStr = null)
        {
            if (string.IsNullOrEmpty(connStr))
                connStr = CommonHelpers.GetConnectionString();

            using (var conn = new SqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"
                    SELECT sch.[name] AS [SchemaName], 
                           tbl.[name] AS [TableName], 
	                       col.[name] AS [ColumnName],
	                       typ.[name] AS [ColumnType],
	                       col.[is_nullable] AS [IsNullable],
	                       idx.[is_primary_key] AS [IsKey],
	                       col.[max_length] AS [Size]
                    FROM [sys].[all_columns] AS col
                    LEFT JOIN [sys].[tables] AS tbl ON tbl.[object_id] = col.[object_id]
                    LEFT JOIN [sys].[schemas] AS sch ON sch.[schema_id] = tbl.[schema_id]
                    LEFT JOIN [sys].[types] AS typ ON typ.[user_type_id] = col.[user_type_id]
                    LEFT JOIN [sys].[index_columns] AS icl ON icl.[object_id] = col.[object_id] AND icl.[column_id] = col.[column_id]
                    LEFT JOIN [sys].[indexes] AS idx ON idx.[object_id] = col.[object_id] AND idx.[index_id] = icl.[index_id]
                    WHERE sch.[name] = 'dbo'
                    ";

                var tbl = new DataTable();
                using (var reader = cmd.ExecuteReader())
                    tbl.Load(reader);

                foreach (var entity in model.Entities)
                {
                    var rows = tbl.Select("[TableName] = '" + entity.TableName + "'");
                    foreach (var row in rows)
                    {
                        if (entity.Fields == null)
                            entity.Fields = new List<ModelField>();

                        var f = entity.Fields
                            .FirstOrDefault(x =>
                                string.Equals((string)row["ColumnName"], x.ColumnName,
                                    StringComparison.InvariantCultureIgnoreCase));
                        if (f == null)
                        {
                            f = new ModelField();
                            entity.Fields
                                .Add(f);
                        }

                        f.ColumnName =
                            (string)row["ColumnName"];

                        if (string.IsNullOrEmpty(f.FieldName))
                            f.FieldName = (string)row["ColumnName"];
                        if (!string.IsNullOrEmpty(f.Relation))
                            continue;

                        f.ColumnType = (string)row["ColumnType"];
                        f.IsNullable = !row.IsNull("IsNullable") ? (bool?)row["IsNullable"] : null;
                        f.IsKey = !row.IsNull("IsKey") ? (bool?)row["IsKey"] : null;
                        f.ColumnSize = !row.IsNull("Size") ? (short?)row["Size"] : null;
                    }
                }
            }
        }
    }
}
