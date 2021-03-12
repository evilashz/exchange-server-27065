using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000027 RID: 39
	internal struct NATIVE_INSTANCE_BACKUP_INFO
	{
		// Token: 0x06000333 RID: 819 RVA: 0x000088B4 File Offset: 0x00006AB4
		public unsafe void FreeNativeInstanceInfo()
		{
			NATIVE_DATABASE_BACKUP_INFO* ptr = (NATIVE_DATABASE_BACKUP_INFO*)((void*)this.rgDatabase);
			int num = 0;
			while ((long)num < (long)((ulong)this.cDatabase))
			{
				ptr[num].FreeNativeDatabaseBackupInfo();
				num++;
			}
			NATIVE_ESE_ICON_DESCRIPTION* ptr2 = (NATIVE_ESE_ICON_DESCRIPTION*)((void*)this.rgIconDescription);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)this.cIconDescription))
			{
				ptr2[num2].FreeNativeIconDescription();
				num2++;
			}
			Marshal.FreeHGlobal(this.wszInstanceName);
			this.wszInstanceName = IntPtr.Zero;
			Marshal.FreeHGlobal(this.rgDatabase);
			this.rgDatabase = IntPtr.Zero;
			Marshal.FreeHGlobal(this.rgIconDescription);
			this.rgIconDescription = IntPtr.Zero;
		}

		// Token: 0x04000071 RID: 113
		public long hInstanceId;

		// Token: 0x04000072 RID: 114
		public IntPtr wszInstanceName;

		// Token: 0x04000073 RID: 115
		public uint ulIconIndexInstance;

		// Token: 0x04000074 RID: 116
		public uint cDatabase;

		// Token: 0x04000075 RID: 117
		public IntPtr rgDatabase;

		// Token: 0x04000076 RID: 118
		public uint cIconDescription;

		// Token: 0x04000077 RID: 119
		public IntPtr rgIconDescription;
	}
}
