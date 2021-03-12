using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000270 RID: 624
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxInfo
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060019BF RID: 6591
		string DisplayName { get; }

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060019C0 RID: 6592
		SmtpAddress PrimarySmtpAddress { get; }

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060019C1 RID: 6593
		ProxyAddress ExternalEmailAddress { get; }

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060019C2 RID: 6594
		IEnumerable<ProxyAddress> EmailAddresses { get; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060019C3 RID: 6595
		OrganizationId OrganizationId { get; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060019C4 RID: 6596
		Guid MailboxGuid { get; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060019C5 RID: 6597
		ADObjectId MailboxDatabase { get; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060019C6 RID: 6598
		DateTime? WhenMailboxCreated { get; }

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060019C7 RID: 6599
		string ArchiveName { get; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060019C8 RID: 6600
		bool IsArchive { get; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060019C9 RID: 6601
		bool IsAggregated { get; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060019CA RID: 6602
		ArchiveStatusFlags ArchiveStatus { get; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060019CB RID: 6603
		ArchiveState ArchiveState { get; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060019CC RID: 6604
		SmtpAddress? RemoteIdentity { get; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060019CD RID: 6605
		bool IsRemote { get; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060019CE RID: 6606
		IMailboxLocation Location { get; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060019CF RID: 6607
		IMailboxConfiguration Configuration { get; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060019D0 RID: 6608
		MailboxLocationType MailboxType { get; }
	}
}
