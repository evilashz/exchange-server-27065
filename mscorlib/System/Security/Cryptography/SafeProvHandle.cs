using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200028C RID: 652
	[SecurityCritical]
	internal sealed class SafeProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002334 RID: 9012 RVA: 0x0007FC02 File Offset: 0x0007DE02
		private SafeProvHandle() : base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x0007FC16 File Offset: 0x0007DE16
		private SafeProvHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x0007FC26 File Offset: 0x0007DE26
		internal static SafeProvHandle InvalidHandle
		{
			get
			{
				return new SafeProvHandle();
			}
		}

		// Token: 0x06002337 RID: 9015
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeCsp(IntPtr pProviderContext);

		// Token: 0x06002338 RID: 9016 RVA: 0x0007FC2D File Offset: 0x0007DE2D
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeProvHandle.FreeCsp(this.handle);
			return true;
		}
	}
}
