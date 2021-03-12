using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Transport.FrontendProxyRoutingAgent
{
	// Token: 0x02000002 RID: 2
	internal class FrontendProxyAgent : SmtpReceiveAgent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public FrontendProxyAgent(FrontendProxyAgentConfiguration configuration, IProxyHubSelector hubSelector)
		{
			this.configuration = configuration;
			this.hubSelector = hubSelector;
			base.OnProxyInboundMessage += this.OnProxyInboundMessageEventHandler;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		private void OnProxyInboundMessageEventHandler(ProxyInboundMessageEventSource source, ProxyInboundMessageEventArgs e)
		{
			IEnumerable<INextHopServer> destinationServers = null;
			if (this.configuration.ProxyDestinationOverride != null)
			{
				FrontendProxyAgent.systemProbeTracer.TracePass(e.MailItem, (long)this.GetHashCode(), "The proxy destination override {0} was found in configuration and will be used as a proxy target", this.configuration.ProxyDestinationOverride);
				destinationServers = new RoutingHost[]
				{
					this.configuration.ProxyDestinationOverride
				};
			}
			else if (e.ClientIsPreE15InternalServer && e.LocalFrontendIsColocatedWithHub)
			{
				destinationServers = new FrontendProxyAgent.ColocatedNextHopServer[]
				{
					new FrontendProxyAgent.ColocatedNextHopServer(e.LocalServerFqdn)
				};
			}
			else
			{
				ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)e.MailItem;
				TransportMailItem mailItem = (TransportMailItem)transportMailItemWrapperFacade.TransportMailItem;
				if (!this.hubSelector.TrySelectHubServers(mailItem, out destinationServers))
				{
					FrontendProxyAgent.systemProbeTracer.TraceFail(e.MailItem, (long)this.GetHashCode(), "TrySelectHubServers() failed");
					FrontendProxyAgentPerformanceCounters.MessagesFailedToRoute.Increment();
					return;
				}
				FrontendProxyAgent.systemProbeTracer.TracePass(e.MailItem, (long)this.GetHashCode(), "TrySelectHubServers() succeeded");
			}
			source.SetProxyRoutingOverride(destinationServers, true);
			FrontendProxyAgentPerformanceCounters.MessagesSuccessfullyRouted.Increment();
		}

		// Token: 0x04000001 RID: 1
		private static readonly SystemProbeTrace systemProbeTracer = new SystemProbeTrace(ExTraceGlobals.FrontendProxyAgentTracer, "FrontendProxyRoutingAgent");

		// Token: 0x04000002 RID: 2
		private FrontendProxyAgentConfiguration configuration;

		// Token: 0x04000003 RID: 3
		private IProxyHubSelector hubSelector;

		// Token: 0x02000003 RID: 3
		private class ColocatedNextHopServer : INextHopServer
		{
			// Token: 0x06000004 RID: 4 RVA: 0x00002218 File Offset: 0x00000418
			public ColocatedNextHopServer(string fqdn)
			{
				this.Fqdn = fqdn;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000005 RID: 5 RVA: 0x00002227 File Offset: 0x00000427
			public bool IsIPAddress
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000006 RID: 6 RVA: 0x0000222A File Offset: 0x0000042A
			public IPAddress Address
			{
				get
				{
					throw new InvalidOperationException("INextHopServer.IPAddress must not be requested since IsIPAddress is false");
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000007 RID: 7 RVA: 0x00002236 File Offset: 0x00000436
			// (set) Token: 0x06000008 RID: 8 RVA: 0x0000223E File Offset: 0x0000043E
			public string Fqdn { get; private set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000009 RID: 9 RVA: 0x00002247 File Offset: 0x00000447
			public bool IsFrontendAndHubColocatedServer
			{
				get
				{
					return true;
				}
			}
		}
	}
}
