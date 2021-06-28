using System;

namespace WorldFavor.Contracts.Dtos
{
    public class Book
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime? Checkout { get; set; }
        public Reader Reader { get; set; }
        public bool IsLost { get; set; }
    }
}