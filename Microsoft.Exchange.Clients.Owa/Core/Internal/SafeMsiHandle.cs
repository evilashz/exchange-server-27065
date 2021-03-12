using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Clients.Owa.Core.Internal
{
	// Token: 0x0200023C RID: 572
	[ComVisible(false)]
	internal sealed class SafeMsiHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001346 RID: 4934 RVA: 0x000777B5 File Offset: 0x000759B5
		private SafeMsiHandle() : base(true)
		{
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000777BE File Offset: 0x000759BE
		internal SafeMsiHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000777CE File Offset: 0x000759CE
		protected override bool ReleaseHandle()
		{
			return SafeNativeMethods.MsiCloseHandle(this.handle) == 0;
		}
	}
}
