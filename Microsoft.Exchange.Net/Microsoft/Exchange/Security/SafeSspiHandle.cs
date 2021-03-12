using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C7E RID: 3198
	internal abstract class SafeSspiHandle : DebugSafeHandle
	{
		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x000BE462 File Offset: 0x000BC662
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this.SspiHandle.IsZero;
			}
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x000BE479 File Offset: 0x000BC679
		public override string ToString()
		{
			return this.SspiHandle.ToString();
		}

		// Token: 0x04003BB2 RID: 15282
		public SspiHandle SspiHandle;
	}
}
