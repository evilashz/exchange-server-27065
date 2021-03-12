using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007D6 RID: 2006
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxAssociationUser : IMailboxAssociationBaseItem, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06004B3A RID: 19258
		// (set) Token: 0x06004B3B RID: 19259
		string JoinedBy { get; set; }

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06004B3C RID: 19260
		// (set) Token: 0x06004B3D RID: 19261
		ExDateTime LastVisitedDate { get; set; }
	}
}
