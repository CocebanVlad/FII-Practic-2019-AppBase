using System.Collections.Generic;

namespace AppBase.ORM
{
    public class BaseEntityCollection<T> : List<T> where T : BaseEntity
    {
    }
}
