using Application.Mappings;

namespace API.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserMappingProfile));
        }
    }
}
