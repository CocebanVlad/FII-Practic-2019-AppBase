using System;
using System.Data.Common;
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
                // Working with Transactions
                // https://docs.microsoft.com/en-us/ef/ef6/saving/transactions

                try
                {
                    // Ensure we have "ADMIN" role created
                    var adminRoleName = "ADMIN";
                    var adminRole = ctx.Roles
                        .FirstOrDefault(role => role.Name == adminRoleName);
                    if (adminRole == null)
                    {
                        adminRole = new Role() { Name = adminRoleName };
                        ctx.Roles.Add(adminRole);

                        Console.WriteLine("Creating role \"" + adminRoleName + "\"");
                    }

                    // Ensure we have "USER" role created
                    var userRoleName = "USER";
                    var userRole = ctx.Roles
                        .FirstOrDefault(role => role.Name == userRoleName);
                    if (userRole == null)
                    {
                        userRole = new Role() { Name = userRoleName };
                        ctx.Roles.Add(userRole);

                        Console.WriteLine("Creating role \"" + userRoleName + "\"");
                    }

                    // Generate 1000 users
                    for (var i = 0; i < 1000; i++)
                    {
                        var userName = Guid.NewGuid().ToString("N");
                        // Guid.ToString Method
                        // https://docs.microsoft.com/en-us/dotnet/api/system.guid.tostring?view=netframework-4.7.2

                        var user = new User();
                        user.UserName = userName;
                        user.Email = userName + "@email.com";
                        user.FirstName = "";
                        user.LastName = "";
                        user.BirthDate = DateTime.Now;

                        // @if i mod 3 equals 0 then make the user an administrator,
                        //    otherwise an user;
                        user.Roles.Add(
                            i % 3 == 0
                                ? adminRole
                                : userRole
                            );

                        ctx.Users.Add(user);

                        Console.WriteLine("Creating user \"" + user.UserName + "\"");
                    }

                    Console.WriteLine("Saving...");

                    ctx.SaveChanges();

                    tr.Commit();

                    Console.WriteLine("Getting TOP 100 users that have \"" +
                        adminRoleName + "\" role");
                    var adminUsersQuery = (DbQuery<User>)ctx.Users
                        .AsNoTracking()
                        .Where(user => user.Roles.Any(x => x.Id == adminRole.Id))
                        .OrderBy(user => user.Id)
                        .Skip(0)
                        .Take(100);

                    var adminUsers = adminUsersQuery.ToList();

                    Console.WriteLine("Generated SQL is:\n" +
                        adminUsersQuery.Sql + "\n\n");

                    foreach (var user in adminUsers)
                        Console.WriteLine("Id: {0}, UserName: {1}, Email: {2}",
                            user.Id, user.UserName, user.Email);
                }
                catch (Exception)
                {
                    tr.Rollback();
                    throw;
                }

                using (var cmd = ctx.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM [UserInRoles] WHERE 1 = 1;
                            DELETE FROM [Users] WHERE 1 = 1;
                            ";
                    cmd.ExecuteNonQuery();
                }
            }

            Console.ReadKey();
        }
    }
}
