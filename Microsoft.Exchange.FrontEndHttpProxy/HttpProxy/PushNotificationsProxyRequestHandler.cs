using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C8 RID: 200
	internal class PushNotificationsProxyRequestHandler : ProxyRequestHandler
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x0002BCF6 File Offset: 0x00029EF6
		protected override MailboxServerLocator CreateMailboxServerLocator(Guid databaseGuid, string domainName, string resourceForest)
		{
			return base.CreateMailboxServerLocator(databaseGuid, domainName, resourceForest);
		}
	}
}
