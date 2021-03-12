using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B3E RID: 2878
	internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003E09 RID: 15881 RVA: 0x000A229F File Offset: 0x000A049F
		public SafeFindHandle() : base(true)
		{
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000A22A8 File Offset: 0x000A04A8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeFindHandle.FindClose(this.handle);
		}

		// Token: 0x06003E0B RID: 15883
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FindClose([In] IntPtr handle);
	}
}
