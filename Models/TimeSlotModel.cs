using System;

namespace cardinal_webservices.Models
{
    public class TimeSlotModel
    {
        public enum intersections
        {
            No_Intersect,
            A_Before_B,
            B_Before_A,
            A_inside_B,
            B_inside_A
        }
        public DateTime start {get; set;}
        public TimeSpan length {get; set;}

        public DateTime end()
        {
            return start.Add(length);
        }

        public string ToString()
        {
            return this.start.ToString() + this.length.ToString();
        }

        public static intersections intersects(TimeSlotModel a, TimeSlotModel b)
        {
            if(a.start == a.end() || b.start == b.end())
            {
                return intersections.No_Intersect;
            }

            if (a.start <= b.start)
            {
                if (a.end() >= b.start && a.end() <= b.end())
                {
                    return intersections.A_Before_B;
                }
                if (a.end() >= b.end())
                {
                    return intersections.B_inside_A;
                }
            }
            else
            {
                if (b.end() >= a.start && b.end() <= a.end())
                {
                    return intersections.B_Before_A;
                }
                if (b.end() >= a.end())
                {
                    return intersections.A_inside_B;
                }
            }
            return intersections.No_Intersect;
        }
    }

    public class InvalidTimeSlotException : Exception
    {}
}