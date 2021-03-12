using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000032 RID: 50
	internal enum DiagRecordLayout
	{
		// Token: 0x04000340 RID: 832
		Header,
		// Token: 0x04000341 RID: 833
		dwParam,
		// Token: 0x04000342 RID: 834
		GenericError,
		// Token: 0x04000343 RID: 835
		WindowsError,
		// Token: 0x04000344 RID: 836
		StoreError,
		// Token: 0x04000345 RID: 837
		InfoEx1,
		// Token: 0x04000346 RID: 838
		PtagError,
		// Token: 0x04000347 RID: 839
		Long,
		// Token: 0x04000348 RID: 840
		Guid,
		// Token: 0x04000349 RID: 841
		RpcCall = 16,
		// Token: 0x0400034A RID: 842
		RpcReturn,
		// Token: 0x0400034B RID: 843
		RpcException,
		// Token: 0x0400034C RID: 844
		DeadRpcPool,
		// Token: 0x0400034D RID: 845
		Version,
		// Token: 0x0400034E RID: 846
		Custom = 255
	}
}
