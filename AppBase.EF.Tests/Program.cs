using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AppBase.EF.Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new AppBaseEntities())
            using (var tr = ctx.Database.BeginTransaction())
            {
                using (var cmd = ctx.Database.Connection.CreateCommand())
                {
                    cmd.Transaction = tr.UnderlyingTransaction;
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

                try
                {
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
                        roles.Add(ctx.Roles.Add(role));
                    }

                    ctx.SaveChanges();

                    for (var i = 0; i < 100; i++)
                    {
                        var user = new User();
                        user.UserName = "User" + (i + 1);
                        user.Email = user.UserName + "@email.com";
                        user.FirstName = "";
                        user.LastName = "";
                        user.BirthDate = DateTime.Now;
                        user.Roles.Add(roles[i % roles.Count]);

                        Console.WriteLine("Creating user \"" + user.UserName + "\"");
                        ctx.Users.Add(user);
                    }

                    Console.WriteLine("Saving...");
                    ctx.SaveChanges();
                    tr.Commit();

                    Console.WriteLine("Getting TOP 100 users");
                    var usersQuery = (DbQuery<User>)ctx.Users
                        .AsNoTracking()
                        .OrderBy(user => user.UserName)
                        .Skip(0)
                        .Take(100);

                    var users = usersQuery.ToList();

                    Console.WriteLine("Generated SQL is:\n" +
                        usersQuery.Sql + "\n\n");

                    foreach (var user in usersQuery)
                        Console.WriteLine("UserName: {0}, Email: {1}",
                            user.UserName, user.Email);
                }
                catch (Exception)
                {
                    tr.Rollback();
                    throw;
                }
            }

            Console.ReadKey();
        }
    }
}
