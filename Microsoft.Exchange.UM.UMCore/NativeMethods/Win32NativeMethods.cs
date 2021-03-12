using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.UM.NativeMethods
{
	// Token: 0x020002A8 RID: 680
	internal static class Win32NativeMethods
	{
		// Token: 0x0600149A RID: 5274
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcessHeap();

		// Token: 0x0600149B RID: 5275
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateIoCompletionPort(IntPtr FileHandle, IntPtr ExistingCompletionPort, IntPtr CompletionKey, uint NumberOfConcurrentThreads);

		// Token: 0x0600149C RID: 5276
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetQueuedCompletionStatus(IntPtr CompletionPort, ref uint lpNumberOfBytes, ref IntPtr lpCompletionKey, ref IntPtr lpOverlapped, int dwMilliseconds);

		// Token: 0x0600149D RID: 5277
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostQueuedCompletionStatus(IntPtr CompletionPort, uint dwNumberOfBytesTransferred, IntPtr lpCompletionKey, IntPtr lpOverlapped);

		// Token: 0x0600149E RID: 5278 RVA: 0x00059500 File Offset: 0x00057700
		public static bool HeapFree(IntPtr lpMem)
		{
			return !(lpMem != IntPtr.Zero) || Win32NativeMethods.HeapFree(Win32NativeMethods.GetProcessHeap(), 0U, lpMem);
		}

		// Token: 0x0600149F RID: 5279
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);
	}
}
