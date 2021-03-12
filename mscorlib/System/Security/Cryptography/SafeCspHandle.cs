using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023D RID: 573
	[SecurityCritical]
	internal sealed class SafeCspHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600209F RID: 8351 RVA: 0x000727A4 File Offset: 0x000709A4
		private SafeCspHandle() : base(true)
		{
		}

		// Token: 0x060020A0 RID: 8352
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

		// Token: 0x060020A1 RID: 8353 RVA: 0x000727AD File Offset: 0x000709AD
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspHandle.CryptReleaseContext(this.handle, 0);
		}
	}
}
