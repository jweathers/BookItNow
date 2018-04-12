using System;
using System.Collections.Generic;
using System.Linq;
namespace BookItNow.Shared.EventSourcing

{
    public abstract class AggregateRoot:IEquatable<AggregateRoot>
    {
        public string Id{get;set;}
        public long Version{get;private set;}
        public DateTime LastUpdatedUTC{get;private set;}
        public bool ShouldBeSaved{get;private set;}
        public IEnumerable<EventBase> EventsToSave => eventsToSave.OrderBy(e=>e.Version).AsEnumerable();
        private readonly HashSet<EventBase> eventsToSave;
        protected AggregateRoot()
        {
            eventsToSave=new HashSet<EventBase>();
        }
        public void Apply<T>(T @event) where T:EventBase
        {
            if(@event==null) throw new ArgumentNullException(nameof(@event));

            ((dynamic)this).Handle((dynamic)@event);
            this.Version=@event.Version;
            this.LastUpdatedUTC=@event.OccuredUTC;

        }
        protected void RaiseEvent<T>(Func<T> eventFactory) where T:EventBase
        {
            if(eventFactory==null) throw new ArgumentNullException(nameof(eventFactory));

            var e = eventFactory();
            RaiseEvent(e);
        }
        protected void RaiseEvent<T>(Action<T> eventConfigurer) where T:EventBase,new()
        {
            if(eventConfigurer==null) throw new ArgumentNullException(nameof(eventConfigurer));

            var e = new T();
            eventConfigurer(e);
        }

        protected void RaiseEvent<T>(T @event) where T:EventBase
        {
            if(@event==null) throw new ArgumentNullException(nameof(@event));

            
            @event.Version=this.Version+1;
            @event.OccuredUTC=DateTime.UtcNow;
            Apply(@event);
            ShouldBeSaved=true;
            eventsToSave.Add(@event);
        }
        
        public override bool Equals(object obj)
        {
           return Equals(obj as AggregateRoot);
        }

        public bool Equals(AggregateRoot other)
        {
            if(other is default) return false;

            return this.Id.Equals(other.Id,StringComparison.OrdinalIgnoreCase);
        }        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return this.Id.GetHashCode();
        }
    }
}
