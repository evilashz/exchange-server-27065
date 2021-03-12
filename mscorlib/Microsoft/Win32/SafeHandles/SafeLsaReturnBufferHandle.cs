using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002A RID: 42
	[SecurityCritical]
	internal sealed class SafeLsaReturnBufferHandle : SafeBuffer
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000049F0 File Offset: 0x00002BF0
		private SafeLsaReturnBufferHandle() : base(true)
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000049F9 File Offset: 0x00002BF9
		internal SafeLsaReturnBufferHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004A09 File Offset: 0x00002C09
		internal static SafeLsaReturnBufferHandle InvalidHandle
		{
			get
			{
				return new SafeLsaReturnBufferHandle(IntPtr.Zero);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004A15 File Offset: 0x00002C15
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaFreeReturnBuffer(this.handle) >= 0;
		}
	}
}
