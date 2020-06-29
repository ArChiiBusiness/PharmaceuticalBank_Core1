using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PB.Core.DE
{
    interface IDataEverywhere
    {
        public List<FeedModel> GetFeeds();
        public bool PendingUpdate(FeedModel feed);
        public bool UpdateFeed(FeedModel feed);
        public string GetFeedData(FeedModel feed);
        public bool UploadFeedData(FeedModel feed, string feedData);
        public int GetUpdateCount();
    }
}
