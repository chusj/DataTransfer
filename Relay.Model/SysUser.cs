namespace Relay.Model
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public class SysUser
    {
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string? CardNo { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
    }
}
