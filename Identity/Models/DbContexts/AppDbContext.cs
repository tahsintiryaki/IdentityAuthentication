using Identity.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.DbContexts
{

    //“IdentityDbContext” sınıfına generic olarak user modelinde “AppUser” sınıfının, role modelinde ise “AppRole” sınıfının kullanılacağını belirtmiş oluyoruz. 3. parametrede ise bu yapılanmanın primary key (Id) kolonlarının “string” tipte değerlerle tutulacağını ifade etmiş oluyoruz.
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContext): base(dbContext)
        {

        }
    }
}
