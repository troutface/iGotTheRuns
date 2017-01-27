[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(iGotTheRuns.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(iGotTheRuns.App_Start.NinjectWebCommon), "Stop")]

namespace iGotTheRuns.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using iGotTheRuns.Services;
    using iGotTheRuns.Data;
    using System.Web.Http;
    using WebApiContrib.IoC.Ninject;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
#if DEBUG
            kernel.Bind<IMailService>().To<MockMailService>().InRequestScope();
#else
            kernel.Bind<IMailService>().To<MailService>().InRequestScope();
            //we use structuremap to accomplish this within the registries of Line Review 
            //structuremap uses "scan.WithDefaultConventions();" OR MAYBE "scan.With(new ControllerConvention());" to accomplish the same thing.
            //look at DefaultRegistry.cs in the IoC folder in LineReview.
            //in StructureMap, you only have to do it once compared to every service apparently in Ninject
#endif

            kernel.Bind<MessageBoardContext>().To<MessageBoardContext>().InRequestScope();
            kernel.Bind<IMessageBoardRepository>().To<MessageBoardRepository>().InRequestScope();
            //InRequestScope: anyone within a single request to the server will get the same instance of the repository.
            // prevents recreating the objects the repo needs.


        }        
    }
}
