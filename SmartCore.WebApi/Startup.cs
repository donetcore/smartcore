using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using SmartCore.Middleware;
using Swashbuckle.AspNetCore.Swagger;
namespace SmartCore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers(options =>
            {
                // requires using Microsoft.AspNetCore.Mvc.Formatters;
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                options.Filters.Add(typeof(ValidateModelAttribute));
                options.Filters.Add(typeof(WebApiResultMiddleware));
                options.Filters.Add(typeof(CustomExceptionAttribute));
                options.RespectBrowserAcceptHeader = true;
            }).AddNewtonsoftJson(options =>
            {
                //Ĭ�� JSON ��ʽ��������� System.Text.Json
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                // Configure a custom converter
                //options.SerializerSettings.Converters.Add(new MyCustomJsonConverter());
            });
            #region Authentication
            services.AddTokenAuthentication(Configuration);
            #endregion
            #region HttpClientFactory
            services.AddHttpClient();
            #endregion
            #region Configure Swagger
            services.AddSwaggerGen(c =>
            {
                //Swashbuckle.AspNetCore.Swagger.SwaggerOptions
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
                c.OrderActionsBy(a => a.RelativePath);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";//�ļ�����Դ����Ŀ����==������==�����==��XML�ĵ��ļ�
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // ����xmlע��. �÷����ڶ����������ÿ�������ע�ͣ�Ĭ��Ϊfalse.
                c.IncludeXmlComments(xmlPath, true);
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    //JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//Jwt default param name
                    In=Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //Add authentication type
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use((context, next) =>
            {
                //Do some work here
                context.Response.Headers.Add("X-WorkId", "127.0.0.1");
                //Pass the request on down to the next pipeline (Which is the MVC middleware)
                return next();
            });
            //app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseSecurityMiddleware(); //Nifty encapsulation with the extension

        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            //containerBuilder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().InstancePerDependency();

            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var repositoryAssembly = assemblies.FirstOrDefault(p => p.FullName.EndsWith("Repository"));
            if (repositoryAssembly != null)
            {
                //ָ����ɨ������е�����ע��Ϊ�ṩ������ʵ�ֵĽӿڡ�
                containerBuilder.RegisterAssemblyTypes(repositoryAssembly).Where(t => t.Name.EndsWith("Repository")).
                 AsImplementedInterfaces().InstancePerDependency();

            }
            var servicesAssembly = assemblies.FirstOrDefault(p => p.FullName.EndsWith("Services"));
            if (servicesAssembly != null)
            {
                //ָ����ɨ������е�����ע��Ϊ�ṩ������ʵ�ֵĽӿڡ�
                containerBuilder.RegisterAssemblyTypes(repositoryAssembly).Where(t => t.Name.EndsWith("Services")).
                 AsImplementedInterfaces().InstancePerDependency();
            }
            //  // ������װ�뵽΢��Ĭ�ϵ�����ע��������
            //containerBuilder.Build();
            //Configuration.DependencyResolver= new AutofacWebApiDependencyResolver(container);
            //��̬ע��������CallLogger �������������
            //containerBuilder.Register(c => new CallLogger(Console.Out));
        }
        //public static class SecurityMiddlewareExtensions
        //{
        //    public static IApplicationBuilder UseSecurityMiddleware(this IApplicationBuilder builder)
        //    {
        //        return builder.UseMiddleware<SecurityMiddleware>();
        //    }
        //}
    }
    ///// <summary>
    ///// ������ ��Ҫʵ�� IInterceptor�ӿ� Intercept����
    ///// </summary>
    //public class CallLogger : IInterceptor
    //{
    //    TextWriter _output;

    //    public CallLogger(TextWriter output)
    //    {
    //        _output = output;
    //    }

    //    /// <summary>
    //    /// ���ط��� ��ӡ�����صķ���ִ��ǰ�����ơ������ͷ���ִ�к�� ���ؽ��
    //    /// </summary>
    //    /// <param name="invocation">���������ط�������Ϣ</param>
    //    public void Intercept(IInvocation invocation)
    //    {

    //        _output.WriteLine("�����ڵ��÷��� \"{0}\"  ������ {1}... ",
    //          invocation.Method.Name,
    //          string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

    //        //�ڱ����صķ���ִ����Ϻ� ����ִ��
    //        invocation.Proceed();

    //        _output.WriteLine("����ִ����ϣ����ؽ����{0}", invocation.ReturnValue);
    //    }
    //}
}
