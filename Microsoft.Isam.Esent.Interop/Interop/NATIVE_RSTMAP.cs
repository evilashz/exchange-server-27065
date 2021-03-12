using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002C0 RID: 704
	internal struct NATIVE_RSTMAP
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x000199F6 File Offset: 0x00017BF6
		public void FreeHGlobal()
		{
			LibraryHelpers.MarshalFreeHGlobal(this.szDatabaseName);
			LibraryHelpers.MarshalFreeHGlobal(this.szNewDatabaseName);
		}

		// Token: 0x04000833 RID: 2099
		public IntPtr szDatabaseName;

		// Token: 0x04000834 RID: 2100
		public IntPtr szNewDatabaseName;
	}
}
