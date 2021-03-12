using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAssociationAdaptor
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000049 RID: 73
		// (remove) Token: 0x0600004A RID: 74
		event Action<IMailboxLocator> OnAfterJoin;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004B RID: 75
		IMailboxLocator MasterLocator { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004C RID: 76
		IAssociationStore AssociationStore { get; }

		// Token: 0x0600004D RID: 77
		MailboxAssociation GetAssociation(IMailboxLocator locator);

		// Token: 0x0600004E RID: 78
		IEnumerable<MailboxAssociation> GetAllAssociations();

		// Token: 0x0600004F RID: 79
		IEnumerable<MailboxAssociation> GetMembershipAssociations(int? maxItems);

		// Token: 0x06000050 RID: 80
		IEnumerable<MailboxAssociation> GetEscalatedAssociations();

		// Token: 0x06000051 RID: 81
		IEnumerable<MailboxAssociation> GetPinAssociations();

		// Token: 0x06000052 RID: 82
		IEnumerable<MailboxAssociation> GetAssociationsWithMembershipChangedAfter(ExDateTime date);

		// Token: 0x06000053 RID: 83
		MailboxLocator GetSlaveMailboxLocator(MailboxAssociation association);

		// Token: 0x06000054 RID: 84
		void DeleteAssociation(MailboxAssociation association);

		// Token: 0x06000055 RID: 85
		void SaveAssociation(MailboxAssociation association, bool markForReplication);

		// Token: 0x06000056 RID: 86
		void SaveSyncState(MailboxAssociation association);

		// Token: 0x06000057 RID: 87
		void ReplicateAssociation(MailboxAssociation association);
	}
}
