using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AD4 RID: 2772
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003B8D RID: 15245 RVA: 0x00099183 File Offset: 0x00097383
		internal SafeCertContextHandle() : base(true)
		{
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x0009918C File Offset: 0x0009738C
		public SafeCertContextHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x0009919C File Offset: 0x0009739C
		public static SafeCertContextHandle Clone(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return new SafeCertContextHandle();
			}
			return CapiNativeMethods.CertDuplicateCertificateContext(handle);
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000991B7 File Offset: 0x000973B7
		protected override bool ReleaseHandle()
		{
			return CapiNativeMethods.CertFreeCertificateContext(this.handle);
		}
	}
}
