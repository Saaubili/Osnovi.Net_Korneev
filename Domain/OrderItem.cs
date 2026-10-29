using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderItem
    {
        public long ClientId {  get; set; }
        public long OrderId { get; set; }
        public string ItemName { get; set; } = "";
        public int ItemQuantity { get; set; }
        public float ItemPrice { get; set; }

    }
}
