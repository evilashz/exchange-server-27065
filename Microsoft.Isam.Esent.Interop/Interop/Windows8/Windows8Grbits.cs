using System;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x0200031A RID: 794
	public static class Windows8Grbits
	{
		// Token: 0x040009C5 RID: 2501
		public const InitGrbit KeepDbAttachedAtEndOfRecovery = (InitGrbit)4096;

		// Token: 0x040009C6 RID: 2502
		public const AttachDatabaseGrbit PurgeCacheOnAttach = (AttachDatabaseGrbit)4096;

		// Token: 0x040009C7 RID: 2503
		public const CreateIndexGrbit IndexDotNetGuid = (CreateIndexGrbit)262144;

		// Token: 0x040009C8 RID: 2504
		public const TempTableGrbit TTDotNetGuid = (TempTableGrbit)256;
	}
}
