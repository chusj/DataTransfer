using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        //第18课测试

        [HttpGet]
        public async Task<object> Get()
        {
            return await _auditSqllogService.Query();
        }
    }
}
