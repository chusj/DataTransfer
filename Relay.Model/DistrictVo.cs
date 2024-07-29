namespace Relay.Model
{
    /// <summary>
    /// 地区、区域视图模型
    /// </summary>
    public class DistrictVo
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Pinyin { get; set; }

        /// <summary>
        /// 层级（1: 省；2: 市；3: 县；4: 乡镇）
        /// </summary>
        public int Level { get; set; } = 0;

        /// <summary>
        /// 父节点编码
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 人口
        /// </summary>
        public float Population { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public float Area { get; set; }
    }
}
