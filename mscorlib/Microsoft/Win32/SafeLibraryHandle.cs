using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000019 RID: 25
	[SecurityCritical]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000156 RID: 342 RVA: 0x000046F9 File Offset: 0x000028F9
		internal SafeLibraryHandle() : base(true)
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004702 File Offset: 0x00002902
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.FreeLibrary(this.handle);
		}
	}
}
