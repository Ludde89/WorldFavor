﻿using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;

namespace WorldFavor.Mappers
{
    public static class ReaderMapper
    {
        public static Reader Map(this ReaderEntity entity)
        {
            return entity != null
                ? new Reader
                {
                    Name = entity.Name,
                    Birth = entity.Birth
                }
                : new Reader();
        }
    }
}