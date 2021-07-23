using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.ViewModels
{
    public class UserDetailViewModel
    {
        [Required (ErrorMessage ="Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Telefon no boş geçilemez")]
        public string PhoneNumber { get; set; }

        public Guid Id { get; set; }
    }
}
