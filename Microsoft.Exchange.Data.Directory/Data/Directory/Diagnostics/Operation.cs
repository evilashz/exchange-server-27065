using System;

namespace Microsoft.Exchange.Data.Directory.Diagnostics
{
	// Token: 0x020000B8 RID: 184
	internal enum Operation : byte
	{
		// Token: 0x04000369 RID: 873
		None,
		// Token: 0x0400036A RID: 874
		GetOperation,
		// Token: 0x0400036B RID: 875
		RemoveOperation,
		// Token: 0x0400036C RID: 876
		PutOperation,
		// Token: 0x0400036D RID: 877
		WCFGetOperation,
		// Token: 0x0400036E RID: 878
		WCFRemoveOperation,
		// Token: 0x0400036F RID: 879
		WCFPutOperation,
		// Token: 0x04000370 RID: 880
		ObjectInitialization,
		// Token: 0x04000371 RID: 881
		ObjectCreation,
		// Token: 0x04000372 RID: 882
		TotalWCFGetOperation,
		// Token: 0x04000373 RID: 883
		TotalWCFRemoveOperation,
		// Token: 0x04000374 RID: 884
		TotalWCFPutOperation,
		// Token: 0x04000375 RID: 885
		WCFBeginOperation,
		// Token: 0x04000376 RID: 886
		WCFEndOperation,
		// Token: 0x04000377 RID: 887
		DataSize,
		// Token: 0x04000378 RID: 888
		TenantRelocationCheck,
		// Token: 0x04000379 RID: 889
		WCFProxyObjectCreation
	}
}
