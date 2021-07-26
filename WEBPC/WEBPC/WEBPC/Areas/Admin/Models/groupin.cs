using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPC.Areas.Admin.Models
{
    public class groupin
    {
        public string Id { get; set; }
        public DateTime date { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Sum { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<decimal> Avg { get; set; }
        public Nullable<decimal> Max { get; set; }
        public Nullable<decimal> min { get; set; }
        public int dongia { get; set; }
    }
}