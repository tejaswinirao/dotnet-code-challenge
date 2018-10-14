using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;
using dotnet_code_challenge.Model;
using System.Collections.Generic;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            HorseData data = new HorseData();

            Console.WriteLine("*********Reading data from XMl File***********");

            List<ResultObject> dataXml = data.getDataFromXML();
            foreach (var d in dataXml)
            {
                Console.WriteLine("Horse Name : " + d.HorseName + "Horse Price : " + d.HorsePrice);
            }
            Console.WriteLine("Data from XMl File is shown above");

            Console.WriteLine("*********************Reading data from JSON File*********************");

            List<ResultObject> dataJson = data.getDataFromJson();
            foreach (var d in dataJson)
            {
                Console.WriteLine("Horse Name : " + d.HorseName + "Horse Price : " + d.HorsePrice);
            }
            Console.WriteLine("Data from Json File is shown above");
            Console.ReadLine();
        }
    }
}

