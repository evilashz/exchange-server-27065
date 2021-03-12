using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AD8 RID: 2776
	internal sealed class SafeCryptKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003BA0 RID: 15264 RVA: 0x000992D2 File Offset: 0x000974D2
		internal SafeCryptKeyHandle() : base(true)
		{
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000992DB File Offset: 0x000974DB
		public SafeCryptKeyHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000992EB File Offset: 0x000974EB
		protected override bool ReleaseHandle()
		{
			return CapiNativeMethods.CryptDestroyKey(this.handle);
		}
	}
}
