using IronOcr;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ISAI_APP.OCR
{
    public static class OCRUtil
    {
        const string skey = "b75f915c405d437986eec6ad0da48f9e";
        const string uriBase = "https://pruebabsd.cognitiveservices.azure.com/vision/v2.0/ocr";

        public static async Task<List<string>> GetText(HttpPostedFileBase file)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", skey);

            string requestPArameters = "";
            //string uri = uriBase + "?" + requestPArameters;
            Uri uri = new Uri("https://pruebabsd.cognitiveservices.azure.com/vision/v2.0/ocr");


            HttpResponseMessage response = null;
            byte[] byteData = GetImageAsByteArray(file);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                string contentstring = await response.Content.ReadAsStringAsync();

                var AllData = JsonConvert.DeserializeObject<RootObject>(contentstring);
                List<string> palabras = new List<string>();

                if (AllData != null && AllData.regions != null)
                {
                    foreach (var item in AllData.regions)
                    {
                        foreach (var line in item.lines)
                        {
                            foreach (var word in line.words)
                            {
                                palabras.Add(word.text);
                            }
                        }
                    }
                }
                return palabras;
            }
        }
        public static byte[] GetImageAsByteArray(HttpPostedFileBase file)
        {
            //FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            //FileStream fileStream = new FileStream(file.InputStream)
            BinaryReader binaryReader = new BinaryReader(file.InputStream);
            return binaryReader.ReadBytes((int)file.InputStream.Length);
        }

        public static T Deserialize<T>(string json)
        {
            Newtonsoft.Json.JsonSerializer s = new JsonSerializer();
            return s.Deserialize<T>(new JsonTextReader(new StringReader(json)));
        }

        public class Word
        {
            public string boundingBox { get; set; }
            public string text { get; set; }
        }

        public class Line
        {
            public string boundingBox { get; set; }
            public List<Word> words { get; set; }
        }

        public class Region
        {
            public string boundingBox { get; set; }
            public List<Line> lines { get; set; }
        }

        public class RootObject
        {
            public string language { get; set; }
            public double textAngle { get; set; }
            public string orientation { get; set; }
            public List<Region> regions { get; set; }
        }


    }
}