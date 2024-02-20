using Meets.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Meets.Data.Services
{
    public class MeetingService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public MeetingService(IDbContextFactory<ApplicationDbContext> dbContextFactory) 
        { 
            _dbContextFactory = dbContextFactory;
        }
        public List<Meeting>? GetMeetingsList()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<Meeting>? meetings = context.Meetings.ToList();
                return meetings;
            }
        }
        public async Task<List<Meeting>?> GetMeetingsListAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<Meeting>? meetings = await context.Meetings.ToListAsync();
                return meetings;
            }
        }
        public List<Meeting>? GetMeetingsList(Func<Meeting, bool> func)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<Meeting>? meetings = context.Meetings.Where(func).ToList();
                return meetings;
            }
        }
        public async Task<List<Meeting>?> GetMeetingsListAsync(Expression<Func<Meeting, bool>> func)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<Meeting>? meetings = await context.Meetings.Where(func).ToListAsync();
                return meetings;
            }
        }
        public List<Meeting>? GetWholeMeetingsList(Func<Meeting, bool> func)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<Meeting>? meetings = context.Meetings
                    .Include(m => m.ApplicationUserMeetings)
                    .Where(func)
                    .ToList();
                return meetings;
            }
        }
        public async Task<List<Meeting>?> GetWholeMeetingsListAsync(Expression<Func<Meeting, bool>> func)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<Meeting>? meetings = await context.Meetings
                    .Include(m => m.ApplicationUserMeetings)
                    .Where(func)
                    .ToListAsync();
                return meetings;
            }
        }

        public void CreateMeeting(Meeting meeting)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Meetings.Add(meeting);
                context.SaveChanges();
            }
        }
        public async Task CreateMeetingAsync(Meeting meeting)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                await context.Meetings.AddAsync(meeting);
                await context.SaveChangesAsync();
            }
        }
        public void CreateMeeting(Meeting meeting, ApplicationUser promoter)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUserMeeting userMeeting = new(promoter, meeting, MeetingRoleType.Promoter, MeetingJoiningStatus.Accepted);
                meeting.ApplicationUserMeetings.Add(userMeeting);
                context.Meetings.Add(meeting);
                context.SaveChanges();
            }
        }
        public async Task CreateMeetingAsync(Meeting meeting, ApplicationUser promoter)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUserMeeting userMeeting = new(promoter, meeting, MeetingRoleType.Promoter, MeetingJoiningStatus.Accepted);
                meeting.ApplicationUserMeetings.Add(userMeeting);
                await context.Meetings.AddAsync(meeting);
                await context.SaveChangesAsync();
            }
        }

        public void AddUserToMeeting(ApplicationUser user, Meeting meeting, MeetingRoleType role, bool invited) 
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUserMeeting userMeeting = new(user, meeting, role, MeetingJoiningStatus.ToAccept, invited);
                context.ApplicationUserMeetings.Add(userMeeting);
                context.SaveChanges();
            }
        }
        public async Task AddUserToMeetingAsync(ApplicationUser user, Meeting meeting, MeetingRoleType role, bool invited)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUserMeeting userMeeting = new(user, meeting, role, MeetingJoiningStatus.ToAccept, invited);
                await context.ApplicationUserMeetings.AddAsync(userMeeting);
                await context.SaveChangesAsync();
            }
        }
        public Meeting? GetMeeting(Func<Meeting, bool> func)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                Meeting? meeting = context.Meetings.SingleOrDefault(func);
                return meeting;
            }
        }
        public async Task<Meeting?> GetMeetingAsync(Expression<Func<Meeting, bool>> func)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                Meeting? meeting = await context.Meetings.SingleOrDefaultAsync(func);
                return meeting;
            }
        }
        public async Task UpdateMeetingAsync(Meeting meetingToBe)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                Meeting meetingTemp = await context.Meetings.SingleAsync(m => m.Id == meetingToBe.Id);
                meetingTemp.Title = meetingToBe.Title;
                meetingTemp.Description = meetingToBe.Description;
                meetingTemp.StartDate = meetingToBe.StartDate;
                meetingTemp.EndDate = meetingToBe.EndDate;
                meetingTemp.UpdateDate = DateTime.Now;
                context.Entry(meetingTemp).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteMeetingAsync(Meeting meeting)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            { 
                context.Meetings.Remove(meeting);
                await context.SaveChangesAsync();
            }
        }
        public async Task RemoveFromMeetingAsync(Meeting meeting, ApplicationUser participant)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUserMeeting userMeeting = await context.ApplicationUserMeetings.SingleOrDefaultAsync(aum => aum.ApplicationUserId == participant.Id && aum.MeetingId == meeting.Id && aum.Role != MeetingRoleType.Promoter);
                context.ApplicationUserMeetings.Remove(userMeeting);
                await context.SaveChangesAsync();
            }
        }
        public async Task JoinToMeetingAsync(Meeting meeting, ApplicationUser user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUserMeeting userMeeting = new(user, meeting, MeetingRoleType.Participant, MeetingJoiningStatus.ToAccept, false);
                await context.ApplicationUserMeetings.AddAsync(userMeeting);
                await context.SaveChangesAsync();
            }
        }
        public async Task AcceptMeetingAsync(Meeting meeting, ApplicationUser user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUserMeeting userMeeting = await context.ApplicationUserMeetings.SingleAsync(aum => aum.MeetingId == meeting.Id && aum.ApplicationUserId == user.Id && aum.JoiningStatus == MeetingJoiningStatus.ToAccept);
                userMeeting.JoiningStatus = MeetingJoiningStatus.Accepted;
                await context.SaveChangesAsync();
            }
        }
        public async Task ExitMeetingAsync(Meeting meeting, ApplicationUser user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUserMeeting userMeeting = await context.ApplicationUserMeetings.SingleAsync(aum => aum.MeetingId == meeting.Id && aum.ApplicationUserId == user.Id);
                //userMeeting.JoiningStatus = MeetingJoiningStatus.Rejected;*/
                //ApplicationUserMeeting userMeeting = new(user, meeting);
                context.Remove(userMeeting);
                await context.SaveChangesAsync();
            }
        }
    }
}
