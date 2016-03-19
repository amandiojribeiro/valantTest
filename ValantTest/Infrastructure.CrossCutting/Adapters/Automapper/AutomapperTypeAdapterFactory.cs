namespace ValantTest.Infrastructure.CrossCutting.Adapters.Automapper
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using Extensions;

    public class AutomapperTypeAdapterFactory : ITypeAdapterFactory
    {
        private readonly MapperConfiguration configuration;

        public AutomapperTypeAdapterFactory()
        {
            try
            {
                var profiles = AppDomain.CurrentDomain
                                 .GetAssemblies()
                                 .Where(x => x.FullName.StartsWith("Valant"))
                                 .SelectMany(a => a.GetLoadableTypes())
                                 .Where(t => t.BaseType == typeof(Profile));

                this.configuration = new MapperConfiguration(cfg =>
                {
                    foreach (var item in profiles.Where(item => item.FullName != "AutoMapper.SelfProfiler`2"))
                    {
                        cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                    }
                });
            }
            catch (ReflectionTypeLoadException)
            {
                Debug.Write("Unable to load Automapper Profiles");
                throw;
            }
        }

        public void AssertConfigurationIsValid()
        {
            this.configuration.AssertConfigurationIsValid();
        }

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter(this.configuration);
        }
    }
}
