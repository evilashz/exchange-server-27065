using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x0200003A RID: 58
	internal class LiveIdMemberNameRoutingLookup : MailboxRoutingLookupBase<LiveIdMemberNameRoutingKey>
	{
		// Token: 0x060000EF RID: 239 RVA: 0x0000414C File Offset: 0x0000234C
		public LiveIdMemberNameRoutingLookup(IUserProvider userProvider) : base(userProvider)
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004155 File Offset: 0x00002355
		protected override User FindUser(LiveIdMemberNameRoutingKey liveIdMemberNameRoutingKey, IRoutingDiagnostics diagnostics)
		{
			return base.UserProvider.FindByLiveIdMemberName(liveIdMemberNameRoutingKey.LiveIdMemberName, liveIdMemberNameRoutingKey.OrganizationContext, diagnostics);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000416F File Offset: 0x0000236F
		protected override string GetDomainName(LiveIdMemberNameRoutingKey routingKey)
		{
			return routingKey.OrganizationDomain;
		}
	}
}
