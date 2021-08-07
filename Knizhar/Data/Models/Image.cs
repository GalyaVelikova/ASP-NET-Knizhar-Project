namespace Knizhar.Data.Models
{
    using System;
    public class Image
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int BookId { get; set; }

        public Book Book { get; set; }
        public int AddedByKnizharId { get; set; }

        public Knizhar AddedByKnizhar { get; set; }

        public string Extension { get; set; }

        //The contetnts of the image is in the file system.
    }
}
