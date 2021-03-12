using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009DA RID: 2522
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class FirstWeekRulesExtensions
	{
		// Token: 0x06005CE0 RID: 23776 RVA: 0x00184500 File Offset: 0x00182700
		public static CalendarWeekRule ToCalendarWeekRule(this FirstWeekRules firstWeekRule)
		{
			switch (firstWeekRule)
			{
			case FirstWeekRules.FirstFourDayWeek:
				return CalendarWeekRule.FirstFourDayWeek;
			case FirstWeekRules.FirstFullWeek:
				return CalendarWeekRule.FirstFullWeek;
			}
			return CalendarWeekRule.FirstDay;
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x0018452C File Offset: 0x0018272C
		public static FirstWeekRules ToFirstWeekRules(this CalendarWeekRule calendarWeekRule)
		{
			switch (calendarWeekRule)
			{
			case CalendarWeekRule.FirstFullWeek:
				return FirstWeekRules.FirstFullWeek;
			case CalendarWeekRule.FirstFourDayWeek:
				return FirstWeekRules.FirstFourDayWeek;
			}
			return FirstWeekRules.FirstDay;
		}
	}
}
