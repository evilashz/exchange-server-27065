using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200008D RID: 141
	[SuppressUnmanagedCodeSecurity]
	[ComVisible(false)]
	internal class Win32
	{
		// Token: 0x060004D7 RID: 1239
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DuplicateHandle(SafeHandle sourceProcessHandle, IntPtr sourceHandle, SafeHandle targetProcessHandle, ref IntPtr targetHandle, uint desiredAccess, bool inheritHandle, Win32.DuplicateHandleOptions options);

		// Token: 0x060004D8 RID: 1240
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern SafeProcessHandle GetCurrentProcess();

		// Token: 0x060004D9 RID: 1241
		[DllImport("msvcrt.dll")]
		internal static extern int memcmp(byte[] a, byte[] b, long count);

		// Token: 0x0200008E RID: 142
		internal enum DuplicateHandleOptions : uint
		{
			// Token: 0x0400053C RID: 1340
			DUPLICATE_SAME_ACCESS = 2U
		}

		// Token: 0x0200008F RID: 143
		internal enum Win32ErrorCodes : uint
		{
			// Token: 0x0400053E RID: 1342
			ERROR_LOGON_FAILURE = 1326U
		}
	}
}
