using System.Text.Json;

namespace TweetsAnalysis.Common
{
    public static class SerializationHelper<T>
    {
        public static List<T> Deserialize(ref string json)
        {
            var jsonArray = json.Split("\r\n");
            var listObjects = new List<T>();
            var newJson = string.Empty;
            for (int i = 0; i < jsonArray.Length; i++)
            {
                try
                {
                    var data = JsonSerializer.Deserialize<T>(jsonArray[i]);
                    if(data != null)
                    {
                        listObjects.Add(data);
                    }                    
                }
                catch (Exception)
                {
                    // store unfinished 
                    if (i == jsonArray.Length - 1) newJson += jsonArray[i];
                }
            }

            json = newJson;

            return listObjects;
        }
    }
}