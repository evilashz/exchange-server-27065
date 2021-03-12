using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Transport.FrontendProxyRoutingAgent
{
	// Token: 0x02000005 RID: 5
	public class FrontendProxyAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002298 File Offset: 0x00000498
		public FrontendProxyAgentFactory()
		{
			this.agentConfiguration = FrontendProxyAgentConfiguration.Load();
			ExTraceGlobals.FrontendProxyAgentTracer.TraceInformation(0, (long)this.GetHashCode(), "The FrontendProxyAgentFactory is created and configuration loaded");
			Components.PerfCountersLoaderComponent.AddCounterToGetExchangeDiagnostics(typeof(FrontendProxyAgentPerformanceCounters), "FrontendProxyAgentPerformanceCounters");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022E6 File Offset: 0x000004E6
		internal FrontendProxyAgentFactory(IProxyHubSelector hubSelector) : this()
		{
			this.hubSelector = hubSelector;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022F5 File Offset: 0x000004F5
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new FrontendProxyAgent(this.agentConfiguration, this.hubSelector ?? Components.ProxyHubSelectorComponent.ProxyHubSelector);
		}

		// Token: 0x04000007 RID: 7
		private FrontendProxyAgentConfiguration agentConfiguration;

		// Token: 0x04000008 RID: 8
		private IProxyHubSelector hubSelector;
	}
}
