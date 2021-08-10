namespace Knizhar.Test.Routing
{
    using Knizhar.Controllers;
    using Knizhar.Models.Knizhari;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class KnizhariControllerTest
    {
        [Fact]
        public void GetCreateRouteShouldBeMapped()
         => MyRouting
                .Configuration()
                .ShouldMap("/Knizhari/Create")
                .To<KnizhariController>(c => c.Create());

        [Fact]
        public void PostCreateShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Knizhari/Create")
                    .WithMethod(HttpMethod.Post))
                .To<KnizhariController>(c => c.Create(With.Any<BecomeKnizharFormModel>()));
                
    }
}
