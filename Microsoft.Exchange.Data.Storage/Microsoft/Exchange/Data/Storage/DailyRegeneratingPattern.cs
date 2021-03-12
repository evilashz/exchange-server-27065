using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200040D RID: 1037
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DailyRegeneratingPattern : RegeneratingPattern
	{
		// Token: 0x06002F01 RID: 12033 RVA: 0x000C149B File Offset: 0x000BF69B
		public DailyRegeneratingPattern(int recurrenceInterval)
		{
			base.RecurrenceInterval = recurrenceInterval;
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000C14AA File Offset: 0x000BF6AA
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			return value is DailyRegeneratingPattern && base.Equals(value, ignoreCalendarTypeAndIsLeapMonth);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000C14BE File Offset: 0x000BF6BE
		internal override LocalizedString When()
		{
			if (base.RecurrenceInterval == 1)
			{
				return ClientStrings.TaskWhenDailyRegeneratingPattern;
			}
			return ClientStrings.TaskWhenNDaysRegeneratingPattern(base.RecurrenceInterval);
		}
	}
}
