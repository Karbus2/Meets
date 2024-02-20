using Microsoft.AspNetCore.Identity;

namespace Meets.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<Meeting> Meetings { get; set; }
        public List<ApplicationUserMeeting> ApplicationUserMeetings { get; set; }
        public List<Invitation> InvitationsSent { get; set; }
        public List<Invitation> InvitationsReceived { get; set; }
        public List<Friend> Friends { get; }
        public byte[] Version { get; set; }
        public ApplicationUser()
        {
            Friends = new();
        }
    }
    public class ApplicationUserPublicDTO()
    {
        public string UserName { get; set; }
        private List<Meeting> _meetings;
        public List<Meeting> Meetings
        {
            get
            {
                return _meetings;
            }
            set
            {
                if (value.Any(m => m.PrivacyPolicy == MeetingPrivacyPolicy.Private))
                {
                    throw new InvalidOperationException("Zbiór zawiera spotkanie prywatne.");
                }
                _meetings = value;
            }
        }
        public List<ApplicationUserMeeting> ApplicationUserMeetings { get; set; }
    }
}
