using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string mail, string token);
    }
}
