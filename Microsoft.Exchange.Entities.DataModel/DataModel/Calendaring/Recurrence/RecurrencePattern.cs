using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Serialization;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x02000068 RID: 104
	public abstract class RecurrencePattern
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000311 RID: 785
		public abstract RecurrencePatternType Type { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000064E2 File Offset: 0x000046E2
		// (set) Token: 0x06000313 RID: 787 RVA: 0x000064EA File Offset: 0x000046EA
		public int Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value > RecurrencePattern.MaxInterval)
				{
					throw new ValueOutOfRangeException("Interval", value);
				}
				this.interval = value;
			}
		}

		// Token: 0x04000173 RID: 371
		public static readonly int MaxInterval = StorageLimits.Instance.RecurrenceMaximumInterval;

		// Token: 0x04000174 RID: 372
		private int interval;
	}
}
