using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCacheCSharp
{
    class Program
    {
        private InMemoryProcess _cacheManager;
        private string _key;
        private CacheItemPolicy _policy;
        public Program()
        {
            _cacheManager = new InMemoryProcess(new MemoryCache("Test Cache"));
        }
        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Cache Menu : ");
                Console.WriteLine("1. Create Cache");
                Console.WriteLine("2. Check Cache");
                Console.WriteLine("3. Get Cache");
                Console.WriteLine("4. Remove Cache");
                Console.WriteLine("5. Create New Or Get Existing Cache");
                Console.Write("Select ? ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        CreateCache();
                        break;
                    case "2":
                        CheckCache();
                        break;
                    case "3":
                        GetCache();
                        break;
                    case "4":
                        RemoveCache();
                        break;
                    case "5":
                        CreateNewOrGetExisting();
                        break;
                    default:
                        Console.WriteLine("Wrong Input.");
                        break;
                }
            }
        }
        void CreateCache()
        {
            Console.Write("Input Key : ");
            string key = Console.ReadLine();
            Console.Write("Input Value : ");
            string value = Console.ReadLine();
            CacheItem item = new CacheItem(key, value);
            _policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
            };
           if( _cacheManager.SetCache(item, _policy))
            {
                Console.WriteLine("Cache Success Created dengan waktu expired 5 minutes.");
                _key = key;
            }
            else
            {
                Console.WriteLine("Cache Failed Created");
            }
        }
        void CheckCache()
        {
            Console.Write("Input Key : ");
            string key = Console.ReadLine();
            if (_cacheManager.CheckExist(key))
            {
                Console.WriteLine("Cache dengan key {0} tersimpan di memory.", key);
            }
            else
            {
                Console.WriteLine("Cache dengan key {0} not found di memory.", key);
            }
        }
        void GetCache()
        {
            Console.Write("Input Key : ");
            string key = Console.ReadLine();
            CacheItem item = _cacheManager.GetCache(key);
            if(item!= null)
            {
                Console.WriteLine("Cache Key : {0}, Cache Item : {1}", key, item.Value);
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }
        void RemoveCache()
        {
            Console.Write("Input Key : ");
            string key = Console.ReadLine();
            object result = _cacheManager.RemoveCache(key);
            if(result != null)
            {
                Console.WriteLine("Success remove this cache.");
            }
            else
            {
                Console.WriteLine("failed remove this cache.");
            }
        }
        void CreateNewOrGetExisting()
        {
            Console.Write("Input Key : ");
            string key = Console.ReadLine();
            Console.Write("Input Value : ");
            string value = Console.ReadLine();
            CacheItem item = new CacheItem(key, value);
            _policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10)
            };
            var result = _cacheManager.AddOrGetExistingCache(item, _policy);
            if (result != null) Console.WriteLine("Berhasil Create/Get Cache : {0}", result);
            else Console.WriteLine("Failed Create/Get cache.");
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            p.MainMenu();
        }
    }
}
