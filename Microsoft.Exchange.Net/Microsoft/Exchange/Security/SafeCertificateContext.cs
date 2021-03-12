using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C82 RID: 3202
	internal sealed class SafeCertificateContext : DebugSafeHandle
	{
		// Token: 0x060046D1 RID: 18129 RVA: 0x000BE4EC File Offset: 0x000BC6EC
		internal unsafe SafeCertificateContext(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				IntPtr value = new IntPtr((void*)ptr);
				this.handle = *(IntPtr*)((void*)value);
			}
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x000BE533 File Offset: 0x000BC733
		protected override bool ReleaseHandle()
		{
			return SspiNativeMethods.CertFreeCertificateContext(this.handle);
		}
	}
}
