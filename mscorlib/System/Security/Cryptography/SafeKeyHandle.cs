using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200028D RID: 653
	[SecurityCritical]
	internal sealed class SafeKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002339 RID: 9017 RVA: 0x0007FC3B File Offset: 0x0007DE3B
		private SafeKeyHandle() : base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0007FC4F File Offset: 0x0007DE4F
		private SafeKeyHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x0007FC5F File Offset: 0x0007DE5F
		internal static SafeKeyHandle InvalidHandle
		{
			get
			{
				return new SafeKeyHandle();
			}
		}

		// Token: 0x0600233C RID: 9020
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeKey(IntPtr pKeyCotext);

		// Token: 0x0600233D RID: 9021 RVA: 0x0007FC66 File Offset: 0x0007DE66
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeKeyHandle.FreeKey(this.handle);
			return true;
		}
	}
}
