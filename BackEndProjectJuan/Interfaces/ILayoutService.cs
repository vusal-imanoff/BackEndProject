using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Interfaces
{
    public interface ILayoutService
    {
        Task<IDictionary<string, string>> GetSettingsAsync();
    }
}
