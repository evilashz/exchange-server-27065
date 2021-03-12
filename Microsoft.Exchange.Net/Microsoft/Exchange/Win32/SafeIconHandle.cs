using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B40 RID: 2880
	internal sealed class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003E0D RID: 15885 RVA: 0x000A22CB File Offset: 0x000A04CB
		private SafeIconHandle() : base(true)
		{
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000A22D4 File Offset: 0x000A04D4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeIconHandle.DestroyIcon(this.handle);
		}

		// Token: 0x06003E0F RID: 15887
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DestroyIcon(IntPtr handle);
	}
}
