using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;

namespace WorldFavor.Mappers
{
    public static class BookMapper
    {
        public static Book Map(this BookEntity entity)
        {
            return entity != null
                ? new Book
                {
                    Checkout = entity.Checkout,
                    ISBN = entity.ISBN,
                    IsLost = entity.IsLost,
                    Title = entity.Title
                }
                : null;
        }

        public static BookEntity Map(this Book book)
        {
            return book != null
                ? new BookEntity
                {
                    Checkout = book.Checkout,
                    ISBN = book.ISBN,
                    IsLost = book.IsLost,
                    Title = book.Title
                }
                : null;
        }
    }
}