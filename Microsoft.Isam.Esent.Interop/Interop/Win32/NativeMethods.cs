using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Win32
{
	// Token: 0x0200030A RID: 778
	internal static class NativeMethods
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x0001C9E1 File Offset: 0x0001ABE1
		public static void ThrowExceptionOnNull(IntPtr ptr, string message)
		{
			if (IntPtr.Zero == ptr)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error(), message);
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		public static void ThrowExceptionOnFailure(bool success, string message)
		{
			if (!success)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error(), message);
			}
		}

		// Token: 0x06000E31 RID: 3633
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr VirtualAlloc(IntPtr plAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

		// Token: 0x06000E32 RID: 3634
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool VirtualFree(IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

		// Token: 0x06000E33 RID: 3635
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll")]
		public static extern IntPtr LocalAlloc(int uFlags, UIntPtr sizetdwBytes);

		// Token: 0x06000E34 RID: 3636
		[DllImport("kernel32.dll")]
		public static extern IntPtr LocalFree(IntPtr hglobal);

		// Token: 0x04000985 RID: 2437
		private const string WinCoreMemoryDll = "kernel32.dll";

		// Token: 0x04000986 RID: 2438
		private const string HeapObsolete = "kernel32.dll";
	}
}
