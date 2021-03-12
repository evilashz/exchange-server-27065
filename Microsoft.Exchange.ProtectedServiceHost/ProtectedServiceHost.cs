using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x02000003 RID: 3
	internal sealed class ProtectedServiceHost : ServiceHostBase
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002AD4 File Offset: 0x00000CD4
		static ProtectedServiceHost()
		{
			ExWatson.Register();
			ServiceHostBase.ComponentGuid = new Guid("AFD2E500-91AB-4d71-9E76-91B13D8818B6");
			ServiceHostBase.Log = new ExEventLog(ServiceHostBase.ComponentGuid, "MSExchangeProtectedServiceHost");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B45 File Offset: 0x00000D45
		private ProtectedServiceHost() : base("MSExchangeProtectedServiceHost", ProtectedServiceHost.Modules)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B58 File Offset: 0x00000D58
		public static void Main(string[] args)
		{
			using (ProtectedServiceHost protectedServiceHost = new ProtectedServiceHost())
			{
				ServiceHostBase.MainEntry(protectedServiceHost, args);
			}
		}

		// Token: 0x04000007 RID: 7
		private static readonly ModuleMap[] Modules = new ModuleMap[]
		{
			new ModuleMap("Microsoft.Exchange.AuthServiceHostServicelet.dll", "Microsoft.Exchange.Servicelets.AuthServiceHost.Servicelet", ServerRole.Cafe | ServerRole.HubTransport | ServerRole.FfoWebService, ServerRole.None, RunInExchangeMode.ExchangeDatacenter),
			new ModuleMap("Microsoft.Exchange.Servicelets.GlobalLocatorCache.dll", "Microsoft.Exchange.Servicelets.GlobalLocatorCache.Servicelet", ServerRole.Cafe | ServerRole.Mailbox | ServerRole.HubTransport, ServerRole.None, RunInExchangeMode.ExchangeDatacenter)
		};
	}
}
