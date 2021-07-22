using Identity.CustomValidations;
using Identity.Models.Authentication;
using Identity.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.IOC
{
    public class ServiceInjector
    {
        public static void Add(IServiceCollection services, IConfiguration configurtion)
        {

          

            //Indentity
            services.AddIdentity<AppUser, AppRole>(_ =>
            {
                _.Password.RequiredLength = 5; //En az kaç karakterli olması gerektiğini belirtiyoruz.
                _.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunluluğunu kaldırıyoruz.
                _.Password.RequireLowercase = false; //Küçük harf zorunluluğunu kaldırıyoruz.
                _.Password.RequireUppercase = false; //Büyük harf zorunluluğunu kaldırıyoruz.
                _.Password.RequireDigit = false; //0-9 arası sayısal karakter zorunluluğunu kaldırıyoruz.

                //AddPasswordValidator<CustomPasswordValidation> ile özelleştirilmiş şifre validasyonu gerçekleştirilmiştir.
            }).AddPasswordValidator<CustomPasswordValidation>().AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
