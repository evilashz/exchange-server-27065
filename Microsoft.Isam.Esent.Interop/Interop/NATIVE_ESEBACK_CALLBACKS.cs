using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200001F RID: 31
	internal struct NATIVE_ESEBACK_CALLBACKS
	{
		// Token: 0x0400004D RID: 77
		public NATIVE_PfnErrESECBPrepareInstanceForBackup pfnPrepareInstance;

		// Token: 0x0400004E RID: 78
		public NATIVE_PfnErrESECBDoneWithInstanceForBackup pfnDoneWithInstance;

		// Token: 0x0400004F RID: 79
		public NATIVE_PfnErrESECBGetDatabasesInfo pfnGetDatabasesInfo;

		// Token: 0x04000050 RID: 80
		public NATIVE_PfnErrESECBFreeDatabasesInfo pfnFreeDatabasesInfo;

		// Token: 0x04000051 RID: 81
		public NATIVE_PfnErrESECBIsSGReplicated pfnIsSGReplicated;

		// Token: 0x04000052 RID: 82
		public NATIVE_PfnErrESECBFreeShipLogInfo pfnFreeShipLogInfo;

		// Token: 0x04000053 RID: 83
		public NATIVE_PfnErrESECBServerAccessCheck pfnServerAccessCheck;

		// Token: 0x04000054 RID: 84
		public NATIVE_PfnErrESECBTrace pfnTrace;
	}
}
