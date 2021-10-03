using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HIMS_Web.Infrastructure.Authentication
{
    public interface ICustomPrincipal : IPrincipal
    {
        int Id { get; set; }
        string Name { get; set; }
        //string MiddleName { get; set; }
        //string LastName { get; set; }
        string Email { get; set; }

    }
}
