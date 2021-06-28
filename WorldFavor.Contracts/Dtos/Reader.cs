using System;
using System.Collections.Generic;

namespace WorldFavor.Contracts.Dtos
{
    public class Reader
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public IEnumerable<Book>  Books { get; set; }
    }
}