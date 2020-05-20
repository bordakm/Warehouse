using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public class LogService : ILogService
    {
        private WarehouseContext context;
        public LogService(WarehouseContext context)
        {
            this.context = context;
        }

        public void AddEntry(string userid, string text)
        {
            context.LogEntries.Add(new LogEntry()
            {
                Time = DateTime.UtcNow.ToLocalTime(),
                EmployeeId  = userid,
                Text = text,                
            });
            context.SaveChanges();            
        }

        public void ClearAllEntries()
        {
            context.LogEntries.RemoveRange(context.LogEntries);
            context.SaveChanges();
        }

        public ICollection<LogEntry> ListAllLogs()
        {
            return context.LogEntries.Include(le => le.Employee).OrderByDescending(le=>le.Time).Select(le => le).ToList();
        }
    }
}
