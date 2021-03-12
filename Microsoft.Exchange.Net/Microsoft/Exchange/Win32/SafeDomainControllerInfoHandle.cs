using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B3B RID: 2875
	internal sealed class SafeDomainControllerInfoHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06003DFD RID: 15869 RVA: 0x000A21E8 File Offset: 0x000A03E8
		public NativeMethods.DomainControllerInformation GetDomainControllerInfo()
		{
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("GetDomainControllerInfo() called on an invalid handle.");
			}
			return (NativeMethods.DomainControllerInformation)Marshal.PtrToStructure(this.handle, typeof(NativeMethods.DomainControllerInformation));
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000A2217 File Offset: 0x000A0417
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeDomainControllerInfoHandle.NetApiBufferFree(this.handle) == 0;
		}

		// Token: 0x06003DFF RID: 15871
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("NETAPI32.DLL")]
		private static extern int NetApiBufferFree(IntPtr ptr);
	}
}
