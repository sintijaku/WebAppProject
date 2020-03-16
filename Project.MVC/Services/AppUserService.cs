using Project.DATA.Context;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Project.MVC.Services
{
    public class AppUserService : IAppUserService
    {
        private MyContext _context;

        public AppUserService(MyContext context)
        {
            _context = context;
        }

        public AppUser Add(AppUser user)
        {
            _context.Add(user);
            Save();
            return user;
        }

        public bool Any(int id)
        {
            return _context.AppUsers.Any(x => x.ID == id);
        }

        public void Delete(int id)
        {
            AppUser appUser = _context.AppUsers.FirstOrDefault(x => x.ID == id);
            if (appUser != null)
            {
                _context.Remove(appUser);
                Save();
            }
        }

        public AppUser FirstOrDefault(Expression<Func<AppUser, bool>> exp)
        {
            return _context.Set<AppUser>().FirstOrDefault(exp);
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _context.AppUsers.ToList();
        }

        public AppUser GetById(int id)
        {
            return _context.AppUsers.Find(id);
        }

        public AppUser GetMyUser(string username)
        {
            AppUser user = _context.AppUsers.FirstOrDefault(x => x.Username == username);
            return user;
        }

        public AppUser Update(AppUser changedUser)
        {
            AppUser user = _context.AppUsers.FirstOrDefault(x => x.ID == changedUser.ID);
            _context.AppUsers.Update(changedUser);
            Save();
            return user;
        }

        public bool UsernameExisits(AppUser user)
        {
            return _context.AppUsers.Any(x => x.Username == user.Username);
        }

        private void Save()
        {
            _context.SaveChanges();
        }
    }
}

