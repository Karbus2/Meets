using Meets.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;

namespace Meets.Data.Services
{
    public class ApplicationUserService
    {
        private IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ApplicationUserService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public ApplicationUser? GetUser(Func<ApplicationUser, bool> func)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUser? user = context.ApplicationUsers.SingleOrDefault(func);
                return user;
            }
        }
        public async Task<ApplicationUser?> GetUserAsync(Expression<Func<ApplicationUser, bool>> func)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUser? user = await context.ApplicationUsers.SingleOrDefaultAsync(func);
                return user;
            }
        }
        public async Task<List<ApplicationUser>?> GetUserListAsync(Expression<Func<ApplicationUser, bool>> func)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<ApplicationUser?> users = await context.ApplicationUsers.Where(func).ToListAsync();
                return users;
            }
        }
        public async Task<List<ApplicationUser>> GetUserListAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<ApplicationUser> users = await context.ApplicationUsers.ToListAsync();
                return users;
            }
        }
        public async Task InviteFriendAsync(ApplicationUser requester, ApplicationUser addressee)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                Invitation invitation = new(requester, addressee);
                await context.Invitations.AddAsync(invitation);
                await context.SaveChangesAsync();
            }
        }
        public async Task AcceptFriendAsync(ApplicationUser requester, ApplicationUser addressee)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                Friend friend = new(requester, addressee);
                await context.Friends.AddAsync(friend);

                friend = new(addressee, requester);
                await context.Friends.AddAsync(friend);

                context.Invitations.Remove(addressee.InvitationsReceived.SingleOrDefault(invr => invr.RequesterId == requester.Id));

                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteInvitationAsync(ApplicationUser userL, ApplicationUser userR)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                Invitation invitation = await context.Invitations.SingleOrDefaultAsync(inv => (inv.RequesterId == userL.Id && inv.AddresseeId == userR.Id) || (inv.RequesterId == userR.Id && inv.AddresseeId == userL.Id));
                context.Invitations.Remove(invitation);

                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteFriendAsync(Friend friend)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                //friend = await context.Friends.SingleOrDefaultAsync(f => f.FriendId == user.Id);
                context.Friends.Remove(friend);

                friend = await context.Friends.SingleOrDefaultAsync(f => f.ApplicationUserId == friend.FriendId);

                context.Friends.Remove(friend);

                await context.SaveChangesAsync();
            }
        }
        public ApplicationUser? GetWholeUser(Func<ApplicationUser, bool> func)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                ApplicationUser? user = context.ApplicationUsers
                    .Include(au => au.InvitationsSent)
                    .Include(au => au.InvitationsReceived)
                    .Include(au => au.ApplicationUserMeetings)
                    .AsSplitQuery()
                    .Include(au => au.Friends)
                    .AsSingleQuery()
                    .ToList()
                    .SingleOrDefault(func);
                return user;
            }
        }
        public async Task<List<ApplicationUser>?> GetUserFriendsAsync(ApplicationUser requester)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                List<Friend> friends = requester.Friends;
                List<ApplicationUser>? friendUsers = null;
                friendUsers = await context.ApplicationUsers
                    .Include(au => au.ApplicationUserMeetings)
                    .Where(au => friends.Any(f => f.FriendId == au.Id))
                    .ToListAsync();
                return friendUsers;
            }
        }
        public List<ApplicationUser>? GetUserFriends(ApplicationUser user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            { 
                List<ApplicationUser>? friendUsers = new();
                foreach (var friend in user.Friends)
                {
                    friendUsers.Add(context.ApplicationUsers.Include(au => au.ApplicationUserMeetings).SingleOrDefault(au => au.Id == friend.FriendId));
                }
                return friendUsers;
            }
        }
    }
}
