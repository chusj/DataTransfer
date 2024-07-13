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

                //方式1 => 1.开始事务
                //_unitOfWorkManage.BeginTran();

                //方式2 => 1.开始事务
                using var uow = _unitOfWorkManage.CreateUnitOfWork();
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

                // 方式2  => 2.提交事务
                uow.Commit(); //析构时如果未提交，回自动回滚

                //方式  => 2 提交事务
                //_unitOfWorkManage.CommitTran();
            }
            catch (Exception)
            {
                //方式1  => 3.回滚事务 => 如果忘记写这句，第2次提交出现不能正常回滚导致页面一直转圈
                //_unitOfWorkManage.RollbackTran();  

                var roles3 = await _roleService.Query();
                Console.WriteLine($"3 third time : the count of role is :{roles3.Count}");
            }

            return "ok";
        }
    }
}
