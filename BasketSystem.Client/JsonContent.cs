using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BasketSystem.Client
{
    /// <summary>
    /// Represents request content in JSON.
    /// </summary>
    internal class JsonContent : StringContent
    {
        public JsonContent(object content)
        : base(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
        {
        }
    }
}
