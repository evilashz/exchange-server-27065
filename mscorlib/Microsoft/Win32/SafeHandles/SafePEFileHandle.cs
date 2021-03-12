using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001E RID: 30
	[SecurityCritical]
	internal sealed class SafePEFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000164 RID: 356 RVA: 0x000047AD File Offset: 0x000029AD
		private SafePEFileHandle() : base(true)
		{
		}

		// Token: 0x06000165 RID: 357
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ReleaseSafePEFileHandle(IntPtr peFile);

		// Token: 0x06000166 RID: 358 RVA: 0x000047B6 File Offset: 0x000029B6
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafePEFileHandle.ReleaseSafePEFileHandle(this.handle);
			return true;
		}
	}
}
