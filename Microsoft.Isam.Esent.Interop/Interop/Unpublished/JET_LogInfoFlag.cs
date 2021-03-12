using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000039 RID: 57
	[Flags]
	public enum JET_LogInfoFlag
	{
		// Token: 0x04000120 RID: 288
		None = 0,
		// Token: 0x04000121 RID: 289
		ReservedLog = 1,
		// Token: 0x04000122 RID: 290
		CircularLoggingCurrent = 2,
		// Token: 0x04000123 RID: 291
		CircularLoggingHistory = 4
	}
}
