namespace Knizhar.Infrastructure.Extensions
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedGenres(services);
            SeedLanguages(services);
            SeedCondition(services);
            SeedTowns(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            data.Database.Migrate();

        }
        private static void SeedGenres(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {
                new Genre {Name = "Fiction"},
                new Genre {Name = "Biographical"},
                new Genre {Name = "Comics"},
                new Genre {Name = "Crime"},
                new Genre {Name = "Fantasy"},
                new Genre {Name = "Historical"},
                new Genre {Name = "Horros"},
                new Genre {Name = "Humorous"},
                new Genre {Name = "Legal"},
                new Genre {Name = "Medical"},
                new Genre {Name = "Political"},
                new Genre {Name = "Psychological"},
                new Genre {Name = "Romance"},
                new Genre {Name = "Science Fiction"},
                new Genre {Name = "CookingFood"},
                new Genre {Name = "HealthDailyLiving"},
                new Genre {Name = "School/Education"},
                new Genre {Name = "Science and Technology"},
                new Genre {Name = "Art"},
                new Genre {Name = "Computer"},
                new Genre {Name = "Health and Fitness"},
            });

            data.SaveChanges();
        }

        private static void SeedLanguages(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data.Languages.Any())
            {
                return;
            }

            data.Languages.AddRange(new[]
            {
                new Language {LanguageName = "Bulgarian"},
                new Language {LanguageName = "English"},

            });

            data.SaveChanges();
        }

        private static void SeedCondition(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data.Conditions.Any())
            {
                return;
            }

            data.Conditions.AddRange(new[]
            {
                new Condition {ConditionName = "Excellent"},
                new Condition {ConditionName = "Very Good"},
                new Condition {ConditionName = "Good"},
                new Condition {ConditionName = "Bad"},

            });

            data.SaveChanges();
        }

        private static void SeedTowns(IServiceProvider services)
        {
            var data = services.GetRequiredService<KnizharDbContext>();

            if (data.Towns.Any())
            {
                return;
            }

            data.Towns.AddRange(new[]
            {
                new Town {Name = "Sofia"},
                new Town {Name = "Varna" },
                new Town {Name = "Plovdiv" },
                new Town {Name = "Burgas" },
                new Town {Name = "Ruse" },
                new Town {Name = "Stara Zagora" },
                new Town {Name = "Pleven" },
                new Town {Name = "Sliven" },
                new Town {Name = "Dobrich" },
                new Town {Name = "Shumen" },
                new Town {Name = "Haskovo" },
                new Town {Name = "Pazardzhik" },
                new Town {Name = "Yambol" },
                new Town {Name = "Pernik" },
                new Town {Name = "Blagoevgrad" },
                new Town {Name = "Veliko Tarnovo" },
                new Town {Name = "Vratsa" },
                new Town {Name = "Kardzhali" },
                new Town {Name = "Kyustendil" },
                new Town {Name = "Gabrovo" },
                new Town {Name = "Asenovgrad" },
                new Town {Name = "Vidin" },
                new Town {Name = "Kazanlak"},
                new Town {Name = "Montana" },
                new Town {Name = "Targovishte" },
                new Town {Name = "Dimitrovgrad" },
                new Town {Name = "Silistra" },
                new Town {Name = "Lovech" },
                new Town {Name = "Razgrad" },
                new Town {Name = "Petrich" },
                new Town {Name = "Dupnitsa" },
                new Town {Name = "Gorna Oryahovitsa" },
                new Town {Name = "Smolyan" },
                new Town {Name = "Sandanski" },
                new Town {Name = "Samokov" },
                new Town {Name = "Nova Zagora" },
                new Town {Name = "Karlovo" },
                new Town {Name = "Velingrad" },
                new Town {Name = "Sevlievo" },
                new Town {Name = "Lom" },
                new Town {Name = "Aytos" },
                new Town {Name = "Svishtov" },
                new Town {Name = "Harmanli" },
                new Town {Name = "Troyan" },
                new Town {Name = "Botevgrad" },
                new Town {Name = "Gotse Delchev" },
                new Town {Name = "Peshtera" },
                new Town {Name = "Karnobat" },
                new Town {Name = "Svilengrad" },
                new Town {Name = "Momchilgrad" },
                new Town {Name = "Panagyurishte" },
                new Town {Name = "Popovo" },
                new Town {Name = "Chirpan" },
                new Town {Name = "Rakovski" },
                new Town {Name = "Parvomay" },
                new Town {Name = "Provadia" },
                new Town {Name = "Berkovitsa" },
                new Town {Name = "Novi Pazar" },
                new Town {Name = "Radomir" },
                new Town {Name = "Ihtiman" },
                new Town {Name = "Pomorie" },
                new Town {Name = "Novi Iskar" },
                new Town {Name = "Nesebar" },
                new Town {Name = "Radnevo" },
                new Town {Name = "Balchik" },
                new Town {Name = "Razlog" },
                new Town {Name = "Byala Slatina" },
                new Town {Name = "Kozloduy" },
                new Town {Name = "Kavarna" },
                new Town {Name = "Stamboliyski" },
                new Town {Name = "Pavlikeni" },
                new Town {Name = "Isperih" },
                new Town {Name = "Mezdra" },
                new Town {Name = "Kostinbrod" },
                new Town {Name = "Etropole" },
                new Town {Name = "Bankya" },
                new Town {Name = "Knezha" },
                new Town {Name = "Elhovo" },
                new Town {Name = "Omurtag" },
                new Town {Name = "Tutrakan" },
                new Town {Name = "Ardino" },
                new Town {Name = "Lukovit" },
                new Town {Name = "Teteven" },
                new Town {Name = "Kubrat" },
                new Town {Name = "Tryavna" },
                new Town {Name = "Bansko" },
                new Town {Name = "Sredets" },
                new Town {Name = "Veliki Preslav" },
                new Town {Name = "Krichim" },
                new Town {Name = "Devnya" },
                new Town {Name = "Rakitovo" },
                new Town {Name = "Septemvri" },
                new Town {Name = "Krumovgrad" },
                new Town {Name = "Belo Pole" },
                new Town {Name = "Lyaskovets" },
                new Town {Name = "Simeonovgrad" },
                new Town {Name = "Dzhebel" },
                new Town {Name = "Dulovo" },
                new Town {Name = "Aksakovo" },
                new Town {Name = "Belene" },
                new Town {Name = "Beloslav" },
                new Town {Name = "Svoge" },
                new Town {Name = "Dryanovo" },
                new Town {Name = "Lyubimets" },
                new Town {Name = "Tervel" },
                new Town {Name = "Zlatograd" },
                new Town {Name = "Dolni Chiflik" },
                new Town {Name = "Simitli" },
                new Town {Name = "Pirdop" },
                new Town {Name = "Kuklen" },
                new Town {Name = "Slivnitsa" },
                new Town {Name = "Elin Pelin" },
                new Town {Name = "General Toshevo" },
                new Town {Name = "Devin" },
                new Town {Name = "Aydemir" },
                new Town {Name = "Kostenets" },
                new Town {Name = "Tvarditsa" },
                new Town {Name = "Straldzha" },
                new Town {Name = "Varshets" },
                new Town {Name = "Tsarevo" },
                new Town {Name = "Kotel" },
                new Town {Name = "Lozen" },
                new Town {Name = "Yakoruda" },
                new Town {Name = "Kameno" },
                new Town {Name = "Belogradchik" },
                new Town {Name = "Elena" },
                new Town {Name = "Vetovo" },
                new Town {Name = "Topolovgrad" },
                new Town {Name = "Bobov Dol" },
                new Town {Name = "Strazhitsa" },
                new Town {Name = "Riltsi" },
                new Town {Name = "Benkovski" },
                new Town {Name = "Oryahovo" },
                new Town {Name = "Chepelare" },
                new Town {Name = "Suvorovo" },
                new Town {Name = "Perushtitsa" },
                new Town {Name = "Zlatitsa" },
                new Town {Name = "Yablanovo" },
                new Town {Name = "Bozhurishte" },
                new Town {Name = "Draginovo" },
                new Town {Name = "Bistritsa" },
                new Town {Name = "Polski Trambesh" },
                new Town {Name = "Dalgopol" },
                new Town {Name = "Rozino" },
                new Town {Name = "Sozopol" },
                new Town {Name = "Dolna Banya" },
                new Town {Name = "Logodazh" },
                new Town {Name = "Koynare" },
                new Town {Name = "Trastenik" },
                new Town {Name = "Dolni Dabnik" },
                new Town {Name = "Kazichene" },
                new Town {Name = "Dve Mogili" },
                new Town {Name = "Kostandovo" },
                new Town {Name = "Ignatievo" },
                new Town {Name = "Kalipetrovo" },
                new Town {Name = "Shivachevo" },
                new Town {Name = "Varbitsa" },
                new Town {Name = "Pravets" },
                new Town {Name = "Nikopol" },
                new Town {Name = "Nedelino" },
                new Town {Name = "Slavyanovo" },
                new Town {Name = "Strelcha" },
                new Town {Name = "Glodzhevo" },
                new Town {Name = "Godech" },
                new Town {Name = "Bukovlak" },
                new Town {Name = "Letnitsa" },
                new Town {Name = "Bratsigovo" },
                new Town {Name = "Malo Konare" },
                new Town {Name = "Sapareva Banya" }
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
               {
                   if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                   {
                       return;
                   }

                   var role = new IdentityRole { Name = AdministratorRoleName };

                   await roleManager.CreateAsync(role);

                   const string adminEmail = "admin@knizhar.com";
                   const string adminPassword = "admin12";

                   var user = new User
                   {
                       Email = adminEmail,
                       UserName = adminEmail,
                       FullName = "Admin"
                   };

                   await userManager.CreateAsync(user, adminPassword);

                   await userManager.AddToRoleAsync(user, role.Name);
               })
                .GetAwaiter()
                .GetResult();
        }

        //private static void SeedKnizhari(IServiceProvider services)
        //{
        //    var data = services.GetRequiredService<KnizharDbContext>();

        //    if (data.Users.Any())
        //    {
        //        return;
        //    }

        //    data.Users.AddRange(new[]
        //    {

        //        new User{FullName = "Ivan Dimitrov"},
        //        new User{FullName = "Galya Velikova"},
        //        new User{FullName = "Mariya Ivanova"}
        //    });

        //    data.SaveChanges();

        //    data.Knizhari.AddRange(new[]
        //    {
        //        new Knizhar{UserName = "Galya", TownId = 112},
        //        new Knizhar{UserName = "Ivan", TownId = 5},
        //        new Knizhar{UserName = "Mariya", TownId = 10},
        //    });

        //    data.SaveChanges();
        //}
        //private static void Books(IServiceProvider services)
        //{
        //    var data = services.GetRequiredService<KnizharDbContext>();

        //    if (data.Books.Any())
        //    {
        //        return;
        //    }

        //    data.Books.AddRange(new[]
        //    {
        //        new Book
        //        {
        //            Isbn = "1234567891234",
        //            Name = "Under the Yoke",
        //            GenreId = 15,
        //            LanguageId = 2,
        //            ConditionId = 4,
        //            Description = "Under the Yoke (Bulgarian: Под игото - Pod Igoto), with subtitle A Romance of Bulgarian Liberty[1] is a historical novel by Bulgarian author Ivan Vazov written in 1887-1888 and published in parts 1889–1890 in a magazine The Collection of Folk Tales and in a single book 1894.[2][3] It is set in a small town in Central Bulgaria during the months leading up to the April Uprising in 1876 and is the most famous piece of classic Bulgarian literature. Under the Yoke has been translated into more than 30 languages. The English translation was made in 1894 by William Morfill and published by the London publishing house William Heinemann.",
        //            Author = new Author{ Name = "Ivan Vazov"},
        //            Comment = "No comments",
        //            IsForGiveAway = false,
        //            Price = 10,
        //            AddedOn = DateTime.UtcNow,
        //            Favourite = false,
        //            IsArchived = false,
        //            KnizharId = 1,
        //        },
        //        new Book
        //        {
        //            Isbn = "12345678912",
        //            Name = "The Colour of Magic",
        //            GenreId = 16,
        //            LanguageId = 1,
        //            ConditionId = 3,
        //            Description = "The Colour of Magic is a 1983 fantasy comedy novel by Terry Pratchett, and is the first book of the Discworld series. The first printing of the British edition consisted of only 506 copies.[1] Pratchett has described it as an attempt to do for the classical fantasy universe what Blazing Saddles did for Westerns.",
        //            Author = new Author{ Name = "Terry Pratchett"},
        //            Image = new Image
        //            {
        //                AddedByKnizharId = 1,
        //                Extension = "jpg",

        //            },
        //            Comment = "No comments",
        //            IsForGiveAway = false,
        //            Price = 35,
        //            AddedOn = DateTime.UtcNow,
        //            Favourite = false,
        //            IsArchived = false,
        //            KnizharId = 1,
        //        },
        //          new Book
        //        {
        //            Isbn = "1234567895678",
        //            Name = "East of Eaden",
        //            GenreId = 1,
        //            LanguageId = 1,
        //            ConditionId = 1,
        //            Description = "East of Eden is a novel by American author and Nobel Prize winner John Steinbeck. Published in September 1952, the work is regarded by many to be Steinbeck's most ambitious novel and by Steinbeck himself to be his magnum opus.[2] Steinbeck stated about East of Eden: It has everything in it I have been able to learn about my craft or profession in all these years,' and later said: 'I think everything else I have written has been, in a sense, practice for this.The novel was originally addressed to Steinbecks young sons, Thom and John (then 6½ and 4½ years old, respectively). Steinbeck wanted to describe the Salinas Valley for them in detail: the sights, sounds, smells and colors.",
        //            Author = new Author{ Name = "John Steinbeck"},
        //            Comment = "No comments",
        //            IsForGiveAway = false,
        //            Price = (decimal)9.80,
        //            AddedOn = DateTime.UtcNow,
        //            Favourite = false,
        //            IsArchived = false,
        //            KnizharId = 2,
        //        },
        //          new Book
        //        {
        //            Isbn = "12345678956",
        //            Name = "Jane Eyre",
        //            GenreId = 1,
        //            LanguageId = 1,
        //            ConditionId = 2,
        //            Description = "Jane Eyre /ɛər/ (originally published as Jane Eyre: An Autobiography) is a novel by English writer Charlotte Brontë, published under the pen name 'Currer Bell', on 16 October 1847, by Smith, Elder & Co. of London. The first American edition was published the following year by Harper & Brothers of New York.[1] Jane Eyre is a Bildungsroman which follows the experiences of its eponymous heroine, including her growth to adulthood and her love for Mr. Rochester, the brooding master of Thornfield Hall.",
        //            Author = new Author{ Name = "Charlotte Bronte"},
        //            Comment = "Some underlining with pencil in the book.",
        //            IsForGiveAway = true,
        //            Price = 0,
        //            AddedOn = DateTime.UtcNow,
        //            Favourite = false,
        //            IsArchived = false,
        //            KnizharId = 2,
        //        },
        //           new Book
        //        {
        //            Isbn = "1234567897845",
        //            Name = "Pride and Prejudice",
        //            GenreId = 1,
        //            LanguageId = 1,
        //            ConditionId = 3,
        //            Description = "Pride and Prejudice is an 1813 romantic novel of manners written by Jane Austen. The novel follows the character development of Elizabeth Bennet, the dynamic protagonist of the book who learns about the repercussions of hasty judgments and comes to appreciate the difference between superficial goodness and actual goodness. Its humour lies in its honest depiction of manners, education, marriage, and money during the Regency era in England.",
        //            AuthorId = 8,
        //            Comment = "The covers are not in perfect condition.",
        //            IsForGiveAway = false,
        //            Price = 15,
        //            AddedOn = DateTime.UtcNow,
        //            Favourite = false,
        //            IsArchived = false,
        //            KnizharId = 2,
        //        },
        //           new Book
        //        {
        //            Isbn = "1234567895",
        //            Name = "World Art: The Essential Illustrated History",
        //            GenreId = 2,
        //            LanguageId = 1,
        //            ConditionId = 1,
        //            Description = "World Art offers the perfect introduction to art and its appreciation, with over 350 full-colour illustrations of works by popular and important artists from all over the world. Organized by era, the reader is taken from Giotto, through Monet, to Pollock to reveal the development of art over the centuries. Supplemented with sections on Art Movements and Painting Techniques, this is the definitive reference for art enthusiasts of any level of knowledge and understanding.",
        //            Author = new Author{ Name = "Karen Firzpatrick"},
        //            Comment = "Some pages are thorn but no text is missing.",
        //            IsForGiveAway = true,
        //            Price = 0,
        //            AddedOn = DateTime.UtcNow,
        //            Favourite = false,
        //            IsArchived = false,
        //            KnizharId = 3,
        //        }
        //    });
        //}
    }
}
