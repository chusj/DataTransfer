using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Relay.Common.Helper
{
    public class JsonHelper : IHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string GetStringNodeValue(string jsonString, string nodeName)
        {
            try
            {
                // 将JSON字符串解析为JObject、尝试获取指定的节点
                JObject jsonObject = JObject.Parse(jsonString);
                JToken node = jsonObject[nodeName];

                if (node == null)
                {
                    return null;
                }

                // 返回节点的值作为字符串
                return node.ToString();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
                return null;
            }
        }

    }
}
