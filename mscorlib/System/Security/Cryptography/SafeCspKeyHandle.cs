using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023F RID: 575
	[SecurityCritical]
	internal sealed class SafeCspKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060020A5 RID: 8357 RVA: 0x000727D1 File Offset: 0x000709D1
		internal SafeCspKeyHandle() : base(true)
		{
		}

		// Token: 0x060020A6 RID: 8358
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyKey(IntPtr hKey);

		// Token: 0x060020A7 RID: 8359 RVA: 0x000727DA File Offset: 0x000709DA
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspKeyHandle.CryptDestroyKey(this.handle);
		}
	}
}
