using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001B RID: 27
	[SecurityCritical]
	internal sealed class SafeFileMappingHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00004735 File Offset: 0x00002935
		[SecurityCritical]
		internal SafeFileMappingHandle() : base(true)
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000473E File Offset: 0x0000293E
		[SecurityCritical]
		internal SafeFileMappingHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000474E File Offset: 0x0000294E
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
