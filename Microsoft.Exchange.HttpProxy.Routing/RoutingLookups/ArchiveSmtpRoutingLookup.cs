using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x02000034 RID: 52
	internal class ArchiveSmtpRoutingLookup : MailboxRoutingLookupBase<ArchiveSmtpRoutingKey>
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00003E48 File Offset: 0x00002048
		public ArchiveSmtpRoutingLookup(IUserProvider userProvider) : base(userProvider)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003E51 File Offset: 0x00002051
		protected override User FindUser(ArchiveSmtpRoutingKey archiveSmtpRoutingKey, IRoutingDiagnostics diagnostics)
		{
			return base.UserProvider.FindBySmtpAddress(archiveSmtpRoutingKey.SmtpAddress, diagnostics);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003E65 File Offset: 0x00002065
		protected override void SelectDatabaseGuidResourceForest(ArchiveSmtpRoutingKey mailboxGuidRoutingKey, User user, out Guid? databaseGuid, out string resourceForest)
		{
			databaseGuid = user.ArchiveDatabaseGuid;
			resourceForest = user.ArchiveDatabaseResourceForest;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003E7C File Offset: 0x0000207C
		protected override string GetDomainName(ArchiveSmtpRoutingKey routingKey)
		{
			return routingKey.SmtpAddress.Domain;
		}
	}
}
