namespace Knizhar.Infrastructure
{
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<KnizharDbContext>();

            data.Database.Migrate();

            SeedGenres(data);
            SeedLanguages(data);
            SeedCondition(data);
            SeedTowns(data);

            return app;
        }

        private static void SeedGenres(KnizharDbContext data)
        {
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

        private static void SeedLanguages(KnizharDbContext data)
        {
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

        private static void SeedCondition(KnizharDbContext data)
        {
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

        private static void SeedTowns(KnizharDbContext data)
        {
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
    }
}
