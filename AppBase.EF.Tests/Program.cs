using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBase.EF.Tests
{
    public class Program
    {
       static void Main(string[] args)
        {
            using (var ctx = new AppBaseEntities())
            {
                //for (var i = 0; i < 10; i++)
                //{
                //    ctx.Users.Add(new User()
                //    {
                //        UserName = "user" + i,
                //        Email = "user" + i + "@mail.com",
                //        FirstName = "John" + i,
                //        LastName = "Doe" + i,
                //        BirthDate = DateTime.Now
                //    });
                //}

                //ctx.SaveChanges();


                var query = ctx.Users.AsNoTracking().Select(x => new { Username = x.UserName });
                var users = query.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine(user.Username);
                }
            }

            Console.ReadKey();
        }
    }
}
