using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000026 RID: 38
	[SecurityCritical]
	public sealed class SafeAccessTokenHandle : SafeHandle
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000048EB File Offset: 0x00002AEB
		private SafeAccessTokenHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000048F9 File Offset: 0x00002AF9
		public SafeAccessTokenHandle(IntPtr handle) : base(IntPtr.Zero, true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000490E File Offset: 0x00002B0E
		public static SafeAccessTokenHandle InvalidHandle
		{
			[SecurityCritical]
			get
			{
				return new SafeAccessTokenHandle(IntPtr.Zero);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000491A File Offset: 0x00002B1A
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero || this.handle == new IntPtr(-1);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004941 File Offset: 0x00002B41
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
