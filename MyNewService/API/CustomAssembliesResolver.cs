using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dispatcher;

namespace MyNewService.API
{
    public class CustomAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();

            List<Assembly> assemblies = new List<Assembly>(baseAssemblies);

            var controllersAssembly = Assembly.LoadFrom(@"C:\libs\controllers\ControllersLibrary.dll");

            baseAssemblies.Add(controllersAssembly);

            return assemblies;
        }
    }
}
