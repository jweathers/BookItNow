using System;

namespace BookItNow.Shared.EventSourcing

{
    public class AggregateRoot:IEquatable<AggregateRoot>
    {
        public string Id{get;set;}



        
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
