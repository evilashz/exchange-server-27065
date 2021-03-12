using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000029 RID: 41
	public enum CopyState
	{
		// Token: 0x04000088 RID: 136
		SuccessfulCopy,
		// Token: 0x04000089 RID: 137
		OpeningInputFile,
		// Token: 0x0400008A RID: 138
		ReadingInputFile,
		// Token: 0x0400008B RID: 139
		CompletingRead,
		// Token: 0x0400008C RID: 140
		OpeningOutputFile,
		// Token: 0x0400008D RID: 141
		WritingOutputFile,
		// Token: 0x0400008E RID: 142
		CompletingWrite,
		// Token: 0x0400008F RID: 143
		FlushingOutputFile,
		// Token: 0x04000090 RID: 144
		CopyStopped,
		// Token: 0x04000091 RID: 145
		InvalidOperation
	}
}
