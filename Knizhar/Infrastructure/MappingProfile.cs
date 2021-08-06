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
            this.CreateMap<Book, BookServiceModel>()
                .ForMember(b => b.Author, cfg => cfg.MapFrom(b => b.Author.Name))
                .ForMember(b => b.ImagePath, cfg => cfg.MapFrom(b => "/images/books/" + b.Image.Id + "." + b.Image.Extension))
                .ForMember(b => b.TheBookIsFor, opt => opt.MapFrom(src => src.IsForGiveAway ? "Give away" : "Exchange"));

            this.CreateMap<BookDetailsServiceModel, BookFormModel>();

            this.CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(b => b.UserId, cfg => cfg.MapFrom(b => b.Knizhar.UserId));
        }
    }
}
