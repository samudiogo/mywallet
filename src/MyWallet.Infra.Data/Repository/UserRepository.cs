using System.Data.Entity;
using System.Threading.Tasks;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.Repository.Shared;

namespace MyWallet.Infra.Data.Repository
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(MyWalletContext context) : base(context)
        {
        }

        public async Task<User> FindByTokenAsync(string token)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Token.Equals(token));
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}