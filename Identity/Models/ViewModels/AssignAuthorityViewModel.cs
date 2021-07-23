using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Models.ViewModels
{
    public class AssignAuthorityViewModel
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
        public string Value { get; set; }
        public bool HasAssign { get; set; }




    }
}
