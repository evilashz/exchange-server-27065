using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000082 RID: 130
	internal sealed class SafeViewOfFileHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00011B4C File Offset: 0x0000FD4C
		internal SafeViewOfFileHandle()
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00011B54 File Offset: 0x0000FD54
		protected override bool ReleaseHandle()
		{
			return SafeViewOfFileHandle.UnmapViewOfFile(this.handle);
		}

		// Token: 0x06000446 RID: 1094
		[DllImport("KERNEL32.DLL", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);
	}
}
