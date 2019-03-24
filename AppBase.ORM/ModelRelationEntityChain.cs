namespace AppBase.ORM
{
    public class ModelRelationEntityChain
    {
        /// <summary>
        /// Get or set entity relation
        /// </summary>
        public ModelRelation Relation { get; set; }

        /// <summary>
        /// Get or set entity relation END 1
        /// </summary>
        public ModelEntity End1 { get; set; }

        /// <summary>
        /// Get or set entity relation PARENT
        /// </summary>
        public ModelEntity Parent { get; set; }

        /// <summary>
        /// Get or set entity relation END 2
        /// </summary>
        public ModelEntity End2 { get; set; }
    }
}
