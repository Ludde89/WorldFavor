using System;

namespace WorldFavor.Contracts.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime Checkout { get; set; }
        public bool IsLost { get; set; }
        public int ReaderId { get; set; }
        public ReaderEntity Reader { get; set; }
    }
}