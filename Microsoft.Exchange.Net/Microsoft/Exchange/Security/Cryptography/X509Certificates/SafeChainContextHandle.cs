using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AD6 RID: 2774
	internal sealed class SafeChainContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003B97 RID: 15255 RVA: 0x0009923A File Offset: 0x0009743A
		internal SafeChainContextHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x0009924A File Offset: 0x0009744A
		private SafeChainContextHandle() : base(true)
		{
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x00099253 File Offset: 0x00097453
		protected override bool ReleaseHandle()
		{
			CapiNativeMethods.CertFreeCertificateChain(this.handle);
			return true;
		}
	}
}
