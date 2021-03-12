using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.FrontendProxyRoutingAgent
{
	// Token: 0x02000004 RID: 4
	public class FrontendProxyAgentConfiguration
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000224A File Offset: 0x0000044A
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002252 File Offset: 0x00000452
		public RoutingHost ProxyDestinationOverride { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x0000225C File Offset: 0x0000045C
		public static FrontendProxyAgentConfiguration Load()
		{
			FrontendProxyAgentConfiguration frontendProxyAgentConfiguration = new FrontendProxyAgentConfiguration();
			string configString = TransportAppConfig.GetConfigString("FrontendProxyDestinationOverride", null);
			if (!string.IsNullOrEmpty(configString))
			{
				frontendProxyAgentConfiguration.ProxyDestinationOverride = new RoutingHost(configString);
			}
			return frontendProxyAgentConfiguration;
		}

		// Token: 0x04000005 RID: 5
		private const string ProxyDestinationString = "FrontendProxyDestinationOverride";
	}
}
