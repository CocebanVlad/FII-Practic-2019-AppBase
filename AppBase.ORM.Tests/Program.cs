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
            using (var conn = new SqlConnection(CommonHelpers.GetConnectionString()))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM [dbo].[UserInRoles] WHERE 1 = 1;
                        DELETE FROM [dbo].[Rights] WHERE 1 = 1;
                        DELETE FROM [dbo].[Roles] WHERE 1 = 1;
                        DELETE FROM [dbo].[Users] WHERE 1 = 1;
                        DELETE FROM [dbo].[Functions] WHERE 1 = 1;
                        ";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Everything deleted");
                }

                var roleNames = new string[] { "Admin", "User" };
                var roles = new List<Role>();
                foreach (var roleName in roleNames)
                {
                    var role = new Role();
                    role.RoleName = roleName;
                    for (var i = 0; i < 10; i++)
                    {
                        var function = new Function();
                        function.FunctionName = roleName + "Function" + (i + 1);
                        var right = new Right();
                        right.Function = function;
                        role.Rights.Add(right);
                    }
                    Console.WriteLine("Creating role \"" + roleName + "\"");
                    var repo = role.CreateRepository(conn);
                    repo.InsertOrUpdate(role);
                    roles.Add(role);
                }
            }
        }
    }
}
