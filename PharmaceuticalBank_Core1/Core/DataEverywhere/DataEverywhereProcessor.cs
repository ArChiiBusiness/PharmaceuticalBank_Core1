using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PB.Core.DE;
using PharmaceuticalBank_Core1.Models.DAL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PB.Core.DE
{
    public class DataEverywhereProcessor : IDataEverywhere
    {
        public static string apiKey = "b82d9f0b-26ff-4d97-bbda-fb0240f95718";

        public List<FeedModel> GetFeeds()
        {
            var client = new WebClient();
            client.Headers.Add("ApiKey", apiKey);
            var response = client.DownloadString("https://app.dataeverywhere.com/api/v1/feeds");
            var feeds = JsonConvert.DeserializeObject<List<FeedModel>>(response);
            return feeds;
        }

        public string GetFeedData(FeedModel feed)
        {
            var client = new WebClient();
            client.Headers.Add("ApiKey", apiKey);
            var response = client.DownloadString("https://app.dataeverywhere.com/api/v1/feeds/" + feed.Id.ToString() + "/true");
            var feedData = response;
            return feedData;
        }

        public bool PendingUpdate(FeedModel feed)
        {
            return (feed.Description == "ClientBuyRequest" || feed.Description == "ClientSellRequest");
        }

        public bool UpdateFeed(FeedModel feed)
        {
            var db = new pharmabank1Context();
            var feedData = GetFeedData(feed);
            var rowArr = JArray.Parse(feedData);
            var newFeedData = new JArray();

            foreach (JObject row in rowArr)
            {
                bool toAdd = false;
                var newRow = new JObject();

                if (feed.Description == "ClientBuyRequest")
                {
                    newRow["Looking to buy"] = "";
                    newRow["Strenght"] = "";
                    newRow["Quantity"] = "";
                    newRow["Results"] = "";
                    newRow["Companies"] = "";
                    newRow["Date"] = "";

                    foreach (var col in row)
                    {
                        string columnName = col.Key;
                        string getValue = col.Value.ToString();

                        if (columnName == "Looking to buy" && getValue != "")
                        {
                            toAdd = true;
                            newRow["Looking to buy"] = getValue;
                            newRow["Strenght"] = "";
                            newRow["Quantity"] = "";
                            newRow["Results"] = String.Format("{0:n0}",db.Shipments.FromSqlRaw("SELECT Date FROM Shipments WHERE Contains([Goods Shipped],'" + getValue + "') AND ShipperCompanyId IS NOT NULL").Count()) + " Results";
                            newRow["Companies"] = String.Format("{0:n0}",db.Shipments.FromSqlRaw("SELECT DISTINCT(ShipperCompanyId) FROM Shipments WHERE Contains([Goods Shipped],'" + getValue + "') AND ShipperCompanyId IS NOT NULL").Count()) + " Companies";
                            newRow["Date"] = db.Shipments.FromSqlRaw("SELECT Date FROM Shipments WHERE Contains([Goods Shipped],'" + getValue + "') AND ShipperCompanyId IS NOT NULL AND Date IS NOT NULL").Select(d => d.Date).Max();
                        }
                    }

                    if (toAdd)
                    {
                        newFeedData.Add(newRow);
                    }

                    toAdd = false;

                }

                if (feed.Description == "ClientSellRequest")
                {
                    newRow["Looking to sell"] = "";
                    newRow["Strenght"] = "";
                    newRow["Quantity"] = "";
                    newRow["Results"] = "";
                    newRow["Companies"] = "";
                    newRow["Date"] = "";
                    foreach (var col in row)
                    {
                        string columnName = col.Key;
                        string getValue = col.Value.ToString();

                        if (columnName == "Looking to sell" && getValue != "")
                        {
                            toAdd = true;
                            newRow["Looking to sell"] = getValue;
                            newRow["Strenght"] = "";
                            newRow["Quantity"] = "";
                            newRow["Results"] = String.Format("{0:n0}",db.Shipments.FromSqlRaw("SELECT Date FROM Shipments WHERE Contains([Goods Shipped],'" + getValue + "') AND ConsigneeCompanyId IS NOT NULL").Count()) + " Results";
                            newRow["Companies"] = String.Format("{0:n0}",db.Shipments.FromSqlRaw("SELECT DISTINCT(ConsigneeCompanyId) FROM Shipments WHERE Contains([Goods Shipped],'" + getValue + "') AND ConsigneeCompanyId IS NOT NULL").Count()) + " Companies";
                            newRow["Date"] = db.Shipments.FromSqlRaw("SELECT Date FROM Shipments WHERE Contains([Goods Shipped],'" + getValue + "') AND ConsigneeCompanyId IS NOT NULL AND Date IS NOT NULL").Select(d => d.Date).Max();
                        }
                    }

                    if (toAdd)
                    {
                        newFeedData.Add(newRow);
                    }

                    toAdd = false;
                }
                
            }

            if (newFeedData.Count > 0)
            {
                UploadFeedData(feed, Newtonsoft.Json.JsonConvert.SerializeObject(newFeedData));
            }

            return true;
        }

        public bool UploadFeedData(FeedModel feed, string feedData)
        {
            var client = new WebClient();
            client.Headers.Add("ApiKey", apiKey);
            client.Headers.Add("Content-type", "text/json");
            feed.Description = "PharmaceuticalBankRequest";
            client.Headers.Add("DE-Feed", JsonConvert.SerializeObject(feed));
            var response = client.UploadString("https://app.dataeverywhere.com/api/v1/revisions/" + feed.Id.ToString(), feedData);
            return true;
        }

        public int GetUpdateCount()
        {
            var feeds = GetFeeds();
            int count = 0;

            foreach (var feed in feeds)
            {
                if (PendingUpdate(feed))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
