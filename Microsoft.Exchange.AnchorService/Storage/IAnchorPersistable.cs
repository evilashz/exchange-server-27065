using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorPersistable : IAnchorSerializable
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000260 RID: 608
		StoreObjectId StoreObjectId { get; }
	}
}
