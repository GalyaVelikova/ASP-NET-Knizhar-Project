namespace Knizhar.Infrastructure
{
    using AutoMapper;
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using Knizhar.Models.Books.Models;
    using Knizhar.Services.Books.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Book, BookServiceModel>();
            this.CreateMap<BookDetailsServiceModel, BookFormModel>();

            this.CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.UserId, cfg => cfg.MapFrom(b => b.Knizhar.UserId));
        }
    }
}
