using System;
using System.Security.Principal;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001DF RID: 479
	internal class OABGeneratorMailboxData : StoreMailboxData
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x00069FC3 File Offset: 0x000681C3
		public OABGeneratorMailboxData(OrganizationId organizationId, Guid databaseGuid, Guid mailboxGuid, string displayName, SecurityIdentifier mailboxSid, string mailboxDomain, Guid offlineAddressBook, TenantPartitionHint tenantPartitionHint, string jobDescription) : base(mailboxGuid, databaseGuid, displayName, organizationId, tenantPartitionHint)
		{
			this.MailboxSid = mailboxSid;
			this.MailboxDomain = mailboxDomain;
			this.OfflineAddressBook = offlineAddressBook;
			this.JobDescription = jobDescription;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00069FF2 File Offset: 0x000681F2
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00069FFA File Offset: 0x000681FA
		public SecurityIdentifier MailboxSid { get; private set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0006A003 File Offset: 0x00068203
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x0006A00B File Offset: 0x0006820B
		public string MailboxDomain { get; private set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0006A014 File Offset: 0x00068214
		// (set) Token: 0x06001272 RID: 4722 RVA: 0x0006A01C File Offset: 0x0006821C
		public Guid OfflineAddressBook { get; private set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0006A025 File Offset: 0x00068225
		// (set) Token: 0x06001274 RID: 4724 RVA: 0x0006A02D File Offset: 0x0006822D
		public string JobDescription { get; private set; }
	}
}
