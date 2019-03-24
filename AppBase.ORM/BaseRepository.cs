using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppBase.ORM
{
    public class BaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get DB connection
        /// </summary>
        protected SqlConnection Connection { get; }

        public BaseRepository(SqlConnection conn)
        {
            Connection = conn;
        }

        /// <summary>
        /// Insert or update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void InsertOrUpdate(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select one entity by key
        /// </summary>
        /// <param name="key">Key (a dictionary containing key column name and its value)</param>
        /// <returns>An entity</returns>
        public virtual T SelectOne(Dictionary<string, string> key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select all entities
        /// </summary>
        /// <returns>A collection of entities</returns>
        public virtual List<T> SelectAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select all filtered entities
        /// </summary>
        /// <param name="filter">Filter (a dictionary containing key column name and its value)</param>
        /// <returns>A collection of entities</returns>
        public virtual List<T> SelectAll(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }
    }
}
