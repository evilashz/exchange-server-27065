using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002E0 RID: 736
	internal class PerTenantOutboundConnectors : TenantConfigurationCacheableItem<TenantOutboundConnector>
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x0007C435 File Offset: 0x0007A635
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x0007C43D File Offset: 0x0007A63D
		public TenantOutboundConnector[] TenantOutboundConnectors { get; private set; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x0007C446 File Offset: 0x0007A646
		public override long ItemSize
		{
			get
			{
				return (long)((this.TenantOutboundConnectors != null) ? this.TenantOutboundConnectors.Length : 0);
			}
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0007C468 File Offset: 0x0007A668
		public override void ReadData(IConfigurationSession tenantConfigurationSession)
		{
			IEnumerable<TenantOutboundConnector> enumerable;
			ADOperationResult outboundConnectors = ConnectorConfiguration.GetOutboundConnectors(tenantConfigurationSession, (TenantOutboundConnector toc) => !toc.IsTransportRuleScoped, out enumerable);
			if (!outboundConnectors.Succeeded)
			{
				throw new TenantOutboundConnectorsRetrievalException(outboundConnectors);
			}
			this.TenantOutboundConnectors = ((enumerable != null) ? enumerable.ToArray<TenantOutboundConnector>() : null);
		}
	}
}
