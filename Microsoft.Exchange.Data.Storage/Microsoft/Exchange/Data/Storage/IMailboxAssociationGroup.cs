using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007D5 RID: 2005
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxAssociationGroup : IMailboxAssociationBaseItem, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06004B38 RID: 19256
		// (set) Token: 0x06004B39 RID: 19257
		ExDateTime PinDate { get; set; }
	}
}
