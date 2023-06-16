using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using Model;

namespace ClientApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Press Enter to send GET request for Root folder...");
      string html = string.Empty;
      string url = @"https://localhost:44308/api/catalog";

      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
      request.AutomaticDecompression = DecompressionMethods.GZip;
      
      string userName = Console.ReadLine();
      
      using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
      using (Stream stream = response.GetResponseStream())
      using (StreamReader reader = new StreamReader(stream))
      {
        html = reader.ReadToEnd();
      }
      Console.WriteLine("GET response:");
      Console.WriteLine(html);
      string input="";
      string name = "";
      do
      {
        Console.WriteLine("Press 1 to send POST request to Create Catalog");
        Console.WriteLine("2 to send GET request to get Catalog, ");
        Console.WriteLine("3 to send PUT request to edit Catalog, ");
        Console.WriteLine("4 to send DELETE request to delete Catalog.");
        Console.WriteLine("0 to EXIT from program.");
        input = Console.ReadLine();
        switch (input)
        {
          case "1":
            Console.WriteLine("Enter ID of the Parent Catalog... ");
            input = Console.ReadLine();
            Console.WriteLine("Enter Name for a new Catalog... ");
            name = Console.ReadLine();
            Catalog newCatalog = new Catalog { Name = name, ParentId = Int32.Parse(input) };
            Console.WriteLine("POST response:");
            Console.WriteLine(Post(url, JsonSerializer.Serialize(newCatalog)));
            break;
          case "2":
            Console.WriteLine("Enter ID of the Catalog... ");
            input = Console.ReadLine();
            Console.WriteLine("GET response:");
            Console.WriteLine(Get(url + "/" + input));
            break;
          case "3":
            Console.WriteLine("Enter ID of the Catalog... ");
            input = Console.ReadLine();
            Console.WriteLine("Enter a new Name for the Catalog... ");
            name = Console.ReadLine();
            Catalog catalog = new Catalog { Id = Int32.Parse(input), Name = name};
            Console.WriteLine("PUT response:");
            Console.WriteLine(Post(url + "/" + input, JsonSerializer.Serialize(catalog), "application/json", "PUT"));
            break;
          case "4":
            Console.WriteLine("Enter ID of the Catalog... ");
            input = Console.ReadLine();
            Console.WriteLine("DELETE response:");
            Console.WriteLine(Post(url + "/" + input, "", "none", "DELETE"));
            break;
          case "0":
            Console.WriteLine("Exiting... ");
            break;
        }
      } while (input != "0");
    }
    public static string Get(string uri)
    {
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
      request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

      using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
      using (Stream stream = response.GetResponseStream())
      using (StreamReader reader = new StreamReader(stream))
      {
        return reader.ReadToEnd();
      }
    }

    public static string Post(string uri, string data, string contentType= "application/json", string method = "POST")
    {
      byte[] dataBytes = Encoding.UTF8.GetBytes(data);

      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
      request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      request.ContentLength = dataBytes.Length;
      request.ContentType = contentType;
      request.Method = method;

      using (Stream requestBody = request.GetRequestStream())
      {
        requestBody.Write(dataBytes, 0, dataBytes.Length);
      }

      using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
      using (Stream stream = response.GetResponseStream())
      using (StreamReader reader = new StreamReader(stream))
      {
        return reader.ReadToEnd();
      }
    }
  }
}
