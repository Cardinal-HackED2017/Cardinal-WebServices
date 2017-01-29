using System;
using System.Collections.Generic;
using System.Linq;

namespace cardinal_webservices.Models
{
    public class CalendarModel
    {
        public List<UserEventModel> events {get; set;}
        public List<TimeSlotModel> gaps {get;}
        private List<TimeSlotModel> consolidatedEvents;

        public DateTime startDate {get; set;}
        public DateTime endDate {get; set;}

        public CalendarModel(DateTime start, DateTime end, List<UserEventModel> events)
        {
            this.startDate = start;
            this.endDate = end;
            this.events = events;
        }

        public void addEvent(UserEventModel eventToAdd)
        {
            this.events.Add(eventToAdd);
        }

        public void addEvents(IEnumerable<UserEventModel> events)
        {
            this.events.AddRange(events);
        }

        public void computeGaps()
        {
            consolidatedEvents = (List<TimeSlotModel>)this.events.OrderByDescending(x => x.timeSlot.start)
                                          .Select(x => x.timeSlot);

            var tempEvents = new List<TimeSlotModel>();
            bool delta = true;
            while(delta)
            {
                delta = false;
                for (int i = 0; i < consolidatedEvents.Count; i++)
                {
                    var thisEvent = consolidatedEvents[i];
                    var nextEvent = consolidatedEvents[i + 1];
                    if (thisEvent.end() > nextEvent.start)
                    {
                        delta = true;
                        thisEvent.length = nextEvent.end() - thisEvent.start;
                        i++; //Skip the consolidated event
                    }
                    tempEvents.Add(thisEvent);
                }
                consolidatedEvents.Clear();
                consolidatedEvents.AddRange(tempEvents);
                tempEvents.Clear();
            }
            
        }
    }
}