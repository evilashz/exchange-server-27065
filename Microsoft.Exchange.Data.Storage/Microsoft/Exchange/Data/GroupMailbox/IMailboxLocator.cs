using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x020007FE RID: 2046
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxLocator
	{
		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06004C4E RID: 19534
		string LegacyDn { get; }

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06004C4F RID: 19535
		string ExternalId { get; }

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06004C50 RID: 19536
		Guid MailboxGuid { get; }

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06004C51 RID: 19537
		string LocatorType { get; }

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06004C52 RID: 19538
		string IdentityHash { get; }

		// Token: 0x06004C53 RID: 19539
		ADUser FindAdUser();

		// Token: 0x06004C54 RID: 19540
		string[] FindAlternateLegacyDNs();

		// Token: 0x06004C55 RID: 19541
		bool IsValidReplicationTarget();
	}
}
