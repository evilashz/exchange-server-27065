using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C83 RID: 3203
	internal sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060046D3 RID: 18131 RVA: 0x000BE540 File Offset: 0x000BC740
		internal SafeTokenHandle() : base(true)
		{
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x000BE549 File Offset: 0x000BC749
		internal SafeTokenHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x000BE559 File Offset: 0x000BC759
		protected override bool ReleaseHandle()
		{
			return SspiNativeMethods.CloseHandle(this.handle);
		}
	}
}
