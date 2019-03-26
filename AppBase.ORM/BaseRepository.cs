using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AppBase.ORM
{
    public class BaseRepository
    {
        /// <summary>
        /// Get DB connection
        /// </summary>
        protected SqlConnection Connection { get; }

        public BaseRepository(SqlConnection conn)
        {
            if (conn == null)
                throw new ArgumentNullException("conn");

            Connection = conn;
        }

        /// <summary>
        /// Insert or update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="skipNestedObjects">
        ///     Flag specifying whether to skip or not InsertOrUpdate for nested objects (optional)
        /// </param>
        public virtual void InsertOrUpdate(BaseEntity entity, bool skipNestedObjects = false)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            using (var tr = Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                try
                {
                    InsertOrUpdate(entity, tr, skipNestedObjects);
                    tr.Commit();
                }
                catch
                {
                    tr.Rollback();
                    throw;
                }
        }

        /// <summary>
        /// Insert or update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="tr">Transaction</param>
        /// <param name="skipNestedObjects">
        ///     Flag specifying whether to skip or not InsertOrUpdate for nested objects (optional)
        /// </param>
        public virtual void InsertOrUpdate(BaseEntity entity, SqlTransaction tr, bool skipNestedObjects = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="skipNestedObjects">
        ///     Flag specifying whether to skip or not Delete for nested objects (optional)
        /// </param>
        public virtual void Delete(BaseEntity entity, bool skipNestedObjects = false)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            using (var tr = Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                try
                {
                    Delete(entity, tr, skipNestedObjects);
                    tr.Commit();
                }
                catch
                {
                    tr.Rollback();
                    throw;
                }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="tr">Transaction</param>
        /// <param name="skipNestedObjects">
        ///     Flag specifying whether to skip or not Delete for nested objects (optional)
        /// </param>
        public virtual void Delete(BaseEntity entity, SqlTransaction tr, bool skipNestedObjects = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select one entity by key
        /// </summary>
        /// <param name="key">Key (a dictionary containing key column name and its value)</param>
        /// <returns>An entity</returns>
        public virtual BaseEntity SelectOne(Dictionary<string, string> key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select all entities
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <returns>A collection of entities</returns>
        public virtual BaseEntityCollection<BaseEntity> SelectAll(int amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select all filtered entities
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <param name="filter">Filter (a dictionary containing key column name and its value)</param>
        /// <returns>A collection of entities</returns>
        public virtual BaseEntityCollection<BaseEntity> SelectAll(int amount, Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }
    }
}
