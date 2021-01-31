using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.Tests
{
    public class GoodsShippedEvent : IEvent
    {
        public string SourceId => "shipping.order";

        public IDictionary<string, string> Meta { get; set; }
    }

    public class PaymentRecievedEvent : IEvent
    {
        public string SourceId => "payment.order";
        public IDictionary<string, string> Meta { get; set; }
    }

    public class GoodsPickedEvent : IEvent
    {
        public string SourceId => "warehouse.order";

        public IDictionary<string, string> Meta { get; set; }
    }

    public class CompletePaymentCommand : ICommand
    { }
    public class CompletePackingCommand : ICommand
    { }

    public class PaymentCompleteEvent : IEvent
    {
        public string SourceId => "shipping.order";

        public IDictionary<string, string> Meta { get; set; }
    }

    public class PackingCompleteEvent : IEvent
    {
        public string SourceId => "shipping.order";

        public IDictionary<string, string> Meta { get; set; }
    }

    public interface ICommand
    {}

    public interface IEvent
    {
        string SourceId { get; }
        IDictionary<string, string> Meta { get; }
    }
}
