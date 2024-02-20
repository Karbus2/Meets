namespace Meets.Data.Models
{
    public class MeetingUserSearch
    {
        public string? UserInput { get; set; }
        public bool SearchType { get; set; } // 1->znajomi 0->spotkania
        /*private bool _friends;
        public bool Friends
        {
            get
            {
                return _friends;
            }
            set
            {
                if (value == _meetings && !value)
                {
                    _friends = true;
                }
                else
                {
                    _friends = value;
                }
            }
        }
        private bool _meetings;
        public bool Meetings
        {
            get
            {
                return _meetings;
            }
            set
            {
                if (value == _friends && !value)
                {
                    _meetings = true;
                }
                else
                {
                    _meetings = value;
                }
            }
        }*/
        public MeetingUserSearch()
        {
            UserInput = null;
            SearchType = true;
            //Friends = true;
            //Meetings = true;
        }
    }
}
