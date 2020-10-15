using System;

namespace Advantage.API.Model
{
    public class Trip
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public decimal QuotedPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? RTA { get; set; }
    } 

}