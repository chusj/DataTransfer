using Relay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relay.Repository
{
    internal interface ISysUserRepository
    {
        Task<List<SysUser>> Query();
    }
}
