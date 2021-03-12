using System;

namespace Microsoft.Isam.Esent.Interop.Windows7
{
	// Token: 0x0200030F RID: 783
	public static class Windows7Grbits
	{
		// Token: 0x04000994 RID: 2452
		public const ColumndefGrbit ColumnCompressed = (ColumndefGrbit)524288;

		// Token: 0x04000995 RID: 2453
		public const SetColumnGrbit Compressed = (SetColumnGrbit)131072;

		// Token: 0x04000996 RID: 2454
		public const SetColumnGrbit Uncompressed = (SetColumnGrbit)65536;

		// Token: 0x04000997 RID: 2455
		public const InitGrbit ReplayIgnoreLostLogs = (InitGrbit)128;

		// Token: 0x04000998 RID: 2456
		public const TermGrbit Dirty = (TermGrbit)8;

		// Token: 0x04000999 RID: 2457
		public const TempTableGrbit IntrinsicLVsOnly = (TempTableGrbit)128;

		// Token: 0x0400099A RID: 2458
		public const EnumerateColumnsGrbit EnumerateInRecordOnly = (EnumerateColumnsGrbit)2097152;

		// Token: 0x0400099B RID: 2459
		public const CommitTransactionGrbit ForceNewLog = (CommitTransactionGrbit)16;

		// Token: 0x0400099C RID: 2460
		public const SnapshotPrepareGrbit ExplicitPrepare = (SnapshotPrepareGrbit)8;

		// Token: 0x0400099D RID: 2461
		public const SetTableSequentialGrbit Forward = (SetTableSequentialGrbit)1;

		// Token: 0x0400099E RID: 2462
		public const DefragGrbit NoPartialMerges = (DefragGrbit)128;

		// Token: 0x0400099F RID: 2463
		public const DefragGrbit DefragmentBTree = (DefragGrbit)256;

		// Token: 0x040009A0 RID: 2464
		public const SetTableSequentialGrbit Backward = (SetTableSequentialGrbit)2;

		// Token: 0x040009A1 RID: 2465
		public const AttachDatabaseGrbit EnableAttachDbBackgroundMaintenance = (AttachDatabaseGrbit)2048;

		// Token: 0x040009A2 RID: 2466
		public const CreateDatabaseGrbit EnableCreateDbBackgroundMaintenance = (CreateDatabaseGrbit)2048;
	}
}
