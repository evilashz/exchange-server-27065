using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000028 RID: 40
	[SecurityCritical]
	internal sealed class SafeLsaMemoryHandle : SafeBuffer
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00004986 File Offset: 0x00002B86
		private SafeLsaMemoryHandle() : base(true)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000498F File Offset: 0x00002B8F
		internal SafeLsaMemoryHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000499F File Offset: 0x00002B9F
		internal static SafeLsaMemoryHandle InvalidHandle
		{
			get
			{
				return new SafeLsaMemoryHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000049AB File Offset: 0x00002BAB
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaFreeMemory(this.handle) == 0;
		}
	}
}
