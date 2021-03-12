using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003BF RID: 959
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DailyRecurrencePattern : IntervalRecurrencePattern
	{
		// Token: 0x06002BB0 RID: 11184 RVA: 0x000AE1EB File Offset: 0x000AC3EB
		public DailyRecurrencePattern()
		{
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000AE1F3 File Offset: 0x000AC3F3
		public DailyRecurrencePattern(int recurrenceInterval)
		{
			base.RecurrenceInterval = recurrenceInterval;
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000AE202 File Offset: 0x000AC402
		public override bool Equals(RecurrencePattern value, bool ignoreCalendarTypeAndIsLeapMonth)
		{
			return value is DailyRecurrencePattern && base.Equals(value, ignoreCalendarTypeAndIsLeapMonth);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000AE218 File Offset: 0x000AC418
		internal override LocalizedString When()
		{
			LocalizedString result;
			if (base.RecurrenceInterval == 1)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					result = ClientStrings.CalendarWhenDailyEveryDay;
				}
				else
				{
					result = ClientStrings.TaskWhenDailyEveryDay;
				}
			}
			else if (base.RecurrenceInterval == 2)
			{
				if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
				{
					result = ClientStrings.CalendarWhenEveryOtherDay;
				}
				else
				{
					result = ClientStrings.TaskWhenEveryOtherDay;
				}
			}
			else if (base.RecurrenceObjectType == RecurrenceObjectType.CalendarRecurrence)
			{
				result = ClientStrings.CalendarWhenDailyEveryNDays(base.RecurrenceInterval);
			}
			else
			{
				result = ClientStrings.TaskWhenDailyEveryNDays(base.RecurrenceInterval);
			}
			return result;
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x000AE28A File Offset: 0x000AC48A
		internal override RecurrenceType MapiRecurrenceType
		{
			get
			{
				return RecurrenceType.Daily;
			}
		}
	}
}
