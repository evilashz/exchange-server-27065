using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AD9 RID: 2777
	internal sealed class SafeCryptProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003BA3 RID: 15267 RVA: 0x000992F8 File Offset: 0x000974F8
		public SafeCryptProvHandle() : base(true)
		{
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x00099301 File Offset: 0x00097501
		internal SafeCryptProvHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x00099311 File Offset: 0x00097511
		public static SafeCryptProvHandle InvalidHandle
		{
			get
			{
				return new SafeCryptProvHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x0009931D File Offset: 0x0009751D
		protected override bool ReleaseHandle()
		{
			return CapiNativeMethods.CryptReleaseContext(this.handle, 0U);
		}
	}
}
