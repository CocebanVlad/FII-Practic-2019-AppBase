using AppBase.ORM.Entities;
using System.Linq;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AppBase.ORM.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //var model = Model.Load("Model.json");
            //ModelManager.Complete(model);
            //foreach (var entity in model.Entities)
            //    foreach (var field in entity.Fields.Where(x => !string.IsNullOrEmpty(x.Relation)))
            //    {
            //        var chain = model
            //            .GetRelationEntityChain(field.Relation, entity);
            //        Console.WriteLine("ENTITY       {0}", entity.Name);
            //        Console.WriteLine("RELATION     {0}", chain.Relation.Name);
            //        Console.WriteLine("TYPE         {0}", chain.RelationType.ToString());
            //        Console.WriteLine("END 1        {0}", chain.End1.Name);
            //        Console.WriteLine("PARENT       {0}", chain.Parent.Name);
            //        Console.WriteLine("END 2        {0}", chain.End2.Name);
            //        Console.WriteLine("");
            //    }
            var user = new User();
            user.UserName = "coceban.vlad";
            user.Email = "coceban.vlad@hotmail.com";
            var role = new Role();
            role.RoleName = "Admin";

            for (var i = 0; i < 10; i++)
            {
                var function = new Function();
                function.FunctionName = "Function" + i;

                var right = new Right();
                right.Function = function;
                right.FunctionName = function.FunctionName;
                right.IsEnabled = true;
                right.Role = role;
                right.RoleName = role.RoleName;
                role.Rights.Add(right);
            }

            user.Roles.Add(role);
            using (var conn = new SqlConnection(CommonHelpers.GetConnectionString()))
            {
                conn.Open();
                var repo = (UserRepository)user.CreateRepository(conn);
                repo.InsertOrUpdate(user);

                //user.FirstName = "Vlad";
                //user.LastName = "Coceban";
                //user.BirthDate = DateTime.Now;
                //repo.InsertOrUpdate(user);

                //repo.Delete(user);
            }
        }
    }
}
