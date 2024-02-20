using System.ComponentModel.DataAnnotations;

namespace Meets.Data.Models
{
    public enum MeetingPrivacyPolicy
    {
        Private,
        FriendsOnly,
        Public
    }
    public class Meeting
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? MaxSize { get; set; }
        public string Location { get; set; }
        public MeetingPrivacyPolicy PrivacyPolicy { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public List<ApplicationUserMeeting> ApplicationUserMeetings { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public byte[] Version { get; set; }


        public Meeting()
        {
            Id = Guid.NewGuid().ToString();
            StartDate = DateTime.Now;
            PrivacyPolicy = MeetingPrivacyPolicy.Private;
            ApplicationUsers = new();
            ApplicationUserMeetings = new();
        }
        public Meeting(string title,
                       string description,
                       int maxSize,
                       string location,
                       MeetingPrivacyPolicy privacyPolicy,
                       DateTime startDate,
                       DateTime endDate) : this()
        {
            Title = title;
            Description = description;
            MaxSize = maxSize;
            Location = location;
            PrivacyPolicy = privacyPolicy;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
