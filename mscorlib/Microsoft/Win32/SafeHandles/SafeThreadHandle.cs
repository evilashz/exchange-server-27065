using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002C RID: 44
	[SecurityCritical]
	internal sealed class SafeThreadHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00004A5A File Offset: 0x00002C5A
		private SafeThreadHandle() : base(true)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004A63 File Offset: 0x00002C63
		internal SafeThreadHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004A73 File Offset: 0x00002C73
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
