namespace Meets.Data.Models
{
    public enum MeetingRoleType
    {
        Participant,
        Moderator,
        Promoter
    }
    public enum MeetingJoiningStatus
    {
        ToAccept,
        Rejected,
        Accepted
    }
    public class ApplicationUserMeeting
    {
        public string ApplicationUserId { get; set; }
        public string MeetingId { get; set; }
        public MeetingRoleType Role { get; set; }
        public MeetingJoiningStatus JoiningStatus { get; set; }
        public bool Invited { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public byte[] Version { get; set; }

        public ApplicationUserMeeting()
        {
            Invited = false;
        }
        public ApplicationUserMeeting(ApplicationUser user, Meeting meeting) : this()
        {
            ApplicationUserId = user.Id;
            MeetingId = meeting.Id;
        }
        public ApplicationUserMeeting(ApplicationUser user, Meeting meeting, MeetingRoleType role) : this(user, meeting)
        {
            Role = role;
        }
        public ApplicationUserMeeting(ApplicationUser user, Meeting meeting, MeetingRoleType role, MeetingJoiningStatus joiningStatus) : this(user, meeting, role)
        {
            JoiningStatus = joiningStatus;
        }
        public ApplicationUserMeeting(ApplicationUser user, Meeting meeting, MeetingRoleType role, MeetingJoiningStatus joiningStatus, bool invited) : this(user, meeting, role, joiningStatus)
        {
            Invited = invited;
        }
    }
}
