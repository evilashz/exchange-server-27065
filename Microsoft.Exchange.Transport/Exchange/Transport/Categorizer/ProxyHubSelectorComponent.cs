using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001BE RID: 446
	internal class ProxyHubSelectorComponent : IProxyHubSelectorComponent, ITransportComponent
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00052D21 File Offset: 0x00050F21
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ProxyHubSelectorTracer;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00052D28 File Offset: 0x00050F28
		public static SystemProbeTrace SystemProbeTracer
		{
			get
			{
				return ProxyHubSelectorComponent.systemProbeTracer;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00052D2F File Offset: 0x00050F2F
		public IProxyHubSelector ProxyHubSelector
		{
			get
			{
				if (this.hubSelector == null)
				{
					throw new InvalidOperationException("Hub selector cannot be used before the component is loaded");
				}
				return this.hubSelector;
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00052D4C File Offset: 0x00050F4C
		public void Load()
		{
			if (this.router == null)
			{
				throw new InvalidOperationException("SetLoadTimeDependencies() must be called before Load()");
			}
			if (this.configuration.ProcessTransportRole == ProcessTransportRole.FrontEnd && !Datacenter.IsForefrontForOfficeDatacenter() && (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.OrganizationMailboxRouting.Enabled || Datacenter.IsPartnerHostedOnly(false)))
			{
				this.orgMailboxCache = new OrganizationMailboxDatabaseCache(this.configuration.AppConfig.PerTenantCache, this.configuration.ProcessTransportRole);
			}
			ProxyHubSelectorPerformanceCounters perfCounters = new ProxyHubSelectorPerformanceCounters(this.configuration.ProcessTransportRole);
			this.hubSelector = new ProxyHubSelector(this.router, this.orgMailboxCache, perfCounters, this.configuration.AppConfig.Routing);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00052E01 File Offset: 0x00051001
		public void Unload()
		{
			this.hubSelector = null;
			if (this.orgMailboxCache != null)
			{
				this.orgMailboxCache.Dispose();
				this.orgMailboxCache = null;
			}
			this.router = null;
			this.configuration = null;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00052E32 File Offset: 0x00051032
		public void SetLoadTimeDependencies(IMailRouter router, ITransportConfiguration configuration)
		{
			if (router == null)
			{
				throw new ArgumentNullException("router");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.router = router;
			this.configuration = configuration;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00052E5E File Offset: 0x0005105E
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x04000A63 RID: 2659
		private static readonly SystemProbeTrace systemProbeTracer = new SystemProbeTrace(ExTraceGlobals.ProxyHubSelectorTracer, "ProxyHubSelector");

		// Token: 0x04000A64 RID: 2660
		private ProxyHubSelector hubSelector;

		// Token: 0x04000A65 RID: 2661
		private OrganizationMailboxDatabaseCache orgMailboxCache;

		// Token: 0x04000A66 RID: 2662
		private IMailRouter router;

		// Token: 0x04000A67 RID: 2663
		private ITransportConfiguration configuration;
	}
}
