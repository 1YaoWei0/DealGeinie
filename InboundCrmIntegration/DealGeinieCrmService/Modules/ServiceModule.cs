using Autofac;
using DealGeinieCrmService.Services;

namespace DealGeinieCrmService.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CrmAnnotationService>().As<ICrmService>();
            builder.RegisterType<CrmTxtAnnotationService>().As<ICrmService>();
        }
    }
}