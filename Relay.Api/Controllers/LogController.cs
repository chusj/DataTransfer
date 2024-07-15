﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relay.Common;
using Relay.IService;
using Relay.Model;

namespace Relay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IBaseService<AuditSqlLog, AuditSqlLogVo> _auditSqllogService;

        public LogController(IBaseService<AuditSqlLog, AuditSqlLogVo> auditSqllogService)
        {
            _auditSqllogService = auditSqllogService;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            return await _auditSqllogService.QuerySplit(d => d.DateTime <= Convert.ToDateTime("2023-12-24"));
        }

        [HttpPost]
        public async Task<object> Post()
        {
            TimeSpan timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var id = timeSpan.TotalSeconds.ObjToLong();
            return await _auditSqllogService.AddSplit(new AuditSqlLog()
            {
                Id = id,
                DateTime = Convert.ToDateTime("2023-12-23"),
            });
        }
    }
}
