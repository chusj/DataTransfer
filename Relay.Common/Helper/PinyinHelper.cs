using NPinyin;
using System.Text;

namespace Relay.Common.Helper
{
    public class PinyinHelper : IHelper
    {
        /// <summary>
        /// 获取给定中文姓名的拼音首字母缩写。
        /// </summary>
        /// <param name="name">中文姓名</param>
        /// <returns>拼音首字母缩写</returns>
        public static string GetInitials(string name)
        {
            var pinyin = Pinyin.GetPinyin(name);
            var initials = new StringBuilder();

            foreach (var part in pinyin.Split(' '))
            {
                if (!string.IsNullOrEmpty(part))
                {
                    initials.Append(part.Substring(0, 1).ToUpper());
                }
            }

            return initials.ToString();
        }

        /// <summary>
        /// 获取给定中文姓名的全拼。
        /// </summary>
        /// <param name="name">中文姓名</param>
        /// <returns>全拼字符串</returns>
        public static string GetFullPinyin(string name)
        {
            return Pinyin.GetPinyin(name);
        }
    }
}
