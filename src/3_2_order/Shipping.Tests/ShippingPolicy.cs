using System;
using System.Collections.Generic;

namespace Shipping.Tests
{
    public class App
    {
        List<IEvent> history = new List<IEvent>();

        public void Given(params IEvent[] events) => history.AddRange(events);

        public void When(IEvent @event)
        {
            history.Add(@event);
            var cmd = ShippingPolicy.When((dynamic)@event);
            var state = history.Rehydrate<Order>();
            history.AddRange(OrderBehavior.Handle(state, (dynamic)cmd));
        }

        public void Then(Action<IEvent[]> f)
            => f(history.ToArray());
    }

    public class ShippingPolicy
    {
        public static ICommand When(PaymentRecievedEvent @event) => new CompletePaymentCommand();
        public static ICommand When(GoodsPickedEvent @event) => new CompletePackingCommand();
    }

    public static class OrderBehavior
    {
        public static IEnumerable<IEvent> Handle(this Order order, CompletePaymentCommand command)
           {
               yield return new PackingCompleteEvent();
               
               if (order.Packed && order.Payed)
               {
                    yield return new GoodsShippedEvent();
               }
           }

        public static IEnumerable<IEvent> Handle(this Order order, CompletePackingCommand command)
            {
               yield return new PackingCompleteEvent();
               
               if (order.Packed && order.Payed)
               {
                    yield return new GoodsShippedEvent();
               }
           }
    }

    public class Order
    {
        public bool Payed;
        public bool Packed;

        public Order When(IEvent @event) 
        {
            return this;
        }

        public Order When(PaymentRecievedEvent @event) 
        {
            this.Payed = true;
            return this;
        }

        public Order When(GoodsPickedEvent @event)
        {
            this.Packed = true;
            return this;
        }

    }
}
