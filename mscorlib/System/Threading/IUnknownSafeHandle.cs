using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004CF RID: 1231
	[SecurityCritical]
	internal class IUnknownSafeHandle : SafeHandle
	{
		// Token: 0x06003B25 RID: 15141 RVA: 0x000DF5BA File Offset: 0x000DD7BA
		public IUnknownSafeHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000DF5C8 File Offset: 0x000DD7C8
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000DF5DA File Offset: 0x000DD7DA
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			HostExecutionContextManager.ReleaseHostSecurityContext(this.handle);
			return true;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x000DF5EC File Offset: 0x000DD7EC
		internal object Clone()
		{
			IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
			if (!this.IsInvalid)
			{
				HostExecutionContextManager.CloneHostSecurityContext(this, unknownSafeHandle);
			}
			return unknownSafeHandle;
		}
	}
}
