using System;
using BookItNow.Shared.EventSourcing;
namespace BookItNow.Domain
{
    public class Property:AggregateRoot
    {
        public Property():base()
        {
            this.Id=Guid.NewGuid().ToString("N");
        }
        public string Name{get;set;}
        public string Description{get;set;}
        public string[] Tags{get;set;}
        public void SetPropertySummary(string name, string description, string[] tags)
        {
            RaiseEvent<PropertySummarySet>(e=>{
                e.Name=name;
                e.Description=description;
                e.Tags=tags;
            });
        }
        protected void Handle(PropertySummarySet @event)
        {
            this.Name=@event.Name;
            this.Description=@event.Description;
            this.Tags=@event.Tags;
        }
    }
}
