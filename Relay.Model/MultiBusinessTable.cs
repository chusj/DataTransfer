﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relay.Model
{
    /// <summary>
    /// 多租户-多表方案 业务表 <br/>
    /// </summary>
    [MultiTenant(TenantTypeEnum.Tables)]
    public class MultiBusinessTable : RootEntityTkey<long>
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
