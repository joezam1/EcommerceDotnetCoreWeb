using Newtonsoft.Json;
using Project.Shop.BusinessLogic.Interfaces;

namespace Project.Shop.BusinessLogic.Helpers
{
    public class JsonHelper : IJsonHelper
    {
        public string JsonSerializeObject(object selectedObject)
        {
            string objectsJson = JsonConvert.SerializeObject(selectedObject, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return objectsJson;
        }
    }
}