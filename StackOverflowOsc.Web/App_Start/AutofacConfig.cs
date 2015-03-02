using Autofac;
using AutoMapper;

namespace StackOverflowOsc.Web
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => Mapper.Engine).As<MappingEngine>();
            //builder.Build();
        }
    }
}