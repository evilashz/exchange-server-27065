using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D8E RID: 3470
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Deadline
	{
		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x06007774 RID: 30580 RVA: 0x0020EBD2 File Offset: 0x0020CDD2
		public TimeSpan TimeLeft
		{
			get
			{
				return this.FinalTime - DateTime.UtcNow;
			}
		}

		// Token: 0x06007775 RID: 30581 RVA: 0x0020EBE4 File Offset: 0x0020CDE4
		private Deadline()
		{
		}

		// Token: 0x06007776 RID: 30582 RVA: 0x0020EBF8 File Offset: 0x0020CDF8
		public Deadline(TimeSpan maxTime)
		{
			this.FinalTime = DateTime.UtcNow.Add(maxTime);
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x06007777 RID: 30583 RVA: 0x0020EC2A File Offset: 0x0020CE2A
		public virtual bool IsOver
		{
			get
			{
				return DateTime.UtcNow >= this.FinalTime;
			}
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x0020EC3C File Offset: 0x0020CE3C
		public override string ToString()
		{
			return this.FinalTime.ToString();
		}

		// Token: 0x040052AE RID: 21166
		public static readonly Deadline NoDeadline = new Deadline();

		// Token: 0x040052AF RID: 21167
		private readonly DateTime FinalTime = DateTime.MaxValue;
	}
}
