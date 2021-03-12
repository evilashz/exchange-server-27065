using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.Transport.Agent.AddressBookPolicyRoutingAgent
{
	// Token: 0x02000003 RID: 3
	public sealed class AddressBookPolicyRoutingAgentFactory : RoutingAgentFactory
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000029CC File Offset: 0x00000BCC
		public AddressBookPolicyRoutingAgentFactory()
		{
			this.cacheTimeout = TimeSpan.FromMinutes((double)TransportAppConfig.GetConfigInt("AddressBookPolicyRoutingCacheTimeoutMinutes", 1, 30, 30));
			this.deferralTimeout = TimeSpan.FromMinutes((double)TransportAppConfig.GetConfigInt("AddressBookPolicyRoutingDeferralTimeoutMinutes", 1, 60, 15));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002A4E File Offset: 0x00000C4E
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new AddressBookPolicyRoutingAgent(this.abpToGalCache, this.orgToGalCache, this.userToAddrListCache, this.cacheTimeout, this.deferralTimeout);
		}

		// Token: 0x04000008 RID: 8
		private const string DeferralTimeoutName = "AddressBookPolicyRoutingDeferralTimeoutMinutes";

		// Token: 0x04000009 RID: 9
		private const string CacheTimeoutName = "AddressBookPolicyRoutingCacheTimeoutMinutes";

		// Token: 0x0400000A RID: 10
		private readonly TimeSpan cacheTimeout;

		// Token: 0x0400000B RID: 11
		private readonly TimeSpan deferralTimeout;

		// Token: 0x0400000C RID: 12
		private readonly TimeoutCache<Guid, Guid> abpToGalCache = new TimeoutCache<Guid, Guid>(16, 1024, false);

		// Token: 0x0400000D RID: 13
		private readonly TimeoutCache<OrganizationId, AddressBookBase[]> orgToGalCache = new TimeoutCache<OrganizationId, AddressBookBase[]>(16, 1024, false);

		// Token: 0x0400000E RID: 14
		private readonly TimeoutCache<ADObjectId, ADMultiValuedProperty<ADObjectId>> userToAddrListCache = new TimeoutCache<ADObjectId, ADMultiValuedProperty<ADObjectId>>(16, 1024, false);
	}
}
