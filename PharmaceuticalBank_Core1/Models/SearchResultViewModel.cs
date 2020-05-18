using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaceuticalBank_Core1.Models
{
    public class SearchResultViewModel
    {

        public class CompanyViewModel { 
            public System.Guid Id { get; set; }
            public String Name { get; set; }
            public String Address { get; set; }
            public String Country { get; set; }
            public String Phone { get; set; }

        }

        public class SearchResultViewModelCompany { 
            public CompanyViewModel Company { get; set; }
            public int Count { get; set; }
        }

        public List<SearchResultViewModelCompany> Companies;
        public int TotalCount { get; set; }
    }
}
