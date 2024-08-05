using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Data;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Angular.CSharp.Training
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // Register your types here
            container.RegisterType<DemoDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<EmployeeAgent>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}