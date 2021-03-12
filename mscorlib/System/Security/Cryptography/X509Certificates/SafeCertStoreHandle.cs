using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002A6 RID: 678
	[SecurityCritical]
	internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002423 RID: 9251 RVA: 0x00083BAE File Offset: 0x00081DAE
		private SafeCertStoreHandle() : base(true)
		{
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00083BB7 File Offset: 0x00081DB7
		internal SafeCertStoreHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x00083BC8 File Offset: 0x00081DC8
		internal static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				SafeCertStoreHandle safeCertStoreHandle = new SafeCertStoreHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertStoreHandle);
				return safeCertStoreHandle;
			}
		}

		// Token: 0x06002426 RID: 9254
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreeCertStoreContext(IntPtr hCertStore);

		// Token: 0x06002427 RID: 9255 RVA: 0x00083BE7 File Offset: 0x00081DE7
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeCertStoreHandle._FreeCertStoreContext(this.handle);
			return true;
		}
	}
}
