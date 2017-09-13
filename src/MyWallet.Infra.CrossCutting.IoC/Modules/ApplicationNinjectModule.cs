using AutoMapper;
using MyWallet.Application.AppServices;
using MyWallet.Application.Contracts;
using Ninject;
using Ninject.Modules;

namespace MyWallet.Infra.CrossCutting.IoC.Modules
{
    public class ApplicationNinjectModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(ctx =>
            {
                var cfg = new MapperConfiguration(mapconfig =>
                {
                    mapconfig.AddProfile<Application.AutoMapper.AutoMapperProfile>();
                    mapconfig.ConstructServicesUsing(t=> Kernel?.Get(t));
                });
                return cfg.CreateMapper();
            }).InSingletonScope();

            Bind<IUserAppService>().To<UserAppService>();
            Bind<IWalletAppService>().To<WalletAppService>();

        }
    }
}