using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000045 RID: 69
	public enum JET_RECOVERYACTIONS
	{
		// Token: 0x04000156 RID: 342
		MissingLogMustFail = 1,
		// Token: 0x04000157 RID: 343
		MissingLogContinueToRedo,
		// Token: 0x04000158 RID: 344
		MissingLogContinueTryCurrentLog,
		// Token: 0x04000159 RID: 345
		MissingLogContinueToUndo,
		// Token: 0x0400015A RID: 346
		MissingLogCreateNewLogStream
	}
}
