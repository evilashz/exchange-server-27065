using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000186 RID: 390
	internal abstract class SafeHandleZeroInvalid : SafeHandle
	{
		// Token: 0x06001080 RID: 4224 RVA: 0x0004F9A2 File Offset: 0x0004DBA2
		public SafeHandleZeroInvalid() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0004F9B0 File Offset: 0x0004DBB0
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
