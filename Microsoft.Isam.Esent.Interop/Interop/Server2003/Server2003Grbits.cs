using System;

namespace Microsoft.Isam.Esent.Interop.Server2003
{
	// Token: 0x020002E3 RID: 739
	public static class Server2003Grbits
	{
		// Token: 0x04000921 RID: 2337
		public const AttachDatabaseGrbit DeleteUnicodeIndexes = (AttachDatabaseGrbit)1024;

		// Token: 0x04000922 RID: 2338
		public const ColumndefGrbit ColumnDeleteOnZero = (ColumndefGrbit)131072;

		// Token: 0x04000923 RID: 2339
		public const TempTableGrbit ForwardOnly = (TempTableGrbit)64;

		// Token: 0x04000924 RID: 2340
		public const EnumerateColumnsGrbit EnumerateIgnoreUserDefinedDefault = (EnumerateColumnsGrbit)1048576;

		// Token: 0x04000925 RID: 2341
		public const CommitTransactionGrbit WaitAllLevel0Commit = (CommitTransactionGrbit)8;
	}
}
