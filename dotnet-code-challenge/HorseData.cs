using System;
using System.Collections.Generic;
using dotnet_code_challenge.Model;
using System.Xml.Linq;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace dotnet_code_challenge
{
    public class HorseData
    {
        public List<ResultObject> getDataFromXML()
        {
            //Read data from xml and show horsename and price in ascending order
            return readDataFromXML();
        }

        public List<ResultObject> getDataFromJson()
        {
            //Read data from json file and show horsename and price in ascending order
            return readDataFromJson();
        }

        private static List<ResultObject> readDataFromJson()
        {
            using (StreamReader r = new StreamReader("FeedData\\Wolferhampton_Race1.json"))
            {
                var raceData = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<RaceObject>(raceData);

                var horseDetails = (from selection in items.RawData.Markets[0].Selections
                                    orderby selection.Tags.name
                                    select new ResultObject
                                    {
                                        HorseName = selection.Tags.name,
                                        HorsePrice = selection.Price.ToString()
                                    }).ToList();
                return horseDetails;
            }
        }

        private static List<ResultObject> readDataFromXML()
        {
            XDocument doc = XDocument.Load("FeedData\\Caulfield_Race1.xml");

            var horseName = (from race in doc.Descendants("race")
                             from horses in race.Elements("horses")
                             from horse in horses.Elements("horse")
                             select new
                             {
                                 name = horse.Attribute("name").Value,
                                 number = horse.Element("number").Value
                             }).ToList();

            var horsePrice = (from prices in doc.Descendants("prices")
                              from price in prices.Elements("price")
                              from horses in price.Elements("horses")
                              from horse in horses.Elements("horse")

                              select new
                              {
                                  number = horse.Attribute("number").Value,
                                  price = horse.Attribute("Price").Value
                              }).ToList();


            var horseDetails = (from horse in horseName
                                join price in horsePrice
                                on horse.number equals price.number
                                orderby horse.name
                                select new ResultObject
                                {
                                    HorseName = horse.name,
                                    HorsePrice = price.price
                                }).ToList();

            return horseDetails;
        }

    }
}
