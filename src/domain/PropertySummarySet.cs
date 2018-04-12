using BookItNow.Shared.EventSourcing;

namespace BookItNow.Domain
{
    public class PropertySummarySet:EventBase
    {
        public PropertySummarySet()
        {
        }

        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string[] Tags { get; internal set; }
    }
}