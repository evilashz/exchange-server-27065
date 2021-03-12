using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x0200003C RID: 60
	internal class MailboxGuidRoutingLookup : MailboxRoutingLookupBase<MailboxGuidRoutingKey>
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00004181 File Offset: 0x00002381
		public MailboxGuidRoutingLookup(IUserProvider userProvider) : base(userProvider)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000418A File Offset: 0x0000238A
		protected override User FindUser(MailboxGuidRoutingKey mailboxGuidRoutingKey, IRoutingDiagnostics diagnostics)
		{
			return base.UserProvider.FindByExchangeGuidIncludingAlternate(mailboxGuidRoutingKey.MailboxGuid, mailboxGuidRoutingKey.TenantDomain, diagnostics);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000041A4 File Offset: 0x000023A4
		protected override void SelectDatabaseGuidResourceForest(MailboxGuidRoutingKey mailboxGuidRoutingKey, User user, out Guid? databaseGuid, out string resourceForest)
		{
			if (mailboxGuidRoutingKey.MailboxGuid == user.ArchiveGuid)
			{
				databaseGuid = user.ArchiveDatabaseGuid;
				resourceForest = user.ArchiveDatabaseResourceForest;
				return;
			}
			databaseGuid = user.DatabaseGuid;
			resourceForest = user.DatabaseResourceForest;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004205 File Offset: 0x00002405
		protected override string GetDomainName(MailboxGuidRoutingKey routingKey)
		{
			return routingKey.TenantDomain;
		}
	}
}
