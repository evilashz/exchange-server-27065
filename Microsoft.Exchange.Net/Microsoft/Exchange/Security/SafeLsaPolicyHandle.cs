using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000ADC RID: 2780
	internal class SafeLsaPolicyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003BB2 RID: 15282 RVA: 0x0009945C File Offset: 0x0009765C
		internal SafeLsaPolicyHandle() : base(true)
		{
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x00099465 File Offset: 0x00097665
		internal SafeLsaPolicyHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x00099475 File Offset: 0x00097675
		protected override bool ReleaseHandle()
		{
			return SafeLsaPolicyHandle.LsaClose(this.handle) == 0;
		}

		// Token: 0x06003BB5 RID: 15285
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", ExactSpelling = true)]
		private static extern int LsaClose(IntPtr handle);
	}
}
