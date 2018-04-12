using System;
namespace BookItNow.Shared.EventSourcing
{
    public interface IEvent
    {
        string AggregateId{get;}
        long Version{get;}
        DateTime OccuredUTC{get;}
    }

    public class EventBase:IEvent
    {
        public string AggregateId{get;set;}
        public long Version{get;set;}
        public DateTime OccuredUTC{get;set;}
    }
}