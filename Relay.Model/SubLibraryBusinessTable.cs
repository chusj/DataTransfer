﻿using Relay.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relay.Model
{
    /// <summary>
    /// 多租户-多库方案 业务表 <br/>
    /// 公共库无需标记[MultiTenant]特性
    /// </summary>
    [MultiTenant(TenantTypeEnum.Db)]
    public class SubLibraryBusinessTable : RootEntityTkey<long>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}