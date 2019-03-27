using System;
using System.Collections.Generic;

namespace AppBase.ORM
{
    public class BaseEntityCollection<T> : List<T>, IFlattingObject<BaseEntity> where T : BaseEntity
    {
        /// <summary>
        /// Event triggered on collection flattening
        /// </summary>
        internal event Action<T> CollectionEntityFlatten;

        /// <summary>
        /// Flatten object
        /// </summary>
        /// <returns>A collection of object</returns>
        public HashSet<BaseEntity> Flatten()
        {
            var bag = new HashSet<BaseEntity>();
            Flatten(ref bag);
            return bag;
        }

        /// <summary>
        /// Flatten object
        /// </summary>
        /// <param name="bag">A reference to a collection where all objects will be collected</param>
        public void Flatten(ref HashSet<BaseEntity> bag)
        {
            foreach (var entity in this)
            {
                CollectionEntityFlatten?.Invoke(entity);
                entity.Flatten(ref bag);
            }
        }
    }
}
