using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B3C RID: 2876
	internal sealed class SafeDsHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003E00 RID: 15872 RVA: 0x000A2227 File Offset: 0x000A0427
		internal SafeDsHandle() : base(true)
		{
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000A2230 File Offset: 0x000A0430
		internal SafeDsHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000A2240 File Offset: 0x000A0440
		internal SafeDsHandle(IntPtr handle) : this(handle, true)
		{
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000A224A File Offset: 0x000A044A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeDsHandle.DsUnBind(ref this.handle) == 0U;
		}

		// Token: 0x06003E04 RID: 15876
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("NTDSAPI.DLL")]
		private static extern uint DsUnBind([In] ref IntPtr handle);
	}
}
