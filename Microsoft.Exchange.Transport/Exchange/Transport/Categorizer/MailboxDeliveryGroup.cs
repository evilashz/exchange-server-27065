using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000240 RID: 576
	internal class MailboxDeliveryGroup : MailboxDeliveryGroupBase
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x00065C8C File Offset: 0x00063E8C
		public MailboxDeliveryGroup(MailboxDeliveryGroupId id, RouteInfo siteRouteInfo, RoutingServerInfo serverInfo, bool isLocalDeliveryGroup, RoutingContextCore contextCore) : base(new RoutedServerCollection(siteRouteInfo, serverInfo, contextCore), DeliveryType.SmtpRelayToMailboxDeliveryGroup, id.ToString(), Guid.Empty, serverInfo.MajorVersion, isLocalDeliveryGroup)
		{
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00065CB9 File Offset: 0x00063EB9
		public void AddHubServer(RoutingServerInfo server, RoutingContextCore contextCore)
		{
			base.AddServerInternal(base.RoutedServerCollection.PrimaryRoute, server, contextCore);
		}
	}
}
