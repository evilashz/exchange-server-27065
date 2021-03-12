using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200028E RID: 654
	[SecurityCritical]
	internal sealed class SafeHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600233E RID: 9022 RVA: 0x0007FC74 File Offset: 0x0007DE74
		private SafeHashHandle() : base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0007FC88 File Offset: 0x0007DE88
		private SafeHashHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x0007FC98 File Offset: 0x0007DE98
		internal static SafeHashHandle InvalidHandle
		{
			get
			{
				return new SafeHashHandle();
			}
		}

		// Token: 0x06002341 RID: 9025
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeHash(IntPtr pHashContext);

		// Token: 0x06002342 RID: 9026 RVA: 0x0007FC9F File Offset: 0x0007DE9F
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeHashHandle.FreeHash(this.handle);
			return true;
		}
	}
}
