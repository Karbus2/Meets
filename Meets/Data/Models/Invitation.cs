namespace Meets.Data.Models
{
    public class Invitation
    {
        public string RequesterId { get; set; }
        public string AddresseeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] Version { get; set; }
        public Invitation() 
        { 
        
        }
        public Invitation(ApplicationUser requester, ApplicationUser addressee)
        {
            RequesterId = requester.Id;
            AddresseeId = addressee.Id;
        }
        public Invitation(string requesterId, string addresseeId)
        {
            RequesterId = requesterId;
            AddresseeId = addresseeId;
        }
    }
}
