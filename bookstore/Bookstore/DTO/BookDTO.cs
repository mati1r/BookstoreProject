using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DTO
{
    public class BookDTO
    {
        public string Id { get; set; }
        public string Publisher { get; set; }

        public string Pub_date { get; set; }

        public string Price { get; set; }

        public byte[] Zdjecie { get; set; }
    }
}
