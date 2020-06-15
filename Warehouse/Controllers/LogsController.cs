using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse;
using Warehouse.Controllers.DTO;
using Warehouse.Entities;
using Warehouse.Services;

namespace Warehouse.Controllers
{
    [Route("api/logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly WarehouseContext _context;
        private readonly ILogService logService;
        public LogsController(WarehouseContext context, ILogService logService)
        {
            _context = context;
            this.logService = logService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ListItem>> GetAllLogs()
        {
            return Ok(logService.ListAllLogs().Select(ToModelLogEntry));
        }

        [HttpDelete]
        [Authorize]
        public ActionResult DeleteAllLogs()
        {
            logService.ClearAllEntries();
            return Ok();
        }


        private ModelLogEntry ToModelLogEntry(LogEntry entry)
        {
            if (entry == null) return null;
            return new ModelLogEntry
            {
                EmployeeName = entry.Employee.FullName,
                Text = entry.Text,
                Time = entry.Time.ToString()
            };
        }
        
    }
}
