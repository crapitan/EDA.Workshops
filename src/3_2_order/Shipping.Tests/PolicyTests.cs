using System;
using System.Linq;
using Xunit;

namespace Shipping.Tests
{
    public class PolicyTests
    {
        [Fact]
        public void PickedDoesntShip()
        {
            var app = new App();

            //Given
            app.Given(Array.Empty<IEvent>());

            //When
            app.When(new GoodsPickedEvent());

            //Then
            app.Then(events => Assert.False(events.OfType<GoodsShippedEvent>().Any()));
        }

        [Fact]
        public void PayedAndPickedShip()
        {
            var app = new App();

            //Given
            app.Given(new PaymentRecievedEvent());

            //When
            app.When(new GoodsPickedEvent());

            //Then
            app.Then(events => Assert.True(events.OfType<GoodsShippedEvent>().Any()));
        }

        [Fact]
        public void PickedAndPayedIssueShip()
        {
            var app = new App();

            //Given
            app.Given(new GoodsPickedEvent());

            //When
            app.When(new PaymentRecievedEvent());

            //Then
            app.Then(events => Assert.True(events.OfType<GoodsShippedEvent>().Any()));
         }
    }
}
