using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001A RID: 26
	[SecurityCritical]
	public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000158 RID: 344 RVA: 0x0000470F File Offset: 0x0000290F
		private SafeFileHandle() : base(true)
		{
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004718 File Offset: 0x00002918
		public SafeFileHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004728 File Offset: 0x00002928
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
