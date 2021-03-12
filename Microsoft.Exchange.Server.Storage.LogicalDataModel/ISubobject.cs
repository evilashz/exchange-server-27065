using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200001A RID: 26
	internal interface ISubobject
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000465 RID: 1125
		int ChildNumber { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000466 RID: 1126
		long? CurrentInid { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000467 RID: 1127
		long? OriginalInid { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000468 RID: 1128
		long CurrentSize { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000469 RID: 1129
		long OriginalSize { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600046A RID: 1130
		SubobjectCollection Subobjects { get; }
	}
}
