using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000005 RID: 5
	internal struct NATIVE_DATABASE_BACKUP_INFO
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000238E File Offset: 0x0000058E
		public void FreeNativeDatabaseBackupInfo()
		{
			Marshal.FreeHGlobal(this.wszDatabaseDisplayName);
			this.wszDatabaseDisplayName = IntPtr.Zero;
			Marshal.FreeHGlobal(this.wszDatabaseStreams);
			this.wszDatabaseStreams = IntPtr.Zero;
		}

		// Token: 0x04000014 RID: 20
		public IntPtr wszDatabaseDisplayName;

		// Token: 0x04000015 RID: 21
		public uint cwDatabaseStreams;

		// Token: 0x04000016 RID: 22
		public IntPtr wszDatabaseStreams;

		// Token: 0x04000017 RID: 23
		public Guid guidDatabase;

		// Token: 0x04000018 RID: 24
		public uint ulIconIndexDatabase;

		// Token: 0x04000019 RID: 25
		public uint fDatabaseFlags;
	}
}
