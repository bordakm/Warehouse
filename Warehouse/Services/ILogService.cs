using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public interface ILogService
    {
        public void AddEntry(string userid, string text);
        public void ClearAllEntries();
        ICollection<LogEntry> ListAllLogs();
    }
}
