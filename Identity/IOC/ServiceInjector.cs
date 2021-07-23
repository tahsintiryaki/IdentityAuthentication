using Identity.CustomValidations;
using Identity.Models.Authentication;
using Identity.Models.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

                _.User.RequireUniqueEmail = true; //Email adreslerini tekilleştiriyoruz.
                _.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+"; //

            }).AddPasswordValidator<CustomPasswordValidation>()  //AddPasswordValidator<CustomPasswordValidation> ile özelleştirilmiş şifre validasyonu gerçekleştirilmiştir.
            .AddUserValidator<CustomUserValidation>()//AddUserValidator<CustomUserValidation> ile özelleştirilmiş username validasyonu gerçekleştirilmiştir.
            .AddErrorDescriber<CustomIdentityErrorDescriber>()//AddErrorDescriber<CustomIdentityErrorDescriber> ile hata mesajları özelleştirilmiştir.
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();//Şifremi unuttum işlemi için eklenmesi gerekmektedir.




            services.ConfigureApplicationCookie(_ =>
            {
                _.LoginPath = new PathString("/User/Login");
                _.Cookie = new CookieBuilder
                {
                    Name = "AspNetCoreIdentityExampleCookie", //Oluşturulacak Cookie'yi isimlendiriyoruz.
                    HttpOnly = false, //Kötü niyetli insanların client-side tarafından Cookie'ye erişmesini engelliyoruz.
                  //Expiration property'si yerine ExpireTimeSpan propertysi kullanılmalı yoksa uygulama hata veriyor 
                    SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin gönderilmemesini belirtiyoruz.
                    SecurePolicy = CookieSecurePolicy.Always //HTTPS üzerinden erişilebilir yapıyoruz.
                };
                _.SlidingExpiration = true; //Expiration süresinin yarısı kadar süre zarfında istekte bulunulursa eğer geri kalan yarısını tekrar sıfırlayarak ilk ayarlanan süreyi tazeleyecektir.
                _.ExpireTimeSpan = TimeSpan.FromMinutes(2); //CookieBuilder nesnesinde tanımlanan Expiration değerinin varsayılan değerlerle ezilme ihtimaline karşın tekrardan Cookie vadesi burada da belirtiliyor.
                _.AccessDeniedPath = new PathString("/User/Error");
            });
        }
    }
}
