using System;
using CommonServiceLocator.NinjectAdapter.Unofficial;
using Microsoft.Practices.ServiceLocation;
using MyWallet.Infra.CrossCutting.IoC.Modules;
using Ninject;

namespace MyWallet.Infra.CrossCutting.IoC
{
    public class IoC
    {
        public IKernel Kernel { get; }

        public IoC()
        {
            Kernel = GetNinjectModules();
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(Kernel));
        }

        private static StandardKernel GetNinjectModules() => new StandardKernel(new RepositoryNinjectModule(),new ApplicationNinjectModule());
    }
}
