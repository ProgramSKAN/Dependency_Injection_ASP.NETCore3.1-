using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services.BookMeetingConfig
{
    public class BookMeetingConfiguration : IBookMeetingConfiguration
    {
        public int MeetingDuration { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
