using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x0200007A RID: 122
	internal abstract class SafeHandleZeroIsInvalid : SafeHandle
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x000117EC File Offset: 0x0000F9EC
		public SafeHandleZeroIsInvalid() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000117FA File Offset: 0x0000F9FA
		protected SafeHandleZeroIsInvalid(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00011804 File Offset: 0x0000FA04
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
