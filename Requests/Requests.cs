using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WheatleyAPI.Requests
{
    public static class Requests // запити до інших АРІ
    {   
        private static string Request(string method, string uri, params string[] json) // універсальний метод запиту до АРІ
        { // params string[] json => необов'язкова зміна -> якщо json не передали, то список буде пустим
            if (method.ToLower() == "get" && json.Length != 0) return "fail"; // перевірка чи не передали у GET - json
            
            string result = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri); // об'єкт запиту
            
            switch (method.ToLower()) // вказування на метод http запиту 
            {
                case "get":
                    httpWebRequest.Method = "GET";
                    break;

                case "post":
                    httpWebRequest.Method = "POST";
                    break;

                case "put":
                    httpWebRequest.Method = "PUT";
                    break;

                case "delete":
                    httpWebRequest.Method = "DELETE";
                    break;

                default: return "fail";
            }

            if (method.ToLower() != "get" && json.Length != 0) //якщо GET -> запису у Body не буде, якщо json не передали запису не буде
            {
                httpWebRequest.ContentType = "application/json"; //який вид контенту передаємо =(json)

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) // запис у Body json'у
                {
                    streamWriter.Write(json[0]);
                }
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse(); // відправка запиту + отримання відповіді

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) // зчитувння json'y із Body
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public static List<ThesaurusDesignation> ThesaurusDesignation(string word) // запит до тезаурусу
        {
            string uri = $"https://dictionaryapi.com/api/v3/references/thesaurus/json/{word.ToLower()}?key=026cde69-3c8e-4f6e-b0a5-095f6bad8b8e"; // ств. URI 
            string json = Request("get", uri); // відправка запиту
            // десеріалізація json у об'єкт
            List<ThesaurusDesignation> designation = JsonSerializer.Deserialize<List<ThesaurusDesignation>>(json); 
            return designation; 
        }

        public static UrbanDesignation UrbanDesignation(string word) // запит до urbanD
        {
            string result = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://mashape-community-urban-dictionary.p.rapidapi.com/define?term={word.ToLower()}"); // ств. об'єкту запиту 

            httpWebRequest.Method = "GET";

            httpWebRequest.Headers["x-rapidapi-key"] = "6e367dc2a7mshf20ef13dee46145p169863jsn5917d0fdb1aa";
            httpWebRequest.Headers["x-rapidapi-host"] = "mashape-community-urban-dictionary.p.rapidapi.com";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return JsonSerializer.Deserialize<UrbanDesignation>(result);
        }
    }
}
