using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200057D RID: 1405
	[SecurityCritical]
	internal sealed class CleanupWorkListElement
	{
		// Token: 0x0600420E RID: 16910 RVA: 0x000F5925 File Offset: 0x000F3B25
		public CleanupWorkListElement(SafeHandle handle)
		{
			this.m_handle = handle;
		}

		// Token: 0x04001B30 RID: 6960
		public SafeHandle m_handle;

		// Token: 0x04001B31 RID: 6961
		public bool m_owned;
	}
}
