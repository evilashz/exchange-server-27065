using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B3D RID: 2877
	internal sealed class SafeDsNameResultHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x000A2262 File Offset: 0x000A0462
		public NativeMethods.DSNameResult GetNameResult()
		{
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("GetNameResult() called on an invalid handle.");
			}
			return (NativeMethods.DSNameResult)Marshal.PtrToStructure(this.handle, typeof(NativeMethods.DSNameResult));
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000A2291 File Offset: 0x000A0491
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			SafeDsNameResultHandle.DsFreeNameResult(this.handle);
			return true;
		}

		// Token: 0x06003E08 RID: 15880
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("NTDSAPI.DLL", CharSet = CharSet.Unicode)]
		private static extern void DsFreeNameResult(IntPtr dsNameResultHandle);
	}
}
