using System;
using System.Threading.Tasks;
using MyWallet.Domain.Models;

namespace MyWallet.Infra.Data.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByTokenIdAsync(string token, Guid id);
        Task<User> FindByEmailAsync(string email);
    }
}