using MyWallet.Infra.Data.Contracts;
using MyWallet.Infra.Data.Repository;
using MyWallet.Infra.Data.Repository.Shared;
using Ninject.Modules;

namespace MyWallet.Infra.CrossCutting.IoC.Modules
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Bind<IWalletRepository>().To<WalletRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}