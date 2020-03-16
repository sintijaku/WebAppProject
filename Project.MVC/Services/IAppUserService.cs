using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Project.MVC.Services
{
    public interface IAppUserService
    {
        IEnumerable<AppUser> GetAll();
        AppUser GetById(int id);
        AppUser Add(AppUser user);
        void Delete(int id);
        AppUser Update(AppUser changedUser);
        AppUser FirstOrDefault(Expression<Func<AppUser, bool>> exp);
        bool Any(int id);
        bool UsernameExisits(AppUser user);
        AppUser GetMyUser(string username);

    }
}
