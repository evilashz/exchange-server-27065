using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000076 RID: 118
	internal static class FederatedDirectorySessionFactory
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000ED80 File Offset: 0x0000CF80
		public static DirectorySession Create(ADUser accessingUser, ExchangePrincipal accessingPrincipal)
		{
			ArgumentValidator.ThrowIfNull("accessingUser", accessingUser);
			ArgumentValidator.ThrowIfNull("accessingPrincipal", accessingPrincipal);
			FederatedDirectorySessionFactory.InitializeIfNeeded();
			ExchangeDirectorySessionContext exchangeDirectorySessionContext = new ExchangeDirectorySessionContext(accessingUser, accessingPrincipal);
			FederatedDirectorySessionFactory.Tracer.TraceDebug<ExchangeDirectorySessionContext>(0L, "Created DirectorySession with context: {0}", exchangeDirectorySessionContext);
			return new DirectorySession(exchangeDirectorySessionContext);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		private static void InitializeIfNeeded()
		{
			if (!FederatedDirectorySessionFactory.initialized)
			{
				lock (FederatedDirectorySessionFactory.locker)
				{
					if (!FederatedDirectorySessionFactory.initialized)
					{
						FederatedDirectorySessionFactory.Initialize();
						FederatedDirectorySessionFactory.initialized = true;
					}
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000EE1C File Offset: 0x0000D01C
		private static void Initialize()
		{
			FederatedDirectorySessionFactory.Tracer.TraceDebug(0L, "Initializing AdapterManager and LogManager");
			LogWriter.Initialize();
			Assembly assembly = Assembly.GetAssembly(typeof(AdapterManager));
			string text = Path.Combine(Path.GetDirectoryName(assembly.Location), "FederatedDirectory.config");
			if (!File.Exists(text))
			{
				text = Path.Combine(ExchangeSetupContext.BinPath, "FederatedDirectory.config");
			}
			FederatedDirectorySessionFactory.Tracer.TraceDebug<string>(0L, "Using config file {0}", text);
			AdapterManager.Initialize(text);
			FederatedDirectorySessionFactory.Tracer.TraceDebug(0L, "AdapterManager and LogManager Initialized");
		}

		// Token: 0x040005AD RID: 1453
		private const string ConfigFileName = "FederatedDirectory.config";

		// Token: 0x040005AE RID: 1454
		private static readonly Trace Tracer = ExTraceGlobals.FederatedDirectoryTracer;

		// Token: 0x040005AF RID: 1455
		private static bool initialized;

		// Token: 0x040005B0 RID: 1456
		private static object locker = new object();
	}
}
