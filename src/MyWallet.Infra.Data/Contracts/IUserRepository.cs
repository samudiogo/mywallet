using System.Threading.Tasks;
using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Infra.Data.Contracts
{
    public interface IUserRepository : IRepository<UserDataModel>
    {
        Task<UserDataModel> FindByTokenAsync(string token);
        Task<UserDataModel> FindByEmailAsync(string email);
    }
}