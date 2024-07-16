using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    [Authorize(Policy ="Client")]
    public class TransactionController : ControllerBase
    {
        private readonly IBaseService<Role, RoleVo> _roleService;
        private readonly ISysUserService _userService;
        private readonly IUnitOfWorkManage _unitOfWorkManage;
        private readonly IHttpContextAccessor _httpContext;

        public TransactionController(IBaseService<Role, RoleVo> roleService,
            ISysUserService userService, 
            IUnitOfWorkManage unitOfWorkManage,
            IHttpContextAccessor httpContext)
        {
            _roleService = roleService;
            _userService = userService;
            _unitOfWorkManage = unitOfWorkManage;
            _httpContext= httpContext;
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

        /// <summary>
        /// 测试 【事务的切面编程】
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Test()
        {
            //用于输出http请求上下文中，jwt的申明信息
            var httpContext = _httpContext.HttpContext?.User.Claims.ToList();
            foreach (var item in httpContext)
            {
                await Console.Out.WriteLineAsync($"{item.Type} : {item.Value}");
            }

            return await _userService.TestTranPropagation();
        }
    }
}
