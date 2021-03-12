using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x0200007F RID: 127
	internal sealed class SafeUserTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00011B26 File Offset: 0x0000FD26
		public SafeUserTokenHandle() : base(true)
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00011B2F File Offset: 0x0000FD2F
		public SafeUserTokenHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011B3F File Offset: 0x0000FD3F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeUserTokenHandle.CloseHandle(this.handle);
		}

		// Token: 0x06000443 RID: 1091
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x02000080 RID: 128
		internal enum LogonType
		{
			// Token: 0x0400020A RID: 522
			LogonService = 5
		}

		// Token: 0x02000081 RID: 129
		internal enum LogonProvider
		{
			// Token: 0x0400020C RID: 524
			Default
		}
	}
}
