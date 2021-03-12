using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200029E RID: 670
	internal struct NATIVE_INSTANCE_INFO
	{
		// Token: 0x04000755 RID: 1877
		public IntPtr hInstanceId;

		// Token: 0x04000756 RID: 1878
		public IntPtr szInstanceName;

		// Token: 0x04000757 RID: 1879
		public IntPtr cDatabases;

		// Token: 0x04000758 RID: 1880
		public unsafe IntPtr* szDatabaseFileName;

		// Token: 0x04000759 RID: 1881
		public unsafe IntPtr* szDatabaseDisplayName;

		// Token: 0x0400075A RID: 1882
		[Obsolete("SLV files are not supported")]
		public unsafe IntPtr* szDatabaseSLVFileName;
	}
}
