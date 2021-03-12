using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MailboxAssociationFromStore : MailboxAssociation
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00006958 File Offset: 0x00004B58
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00006960 File Offset: 0x00004B60
		public VersionedId ItemId { get; set; }
	}
}
