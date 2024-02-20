namespace Meets.Data.Models
{
    public class Friend
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string FriendId { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] Version { get; set; }
        public Friend()
        {
           
        }
        public Friend(ApplicationUser user, ApplicationUser friend)
        {
            ApplicationUserId = user.Id;
            FriendId = friend.Id;
        }
        public Friend(string userId, string friendId)
        {
            ApplicationUserId = userId;
            FriendId = friendId;
        }
    }
}
