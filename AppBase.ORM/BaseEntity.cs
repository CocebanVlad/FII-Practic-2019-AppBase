using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppBase.ORM
{
    public class BaseEntity
    {
        /// <summary>
        /// Flatten entity
        /// </summary>
        /// <returns>Flatten set of all nested entities</returns>
        internal HashSet<BaseEntity> Flatten()
        {
            var bag = new HashSet<BaseEntity>();
            Flatten(ref bag);
            return bag;
        }

        /// <summary>
        /// Flatten entity
        /// </summary>
        /// <param name="bag">A reference to a HashSet where all entities will be collected</param>
        private void Flatten(ref HashSet<BaseEntity> bag)
        {
            bag = bag ?? new HashSet<BaseEntity>();
            if (!bag.Add(this))
                return;

            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType.IsGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(BaseEntityCollection<>))
                {
                    var coll = prop.GetValue(this) as IList;
                    if (coll != null)
                        foreach (BaseEntity item in coll)
                            item.Flatten(ref bag);
                }
                else if (typeof(BaseEntity).IsAssignableFrom(prop.PropertyType))
                {
                    var val = prop.GetValue(this) as BaseEntity;
                    if (val != null)
                        val.Flatten(ref bag);
                }
            }
        }

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="conn">DB connection</param>
        /// <returns>Repository</returns>
        public virtual BaseRepository CreateRepository(SqlConnection conn)
        {
            throw new NotImplementedException();
        }
    }
}
