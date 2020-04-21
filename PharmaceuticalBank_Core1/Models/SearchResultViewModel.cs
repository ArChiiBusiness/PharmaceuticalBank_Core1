using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaceuticalBank_Core1.Models
{
    public class SearchResultViewModel
    {

        public class SearchResultViewModelCompany { 
            public DAL2.Companies Company { get; set; }
            public int Count { get; set; }
        }

        public List<SearchResultViewModelCompany> Companies;
        public int TotalCount { get; set; }
    }
}
