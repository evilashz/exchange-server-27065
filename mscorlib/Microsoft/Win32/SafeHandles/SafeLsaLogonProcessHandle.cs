using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000027 RID: 39
	[SecurityCritical]
	internal sealed class SafeLsaLogonProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000494E File Offset: 0x00002B4E
		private SafeLsaLogonProcessHandle() : base(true)
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004957 File Offset: 0x00002B57
		internal SafeLsaLogonProcessHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00004967 File Offset: 0x00002B67
		internal static SafeLsaLogonProcessHandle InvalidHandle
		{
			get
			{
				return new SafeLsaLogonProcessHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004973 File Offset: 0x00002B73
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaDeregisterLogonProcess(this.handle) >= 0;
		}
	}
}
