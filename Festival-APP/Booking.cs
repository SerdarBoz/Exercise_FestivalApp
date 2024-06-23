using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Festival_APP
{
    public class BookingException : Exception
    {
        public BookingException(string message) : base(message)
        {
        }
    }
    internal class Booking
    {
        private int start;
        private int end;
        private List <string> RiderItem;
        public int Id { get; }
        public string GroupName { get; set; }
        public int StartHour
        {
            get
            {
                return start;
            }
            set
            {
                if (value < 0 || value > 23) 
                {
                    throw new BookingException("Startuur mag niet lager zijn dan 0 of hoger dan 23");
                }
                else 
                { 
                    start = value;
                }
            }
        }
        public int EndHour {
            get
            {
                return end;
            }
            set
            {
                if(value <= start || value < 0 || value > 23) 
                {
                    throw new BookingException("Het einduur moet hoger zijn dan 0 en lager zijn dan 23 en mag niet voor het startuur zijn");
                } 
                else 
                {
                        end = value;
                }
                
            }    
        }
        public int Duration { 
            get
            {
                return (EndHour - StartHour);
            }
        }
        public Booking(int id, string groupName, int startHour, int endHour) 
        {
            this.Id = id;
            this.GroupName = groupName;
            this.StartHour = startHour;
            this.EndHour = endHour;
            this.RiderItem = new List<string>();
        }
        public List<string> RiderItems {
            get
            {
                return RiderItem;
            }
        }
        public void addRiderItem(string riderItem) 
        {
            RiderItem.Add(riderItem);
        }
        public bool conflictsWith(Booking booking) 
        {
            if(this.StartHour < booking.EndHour && this.EndHour > booking.StartHour) 
            {
                return true;
            } else 
            { 
                return false; 
            }
        }
    }
}
