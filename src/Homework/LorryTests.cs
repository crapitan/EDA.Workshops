namespace Homework.First.Test
{
    [Fact]
        public void ThreeFailedAttemptsLast16MinutesSucceceeds()
        {
            var app = new App(() => DateTime.Parse("2020-06-01 13:30"));
            var time = DateTime.Parse("2020-06-01 13:15");

            //Given
            app.Given(new[] {
               
            });

            //When
            app.Handle(new LoginCommand());
        }

        [Fact]
        public void ThreeFailedAttemptsLast15minutesThrows()
        {
            var app = new App(() => DateTime.Parse("2020-06-01 13:30"));
            var time = DateTime.Parse("2020-06-01 13:15");

            //Given
            app.Given(new[] {
             
            });

            //When
            Assert.Throws<AuthenticationException>(
                () => app.Handle(new LoginCommand())
            );
        }


}