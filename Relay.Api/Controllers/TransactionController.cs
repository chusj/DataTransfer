using Microsoft.AspNetCore.Mvc;
using Relay.Common;
using Relay.IService;
using Relay.Model;
using Relay.Repository.UnitOfWorks;

namespace Relay.Api.Controllers
{
    /// <summary>
    /// 事务控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IBaseService<Role, RoleVo> _roleService;
        private readonly IUnitOfWorkManage _unitOfWorkManage;

        public TransactionController(IBaseService<Role, RoleVo> roleService, IUnitOfWorkManage unitOfWorkManage)
        {
            _roleService = roleService;
            _unitOfWorkManage = unitOfWorkManage;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                Console.WriteLine($"Begin Transaction");

                _unitOfWorkManage.BeginTran();
                //using var uow = _unitOfWorkManage.CreateUnitOfWork();
                var roles = await _roleService.Query();
                Console.WriteLine($"1 first time : the count of role is :{roles.Count}");


                Console.WriteLine($"insert a data into the table role now.");
                TimeSpan timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var insertPassword = await _roleService.Add(new Role()
                {
                    Id = timeSpan.TotalSeconds.ObjToLong(),
                    IsDeleted = false,
                    Name = "role name",
                });

                var roles2 = await _roleService.Query();
                Console.WriteLine($"2 second time : the count of role is :{roles2.Count}");


                int ex = 0;
                Console.WriteLine($"There's an exception!!");
                Console.WriteLine($" ");
                int throwEx = 1 / ex;

                //uow.Commit();
                _unitOfWorkManage.CommitTran();
            }
            catch (Exception)
            {
                _unitOfWorkManage.RollbackTran();  //如果忘记写这句，第2次提交出现问题，页面一直转圈

                var roles3 = await _roleService.Query();
                Console.WriteLine($"3 third time : the count of role is :{roles3.Count}");
            }

            return "ok";
        }
    }
}
