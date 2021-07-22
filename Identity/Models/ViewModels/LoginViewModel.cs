using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen e-posta adresini boş geçmeyiniz.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen uygun formatta e-posta adresi giriniz.")]
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen uygun formatta şifre giriniz.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }


        /// <summary>
        /// Beni hatırla...
        /// </summary>
        [Display(Name = "Beni Hatırla")]
        public bool Persistent { get; set; }
        public bool Lock { get; set; }

        //“Persistent” ve “Lock” isminde iki adet bool tipinde propertyde barındırmaktadır. Bu dikkat çekici model elemanlarının sırasıyla ne olduklarına değinirsek eğer; “Persistent”, cookie konfigürasyonlarında Expiration değeri olarak verilen vade süresinin oluşturulacak cookie için geçerli/aktif olup olmamasını tutmakta, “Lock” özelliği ise kullanıcının belirli sayıda yapmış olduğu oturum girişi hatalarında ilgili user profilinin kilitlenip kilitlenmeyeceğini tutmaktadır.
    }
}

