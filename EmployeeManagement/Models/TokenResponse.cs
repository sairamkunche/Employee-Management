using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class TokenResponse
    {
        string access_token { get; set; }
        string Bearer { get; set; }
    }
}
