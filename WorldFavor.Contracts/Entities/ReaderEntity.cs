using System;
using System.Collections.Generic;
using WorldFavor.Contracts.Dtos;

namespace WorldFavor.Contracts.Entities
{
    public class ReaderEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public IEnumerable<BookEntity> Books { get; set; }
    }
}