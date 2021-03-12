using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x02000190 RID: 400
	internal sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x000299B7 File Offset: 0x00027BB7
		internal SafeTokenHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000299C7 File Offset: 0x00027BC7
		private SafeTokenHandle() : base(true)
		{
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x000299D0 File Offset: 0x00027BD0
		internal static SafeTokenHandle InvalidHandle
		{
			get
			{
				return new SafeTokenHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000299DC File Offset: 0x00027BDC
		protected override bool ReleaseHandle()
		{
			return NativeMethods.CloseHandle(this.handle);
		}
	}
}
