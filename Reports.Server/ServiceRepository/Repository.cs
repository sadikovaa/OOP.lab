using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Server.ServiceRepository
{
    public class Repository<T>
    {
        private string _path;

        public Repository(string path)
        {
            _path = path;
        }
        public List<T> GetList()
        {
            
            List<T> list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_path));
            return list;
        }
        public void SetList(List<T> list)
        {
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(_path, json);
        }
    }
}