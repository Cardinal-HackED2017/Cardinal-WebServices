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
        public TimeSpan dayStart {get; set;}
        public TimeSpan dayEnd {get; set;}
        public TimeSpan lengthOfMeeting {get; set;}

        public CalendarModel(DateTime start, DateTime end, TimeSpan dayStart, TimeSpan dayEnd, TimeSpan length, List<UserEventModel> events)
        {
            this.startDate = start;
            this.endDate = end;
            this.events = events;
            this.lengthOfMeeting = length;
            gaps = new List<TimeSlotModel>();
            consolidatedEvents = new List<TimeSlotModel>();
        }

        public CalendarModel()
        {
            events = new List<UserEventModel>();
            gaps = new List<TimeSlotModel>();
            consolidatedEvents = new List<TimeSlotModel>();
        }

        public void addEvent(UserEventModel eventToAdd)
        {
            this.events.Add(eventToAdd);
        }

        public void addEvents(IEnumerable<UserEventModel> events)
        {
            this.events.AddRange(events);
        }

        public void consolidateEvents()
        {
            var tempEvents = (events.OrderByDescending(x => x.timeSlot.start)
                                                                    .Select(x => x.timeSlot).ToList());
            
            foreach(TimeSlotModel t in tempEvents)
            {
                if (t.start.TimeOfDay < dayStart)
                {
                    t.start = t.start.Date.Add(dayStart);
                }
                if (t.end().TimeOfDay > dayEnd)
                {
                    t.length = t.length - (t.end().TimeOfDay - dayEnd);
                }
            }
            tempEvents.Reverse();
            bool delta = true;
            
            while(delta)
            {
                consolidatedEvents.Clear();
                consolidatedEvents.AddRange(tempEvents);
                tempEvents.Clear();
                delta = false;
                for (int i = 0; i < consolidatedEvents.Count; i++)
                {

                    var thisEvent = consolidatedEvents[i];
                    Console.WriteLine(thisEvent.ToString());
                    var nextEvent = i + 1 != consolidatedEvents.Count ? consolidatedEvents[i + 1] : null;
                    if (nextEvent != null)
                    {
                        var intersect = TimeSlotModel.intersects(thisEvent, nextEvent);

                        switch (intersect)
                        {
                            case TimeSlotModel.intersections.A_Before_B:
                                Console.WriteLine("A BEFORE B");
                                thisEvent.length = nextEvent.end() - thisEvent.start;
                                tempEvents.Add(thisEvent);
                                i++; //Skip nextevent
                                delta = true;
                                break;
                            case TimeSlotModel.intersections.A_inside_B:
                                Console.WriteLine("A INSIDE B");
                                tempEvents.Add(nextEvent);
                                i++;
                                delta = true;
                                break;
                            case TimeSlotModel.intersections.B_Before_A:
                                Console.WriteLine("B BEFORE A");
                                thisEvent.length = thisEvent.end() - nextEvent.start;
                                thisEvent.start = nextEvent.start;
                                tempEvents.Add(thisEvent);
                                i++; //Skip nextevent
                                delta = true;
                                break;
                            case TimeSlotModel.intersections.B_inside_A:
                                Console.WriteLine("B INSIDE A");
                                tempEvents.Add(thisEvent);
                                i++;
                                delta = true;
                                break;
                            case TimeSlotModel.intersections.No_Intersect:
                                Console.WriteLine("NO INTERSECT");
                                tempEvents.Add(thisEvent);
                                break;
                        }
                    }
                    else if (thisEvent != null)
                    {
                        tempEvents.Add(thisEvent);
                    }
                }
            }
            createGaps();
        }

        private void createGaps()
        {
            for(int i = 0; i < consolidatedEvents.Count; i++)
            {
                TimeSpan length = i+1 < consolidatedEvents.Count ? consolidatedEvents[i+1].start.TimeOfDay - consolidatedEvents[i].end().TimeOfDay : dayEnd - consolidatedEvents[i].end().TimeOfDay;

                if (length > lengthOfMeeting)
                {
                    gaps.Add(new TimeSlotModel
                    {
                        start = consolidatedEvents[i].end(),
                        length = i+1 < consolidatedEvents.Count ? consolidatedEvents[i+1].start.TimeOfDay - consolidatedEvents[i].end().TimeOfDay : dayEnd - consolidatedEvents[i].end().TimeOfDay
                    });
                }
            }
        }
        /*
        public void testConsolidation()
        {
            this.dayStart = new TimeSpan(8, 0, 0);// 8:00 a,
            this.dayEnd = new TimeSpan(18,0,0);
            this.lengthOfMeeting = new TimeSpan(2,0,0);
            TimeSlotModel timeSlottest = new TimeSlotModel{
                start = DateTime.Now,
                length = new TimeSpan(2,0,0) // 2 hours
            };
            events.Add(new UserEventModel{
                name = "test1",
                userId = "lel",
                timeSlot = timeSlottest
            });
             TimeSlotModel timeSlottest2 = new TimeSlotModel{
                start = DateTime.Now,
                length = new TimeSpan(1,0,0) // 2 hours
            };
            events.Add(
                new UserEventModel {
                    name = "test2",
                    userId = ";e;",
                    timeSlot = timeSlottest2
                }
            );
            TimeSlotModel timeSlottest3 = new TimeSlotModel{
                start = DateTime.Now + new TimeSpan(1,0,0),
                length = new TimeSpan(4,0,0) // 2 hours
            };
            events.Add(
                new UserEventModel {
                    name = "test2",
                    userId = ";e;",
                    timeSlot = timeSlottest3
                }
            );

            TimeSlotModel timeSlottest4 = new TimeSlotModel{
                start = DateTime.Now + new TimeSpan(8,0,0),
                length = new TimeSpan(4,0,0) // 2 hours
            };
            events.Add(
                new UserEventModel {
                    name = "test2",
                    userId = ";e;",
                    timeSlot = timeSlottest4
                }
            );
            foreach(UserEventModel t in events)
            {
                Console.WriteLine(t.timeSlot.ToString());
            }
            this.consolidateEvents();
            Console.WriteLine(consolidatedEvents.Count);
            foreach(TimeSlotModel timeslot in consolidatedEvents)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(timeslot.ToString());
            }
            foreach(TimeSlotModel gap in gaps)
            {
                Console.WriteLine("-----------------------GAP------------");
                Console.WriteLine(gap.ToString());
            }
            
        }*/
    }
}