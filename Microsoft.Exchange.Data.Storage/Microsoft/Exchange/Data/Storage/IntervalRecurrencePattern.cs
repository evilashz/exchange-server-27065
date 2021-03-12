using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003BE RID: 958
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class IntervalRecurrencePattern : RecurrencePattern
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x000AE177 File Offset: 0x000AC377
		// (set) Token: 0x06002BAD RID: 11181 RVA: 0x000AE17F File Offset: 0x000AC37F
		public int RecurrenceInterval
		{
			get
			{
				return this.recurrenceInterval;
			}
			protected set
			{
				if (value < 1 || value > StorageLimits.Instance.RecurrenceMaximumInterval)
				{
					throw new ArgumentOutOfRangeException(ServerStrings.ExInvalidRecurrenceInterval(value), "RecurrenceInterval");
				}
				this.recurrenceInterval = value;
			}
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000AE1B0 File Offset: 0x000AC3B0
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			if (!(value is IntervalRecurrencePattern))
			{
				return false;
			}
			IntervalRecurrencePattern intervalRecurrencePattern = (IntervalRecurrencePattern)value;
			return intervalRecurrencePattern.RecurrenceInterval == this.recurrenceInterval;
		}

		// Token: 0x04001863 RID: 6243
		private int recurrenceInterval = 1;
	}
}
