using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000058 RID: 88
	public enum LogTransactionInformationBlockType : byte
	{
		// Token: 0x040002ED RID: 749
		Unknown,
		// Token: 0x040002EE RID: 750
		ForTestPurposes,
		// Token: 0x040002EF RID: 751
		Identity,
		// Token: 0x040002F0 RID: 752
		AdminRpc,
		// Token: 0x040002F1 RID: 753
		MapiRpc,
		// Token: 0x040002F2 RID: 754
		Task,
		// Token: 0x040002F3 RID: 755
		Digest,
		// Token: 0x040002F4 RID: 756
		MaxValue
	}
}
