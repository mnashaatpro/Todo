using Healty.Core.Models;
using Healty.Core.Repositories;

using System.Collections.Generic;
using System.Linq;

namespace Healty.Persistence.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IEnumerable<UserNotification> GetUserNotificationsFor(string userId)
        //{
        //    return _context.UserNotifications
        //        .Where(un => un.UserId == userId && !un.IsRead)
        //        .ToList();
        //}
    }
}