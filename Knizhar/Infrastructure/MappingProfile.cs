namespace Knizhar.Infrastructure
{
    using AutoMapper;
    using Knizhar.Data.Models;
    using Knizhar.Models.Books;
    using Knizhar.Services.Books.Models;
    using Knizhar.Services.Knizhari;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Genre, BookGenreServiceModel>();
            this.CreateMap<Town, TownServiceModel>();
            this.CreateMap<Language, BookLanguageServiceModel>()
                .ForMember(l => l.Name, cfg => cfg.MapFrom(l => l.LanguageName));
            this.CreateMap<Condition, BookConditionServiceModel>()
                .ForMember(c => c.Name, cfg => cfg.MapFrom(c => c.ConditionName));

            this.CreateMap<Book, BookServiceModel>()
                .ForMember(b => b.ImagePath, cfg => cfg.MapFrom(b => "/images/books/" + b.Image.Id + "." + b.Image.Extension))
                .ForMember(b => b.TheBookIsFor, opt => opt.MapFrom(src => src.IsForGiveAway ? "Give away" : "Exchange"));

            this.CreateMap<BookDetailsModel, BookFormModel>();

            this.CreateMap<Book, BookDetailsModel>()
                .ForMember(b => b.UserId, cfg => cfg.MapFrom(b => b.Knizhar.UserId))
                .ForMember(b => b.LanguageName, cfg => cfg.MapFrom(b => b.Language.LanguageName))
                .ForMember(b => b.TownName, cfg => cfg.MapFrom(b => b.Knizhar.Town.Name))
                .ForMember(b => b.ImagePath, cfg => cfg.MapFrom(b => "/images/books/" + b.Image.Id + "." + b.Image.Extension))
                .ForMember(b => b.TheBookIsFor, opt => opt.MapFrom(src => src.IsForGiveAway ? "Give away" : "Exchange")); ;
        }
    }
}
