using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
           // SmartCore.Infrastructure.LogManager.Info("Current Thead Id:{0}", Thread.CurrentThread.ManagedThreadId);
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
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    //Ĭ�� namespace: application
                    // .AddApollo(builder.Build().GetSection("apollo"))
                    //.AddDefault()
                    //ApolloConfig.Configuration = builder.Build();
                })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                    webBuilder.UseStartup<Startup>().UseNLog();
            }).UseServiceProviderFactory(new AutofacServiceProviderFactory());

        //.UseKestrel(options =>
        //            {
        //    //options.ListenUnixSocket("/tmp/kestrel-server.sock");
        //    //options.ListenUnixSocket("/tmp/kestrel-test.sock", listenOptions =>
        //    //{
        //    //    listenOptions.UseHttps("testCert.pfx", "testpassword");
        //    //}); 
        //    // Set properties and call methods on options
        //    //Ϊ����Ӧ�����ò����򿪵���� TCP ������,Ĭ������£������������������ (NULL)
        //    //options.Limits.MaxConcurrentConnections = 100;
        //    //�����Ѵ� HTTP �� HTTPS ��������һ��Э�飨���磬Websocket ���󣩵����ӣ���һ�����������ơ� ���������󣬲������ MaxConcurrentConnections ����
        //    //options.Limits.MaxConcurrentUpgradedConnections = 100;
        //    //����������СMaximum request body size ȱʡֵΪ30,000,000byte, ��Լ��28.6MB��
        //    options.Limits.MaxRequestBodySize = 10 * 1024;
        //    //��С������������Minimum request body data rate ȱʡֵΪ30 to 240 bytes/second with a 5 second grace period. https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.server.kestrel.core.kestrelserverlimits.minrequestbodydatarate?view=aspnetcore-2.1
        //    options.Limits.MinRequestBodyDataRate = null;//��ֵnull Ϊ�˽����Ų����� 50 ��ʱ�ᷢ��Reading the request body timed out due to data arriving too slowly. See MinRequestBodyDataRate����쳣
        //                                                 //new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
        //    options.Limits.MinResponseDataRate =
        //        new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
        //    //��ȡ�����ñ��ֻ״̬��ʱ�� 
        //    //option.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(20);
        //    //option.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(20);
        //    //options.Listen(IPAddress.Loopback, 5000);
        //    //options.Listen(IPAddress.Loopback, 5001, listenOptions =>
        //    //{
        //    //    listenOptions.UseHttps("testCert.pfx", "testPassword");
        //    //});

        //})

    }
}
