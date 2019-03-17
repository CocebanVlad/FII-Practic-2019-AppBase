using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBase.ORM
{
    public class ModelManager
    {
        public static Model Load()
        {
            return JsonConvert.DeserializeObject<Model>(File.ReadAllText("model.json"));
        }
    }
}
