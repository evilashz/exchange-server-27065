using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200026B RID: 619
	[Flags]
	public enum JET_cbtyp
	{
		// Token: 0x04000463 RID: 1123
		Null = 0,
		// Token: 0x04000464 RID: 1124
		Finalize = 1,
		// Token: 0x04000465 RID: 1125
		BeforeInsert = 2,
		// Token: 0x04000466 RID: 1126
		AfterInsert = 4,
		// Token: 0x04000467 RID: 1127
		BeforeReplace = 8,
		// Token: 0x04000468 RID: 1128
		AfterReplace = 16,
		// Token: 0x04000469 RID: 1129
		BeforeDelete = 32,
		// Token: 0x0400046A RID: 1130
		AfterDelete = 64,
		// Token: 0x0400046B RID: 1131
		UserDefinedDefaultValue = 128,
		// Token: 0x0400046C RID: 1132
		OnlineDefragCompleted = 256,
		// Token: 0x0400046D RID: 1133
		FreeCursorLS = 512,
		// Token: 0x0400046E RID: 1134
		FreeTableLS = 1024
	}
}
