using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000509 RID: 1289
	public class Work
	{
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x00123063 File Offset: 0x00121263
		// (set) Token: 0x0600314B RID: 12619 RVA: 0x0012306B File Offset: 0x0012126B
		public float WorkAmount
		{
			get
			{
				return this.workAmount;
			}
			set
			{
				this.workAmount = value;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x00123074 File Offset: 0x00121274
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x0012307C File Offset: 0x0012127C
		public DurationUnit WorkUnit
		{
			get
			{
				return this.workUnit;
			}
			set
			{
				this.workUnit = value;
			}
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00123085 File Offset: 0x00121285
		internal Work(float workAmount, DurationUnit workUnit)
		{
			this.workAmount = workAmount;
			this.workUnit = workUnit;
		}

		// Token: 0x04002208 RID: 8712
		private float workAmount;

		// Token: 0x04002209 RID: 8713
		private DurationUnit workUnit;
	}
}
