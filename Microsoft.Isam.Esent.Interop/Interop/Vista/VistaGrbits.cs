using System;

namespace Microsoft.Isam.Esent.Interop.Vista
{
	// Token: 0x02000305 RID: 773
	public static class VistaGrbits
	{
		// Token: 0x0400095D RID: 2397
		public const CreateIndexGrbit IndexCrossProduct = (CreateIndexGrbit)16384;

		// Token: 0x0400095E RID: 2398
		public const CreateIndexGrbit IndexDisallowTruncation = (CreateIndexGrbit)65536;

		// Token: 0x0400095F RID: 2399
		public const CreateIndexGrbit IndexNestedTable = (CreateIndexGrbit)131072;

		// Token: 0x04000960 RID: 2400
		public const EndExternalBackupGrbit TruncateDone = (EndExternalBackupGrbit)256;

		// Token: 0x04000961 RID: 2401
		public const InitGrbit RecoveryWithoutUndo = (InitGrbit)8;

		// Token: 0x04000962 RID: 2402
		public const InitGrbit TruncateLogsAfterRecovery = (InitGrbit)16;

		// Token: 0x04000963 RID: 2403
		public const InitGrbit ReplayMissingMapEntryDB = (InitGrbit)32;

		// Token: 0x04000964 RID: 2404
		public const InitGrbit LogStreamMustExist = (InitGrbit)64;

		// Token: 0x04000965 RID: 2405
		public const SnapshotPrepareGrbit ContinueAfterThaw = (SnapshotPrepareGrbit)4;

		// Token: 0x04000966 RID: 2406
		internal const CreateIndexGrbit IndexKeyMost = (CreateIndexGrbit)32768;

		// Token: 0x04000967 RID: 2407
		internal const CreateIndexGrbit IndexUnicode = (CreateIndexGrbit)2048;
	}
}
