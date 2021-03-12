using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001C RID: 28
	[SecurityCritical]
	internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600015E RID: 350 RVA: 0x0000475B File Offset: 0x0000295B
		[SecurityCritical]
		internal SafeFindHandle() : base(true)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004764 File Offset: 0x00002964
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.FindClose(this.handle);
		}
	}
}
