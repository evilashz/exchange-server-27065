using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200040E RID: 1038
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WeeklyRegeneratingPattern : RegeneratingPattern
	{
		// Token: 0x06002F04 RID: 12036 RVA: 0x000C14DA File Offset: 0x000BF6DA
		public WeeklyRegeneratingPattern(int recurrenceInterval)
		{
			base.RecurrenceInterval = recurrenceInterval;
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000C14E9 File Offset: 0x000BF6E9
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			return value is WeeklyRegeneratingPattern && base.Equals(value, ignoreCalendarTypeAndIsLeapMonth);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000C14FD File Offset: 0x000BF6FD
		internal override LocalizedString When()
		{
			if (base.RecurrenceInterval == 1)
			{
				return ClientStrings.TaskWhenWeeklyRegeneratingPattern;
			}
			return ClientStrings.TaskWhenNWeeksRegeneratingPattern(base.RecurrenceInterval);
		}
	}
}
