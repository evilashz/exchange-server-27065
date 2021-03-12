using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000021 RID: 33
	[ComVisible(false)]
	[CLSCompliant(false)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public sealed class AuthzContextHandle : DisposeTrackableSafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00004C7D File Offset: 0x00002E7D
		private AuthzContextHandle()
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004C85 File Offset: 0x00002E85
		internal AuthzContextHandle(IntPtr authenticatedUserHandle) : base(authenticatedUserHandle)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004C8E File Offset: 0x00002E8E
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AuthzContextHandle>(this);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004C96 File Offset: 0x00002E96
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || NativeMethods.AuthzFreeContext(this.handle);
		}
	}
}
