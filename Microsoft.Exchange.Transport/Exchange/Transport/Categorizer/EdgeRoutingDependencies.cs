using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000226 RID: 550
	internal class EdgeRoutingDependencies
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x0006298B File Offset: 0x00060B8B
		public EdgeRoutingDependencies(ITransportConfiguration transportConfig)
		{
			this.transportConfig = transportConfig;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0006299A File Offset: 0x00060B9A
		public virtual ICollection<string> EdgeToHubAcceptedDomains
		{
			get
			{
				RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
				return this.transportConfig.FirstOrgAcceptedDomainTable.EdgeToBHDomains;
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000629BC File Offset: 0x00060BBC
		public virtual bool IsLocalServerId(ADObjectId serverId)
		{
			RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
			return this.transportConfig.LocalServer.TransportServer.Id.Equals(serverId);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000629E9 File Offset: 0x00060BE9
		public virtual void RegisterForAcceptedDomainChanges(ConfigurationUpdateHandler<AcceptedDomainTable> handler)
		{
			RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
			this.transportConfig.FirstOrgAcceptedDomainTableChanged += handler;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00062A07 File Offset: 0x00060C07
		public virtual void UnregisterFromAcceptedDomainChanges(ConfigurationUpdateHandler<AcceptedDomainTable> handler)
		{
			RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
			this.transportConfig.FirstOrgAcceptedDomainTableChanged -= handler;
		}

		// Token: 0x04000BCA RID: 3018
		private ITransportConfiguration transportConfig;
	}
}
