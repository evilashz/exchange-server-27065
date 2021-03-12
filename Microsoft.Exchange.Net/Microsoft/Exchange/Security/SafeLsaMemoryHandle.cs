using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000ADB RID: 2779
	internal sealed class SafeLsaMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003BAE RID: 15278 RVA: 0x00099433 File Offset: 0x00097633
		internal SafeLsaMemoryHandle() : base(true)
		{
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x0009943C File Offset: 0x0009763C
		internal SafeLsaMemoryHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x0009944C File Offset: 0x0009764C
		protected override bool ReleaseHandle()
		{
			return SafeLsaMemoryHandle.LsaFreeMemory(this.handle) == 0;
		}

		// Token: 0x06003BB1 RID: 15281
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", ExactSpelling = true)]
		private static extern int LsaFreeMemory(IntPtr handle);
	}
}
