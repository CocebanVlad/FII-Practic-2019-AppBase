using AppBase.ORM.Entities;
using System.Linq;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;

namespace AppBase.ORM.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
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
                repo.Delete(user);
                repo.InsertOrUpdate(user);
            }

            Console.ReadKey();
        }
    }
}
