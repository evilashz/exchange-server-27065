using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000A06 RID: 2566
	internal sealed class SystemWorkloadManagerEvent
	{
		// Token: 0x060076B9 RID: 30393 RVA: 0x00187194 File Offset: 0x00185394
		public SystemWorkloadManagerEvent(ResourceLoad load, int slots, bool delayed)
		{
			this.DateTime = TimeProvider.UtcNow;
			this.Load = load;
			this.Slots = slots;
			this.Delayed = delayed;
		}

		// Token: 0x17002A79 RID: 10873
		// (get) Token: 0x060076BA RID: 30394 RVA: 0x001871BC File Offset: 0x001853BC
		// (set) Token: 0x060076BB RID: 30395 RVA: 0x001871C4 File Offset: 0x001853C4
		public DateTime DateTime { get; private set; }

		// Token: 0x17002A7A RID: 10874
		// (get) Token: 0x060076BC RID: 30396 RVA: 0x001871CD File Offset: 0x001853CD
		// (set) Token: 0x060076BD RID: 30397 RVA: 0x001871D5 File Offset: 0x001853D5
		public ResourceLoad Load { get; private set; }

		// Token: 0x17002A7B RID: 10875
		// (get) Token: 0x060076BE RID: 30398 RVA: 0x001871DE File Offset: 0x001853DE
		// (set) Token: 0x060076BF RID: 30399 RVA: 0x001871E6 File Offset: 0x001853E6
		public int Slots { get; private set; }

		// Token: 0x17002A7C RID: 10876
		// (get) Token: 0x060076C0 RID: 30400 RVA: 0x001871EF File Offset: 0x001853EF
		// (set) Token: 0x060076C1 RID: 30401 RVA: 0x001871F7 File Offset: 0x001853F7
		public bool Delayed { get; private set; }

		// Token: 0x060076C2 RID: 30402 RVA: 0x00187200 File Offset: 0x00185400
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DateTime={0},Load={1}", this.DateTime, this.Load);
			if (this.Slots >= 0)
			{
				stringBuilder.AppendFormat(",Slots={0}", this.Slots);
			}
			if (this.Delayed)
			{
				stringBuilder.AppendFormat(",Delayed={0}", this.Delayed);
			}
			return stringBuilder.ToString();
		}
	}
}
