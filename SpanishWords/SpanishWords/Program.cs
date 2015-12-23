using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Globalization;

namespace SpanishWords
{
    public static class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"C:\Users\paulo\Desktop\\words.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            for (int i = 1; i < 2494; i++)
            {

                HtmlDocument doc = new HtmlDocument();
                string pageUrl = "http://www.wordmine.info/Search.aspx?stype=words-that-starts-with&slang=es&page=" + i;
                var webRequest = HttpWebRequest.Create(pageUrl);
                Stream stream = webRequest.GetResponse().GetResponseStream();
                doc.Load(stream, Encoding.UTF8);
                stream.Close();
                string wordSelector = "//span[@class='notranslate'] ";
                HtmlNodeCollection words = doc.DocumentNode.SelectNodes(wordSelector);
                foreach(var w in words)
                {
                    var word = w.InnerText.ToString();
                    if (!word.Contains(" ") && !word.Contains("."))
                    {
                        word = RemoveDiacritics(word);
                        Console.WriteLine(word);
                        sw.WriteLine(word);
                    }
                }
            }
            sw.Flush();
            sw.Close();
            Console.ReadLine();
        }

        static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}
