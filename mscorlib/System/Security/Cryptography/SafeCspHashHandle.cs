using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023E RID: 574
	[SecurityCritical]
	internal sealed class SafeCspHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060020A2 RID: 8354 RVA: 0x000727BB File Offset: 0x000709BB
		private SafeCspHashHandle() : base(true)
		{
		}

		// Token: 0x060020A3 RID: 8355
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyHash(IntPtr hKey);

		// Token: 0x060020A4 RID: 8356 RVA: 0x000727C4 File Offset: 0x000709C4
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspHashHandle.CryptDestroyHash(this.handle);
		}
	}
}
