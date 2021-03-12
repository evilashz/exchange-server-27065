using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200025E RID: 606
	[Flags]
	public enum SpaceHintsGrbit
	{
		// Token: 0x04000439 RID: 1081
		None = 0,
		// Token: 0x0400043A RID: 1082
		SpaceHintUtilizeParentSpace = 1,
		// Token: 0x0400043B RID: 1083
		CreateHintAppendSequential = 2,
		// Token: 0x0400043C RID: 1084
		CreateHintHotpointSequential = 4,
		// Token: 0x0400043D RID: 1085
		RetrieveHintReserve1 = 8,
		// Token: 0x0400043E RID: 1086
		RetrieveHintTableScanForward = 16,
		// Token: 0x0400043F RID: 1087
		RetrieveHintTableScanBackward = 32,
		// Token: 0x04000440 RID: 1088
		RetrieveHintReserve2 = 64,
		// Token: 0x04000441 RID: 1089
		RetrieveHintReserve3 = 128,
		// Token: 0x04000442 RID: 1090
		DeleteHintTableSequential = 256
	}
}
