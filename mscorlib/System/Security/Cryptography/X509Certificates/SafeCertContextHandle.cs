using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002A5 RID: 677
	[SecurityCritical]
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600241D RID: 9245 RVA: 0x00083B42 File Offset: 0x00081D42
		private SafeCertContextHandle() : base(true)
		{
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x00083B4B File Offset: 0x00081D4B
		internal SafeCertContextHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x00083B5C File Offset: 0x00081D5C
		internal static SafeCertContextHandle InvalidHandle
		{
			get
			{
				SafeCertContextHandle safeCertContextHandle = new SafeCertContextHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertContextHandle);
				return safeCertContextHandle;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x00083B7B File Offset: 0x00081D7B
		internal IntPtr pCertContext
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return Marshal.ReadIntPtr(this.handle);
			}
		}

		// Token: 0x06002421 RID: 9249
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreePCertContext(IntPtr pCert);

		// Token: 0x06002422 RID: 9250 RVA: 0x00083BA0 File Offset: 0x00081DA0
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeCertContextHandle._FreePCertContext(this.handle);
			return true;
		}
	}
}
