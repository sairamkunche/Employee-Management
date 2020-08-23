using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Contracts.Interfaces
{
    public interface ITokenResponse
    {
        string access_token { get; set; }
        string Bearer { get; set; }
    }
}
