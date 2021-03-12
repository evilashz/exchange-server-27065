using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000079 RID: 121
	internal sealed class IoCompletionPort : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x000116D3 File Offset: 0x0000F8D3
		private IoCompletionPort() : base(true)
		{
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000116DC File Offset: 0x0000F8DC
		public IoCompletionPort(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000116EC File Offset: 0x0000F8EC
		public bool GetQueuedCompletionStatus(out uint limitClass, out UIntPtr completionKey, out int processId, uint milliseconds)
		{
			processId = 0;
			IntPtr intPtr;
			if (NativeMethods.GetQueuedCompletionStatus(this, out limitClass, out completionKey, out intPtr, milliseconds))
			{
				processId = intPtr.ToInt32();
				return true;
			}
			NativeMethods.ErrorCode lastWin32Error = (NativeMethods.ErrorCode)Marshal.GetLastWin32Error();
			if (lastWin32Error == NativeMethods.ErrorCode.WaitTimeout)
			{
				return false;
			}
			throw new Win32Exception((int)lastWin32Error, "GetQueuedCompletionStatus failed with error code " + lastWin32Error);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00011740 File Offset: 0x0000F940
		public bool PostQueuedCompletionStatus(uint limitClass, uint completionKey)
		{
			IntPtr zero = IntPtr.Zero;
			if (!NativeMethods.PostQueuedCompletionStatus(this, limitClass, new UIntPtr(completionKey), zero))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(lastWin32Error, "PostQueuedCompletionStatus failed with error code " + lastWin32Error);
			}
			return true;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00011784 File Offset: 0x0000F984
		public bool PostQueuedCompletionStatus(uint limitClass, uint completionKey, IntPtr data)
		{
			if (!NativeMethods.PostQueuedCompletionStatus(this, limitClass, new UIntPtr(completionKey), data))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(lastWin32Error, "PostQueuedCompletionStatus failed with error code " + lastWin32Error);
			}
			return true;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000117C1 File Offset: 0x0000F9C1
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000117D3 File Offset: 0x0000F9D3
		public static IoCompletionPort InvalidHandle
		{
			get
			{
				return new IoCompletionPort(IntPtr.Zero);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000117DF File Offset: 0x0000F9DF
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return IoCompletionPort.CloseHandle(this.handle);
		}

		// Token: 0x06000423 RID: 1059
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);
	}
}
