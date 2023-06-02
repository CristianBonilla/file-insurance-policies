using Autofac;
using Vehicle.InsurancePolicies.API.Extensions;
using Vehicle.InsurancePolicies.API.Modules;
using Vehicle.InsurancePolicies.API.Options;

namespace Vehicle.InsurancePolicies.API
{
  public class Startup
  {
    readonly IConfiguration _configuration;
    readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      _configuration = configuration;
      _env = env;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.InstallServicesFromAssembly(_configuration, _env);
    }

    // Register your own things directly with Autofac here.
    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterModule<MongoDomainModule>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();

        SwaggerOptions swagger = _configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
        app.UseSwagger(options => options.RouteTemplate = swagger.JsonRoute);
        app.UseSwaggerUI(options => options.SwaggerEndpoint(swagger.UIEndpoint, swagger.Description));
      }

      app.UseCors(CommonValues.AllowOrigins);

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
