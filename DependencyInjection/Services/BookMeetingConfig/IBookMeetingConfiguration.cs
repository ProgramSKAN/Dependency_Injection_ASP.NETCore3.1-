namespace DependencyInjection.Services.BookMeetingConfig
{
    public interface IBookMeetingConfiguration
    {
        int MeetingDuration { get; set; }
        int NumberOfPeople { get; set; }
    }
}