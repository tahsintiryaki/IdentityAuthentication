using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Models.Claims
{
    public static class ClaimData
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            //User Claims
            new Claim("EditProfile","Profil Düzenle"),
            new Claim("SignIn","Üye Ol"),
            new Claim("PasswordReset","Şifremi Unuttum"),
           
        };
    }
}
