using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using SmartCore.ConfigCenter.Apollo;

namespace SmartCore.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            SmartCore.Infrastructure.LogManager.Info("Current Thead Id:{0}", Thread.CurrentThread.ManagedThreadId);
            //logger.Info(string.Format("Current Thead Id:{0}", Thread.CurrentThread.ManagedThreadId));
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        ///WebHost �� ����WebӦ�õ����� ������ΪӦ��������WebHost��WebHostBuilder ��WebHost�Ĺ����ߣ�
        /// </summary>
        /// <param name="args"></param>
        /// <remark>.NET CORE ����һ��IOC���� �õ�����IOC����Autofac������õ�</remark>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration((hostingContext, builder) =>
                { 
                    var env = hostingContext.HostingEnvironment;
                    //����appsettings.json�ļ� ʹ��ģ�崴������Ŀ��������һ�������ļ��������ļ��а���Logging��������
                    builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddApollo(builder.Build().GetSection("apollo"))
                    .AddDefault();//Ĭ�� namespace: application

                    //ApolloConfig.Configuration = builder.Build();
                })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                    webBuilder.UseStartup<Startup>().UseNLog();
            }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
        #region ��Ӱ���������
        //  .ConfigureAppConfiguration((hostingContext, builder) =>
        //    {
        //    //builder.SetBasePath(Directory.GetCurrentDirectory())
        //    //.AddJsonFile(
        //    //    hostingContext.HostingEnvironment.IsProduction()
        //    //        ? "appsettings.Production.json"
        //    //        : "appsettings.Development.json", true, true)
        //    var env = hostingContext.HostingEnvironment;
        //    //����appsettings.json�ļ� ʹ��ģ�崴������Ŀ��������һ�������ļ��������ļ��а���Logging��������
        //    builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
        //    //.AddApollo(builder.Build().GetSection("apollo"))
        //    //.AddDefault();

        //    //ApolloConfig.Configuration = builder.Build();
        //})
        #endregion

        //   .ConfigureLogging((hostingContext, logging) =>
        //    {
        //    logging.AddDebug();
        //})
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="hostBuilder"></param>
        ///// <param name="startupType"></param>
        ///// <returns></returns>
        //public static IWebHostBuilder UseStartup(this IWebHostBuilder hostBuilder, Type startupType)
        //{
        //    var startupAssemblyName = startupType.GetTypeInfo().Assembly.GetName().Name;

        //    return hostBuilder
        //        .UseSetting(WebHostDefaults.ApplicationKey, startupAssemblyName)
        //        .ConfigureServices(services =>
        //        {
        //            if (typeof(IStartup).GetTypeInfo().IsAssignableFrom(startupType.GetTypeInfo()))
        //            {
        //                services.AddSingleton(typeof(IStartup), startupType);
        //            }
        //            else
        //            {
        //                services.AddSingleton(typeof(IStartup), sp =>
        //                {
        //                    var hostingEnvironment = sp.GetRequiredService<IHostingEnvironment>();
        //                    return new ConventionBasedStartup(StartupLoader.LoadMethods(sp, startupType, hostingEnvironment.EnvironmentName));
        //                });
        //            }
        //        });
        //}
    }
}
