using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x0200007E RID: 126
	internal sealed class SafeThreadHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x00011AA3 File Offset: 0x0000FCA3
		internal SafeThreadHandle() : base(true)
		{
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00011AAC File Offset: 0x0000FCAC
		public static SafeThreadHandle GetCurrentThreadHandle()
		{
			SafeThreadHandle result;
			using (SafeProcessHandle currentProcess = NativeMethods.GetCurrentProcess())
			{
				using (SafeThreadHandle currentThread = NativeMethods.GetCurrentThread())
				{
					SafeThreadHandle safeThreadHandle;
					if (NativeMethods.DuplicateThreadHandle(currentProcess, currentThread, currentProcess, out safeThreadHandle, 0U, false, 2U))
					{
						result = safeThreadHandle;
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00011B10 File Offset: 0x0000FD10
		public int CancelRpcRequest(int timeoutSeconds)
		{
			return NativeMethods.RpcCancelThreadEx(this, timeoutSeconds);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00011B19 File Offset: 0x0000FD19
		protected override bool ReleaseHandle()
		{
			return SafeThreadHandle.CloseHandle(this.handle);
		}

		// Token: 0x0600043F RID: 1087
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);
	}
}
