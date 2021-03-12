using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200006A RID: 106
	internal class ModuleHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000DC26 File Offset: 0x0000BE26
		public ModuleHandle() : base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000DC3A File Offset: 0x0000BE3A
		protected override bool ReleaseHandle()
		{
			return NativeMethods.FreeLibrary(this.handle);
		}
	}
}
