using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200015E RID: 350
	public static class ScheduleLocalizer
	{
		// Token: 0x040005A3 RID: 1443
		private static readonly DateTime MiniDateTime = DateTime.MinValue;

		// Token: 0x040005A4 RID: 1444
		public static readonly string RunWeekDaysFrom0800AMTo0500PM = Strings.FormatWeekDaysInterval(ScheduleLocalizer.MiniDateTime.AddHours(8.0), ScheduleLocalizer.MiniDateTime.AddHours(17.0));

		// Token: 0x040005A5 RID: 1445
		public static readonly string RunWeekDaysFrom0900AMTo0500PM = Strings.FormatWeekDaysInterval(ScheduleLocalizer.MiniDateTime.AddHours(9.0), ScheduleLocalizer.MiniDateTime.AddHours(17.0));

		// Token: 0x040005A6 RID: 1446
		public static readonly string RunWeekDaysFrom0900AMTo0600PM = Strings.FormatWeekDaysInterval(ScheduleLocalizer.MiniDateTime.AddHours(9.0), ScheduleLocalizer.MiniDateTime.AddHours(18.0));

		// Token: 0x040005A7 RID: 1447
		public static readonly string RunWeekDaysFrom0800AMTo1200PMAnd0100PMTo0500PM = Strings.FormatWeekDaysTwoIntervals(ScheduleLocalizer.MiniDateTime.AddHours(8.0), ScheduleLocalizer.MiniDateTime.AddHours(12.0), ScheduleLocalizer.MiniDateTime.AddHours(13.0), ScheduleLocalizer.MiniDateTime.AddHours(17.0));

		// Token: 0x040005A8 RID: 1448
		public static readonly string RunWeekDaysFrom0900AMTo1200PMAnd0100PMTo0600PM = Strings.FormatWeekDaysTwoIntervals(ScheduleLocalizer.MiniDateTime.AddHours(9.0), ScheduleLocalizer.MiniDateTime.AddHours(12.0), ScheduleLocalizer.MiniDateTime.AddHours(13.0), ScheduleLocalizer.MiniDateTime.AddHours(18.0));

		// Token: 0x040005A9 RID: 1449
		public static readonly string RunDailyFrom1100PMTo0300AM = Strings.FormatRunDailyFromTo(ScheduleLocalizer.MiniDateTime.AddHours(23.0), ScheduleLocalizer.MiniDateTime.AddHours(3.0));

		// Token: 0x040005AA RID: 1450
		public static readonly string RunDailyFromMidnightTo0400AM = Strings.FormatRunDailyFromMidnightTo(ScheduleLocalizer.MiniDateTime.AddHours(4.0));

		// Token: 0x040005AB RID: 1451
		public static readonly string RunDailyFrom0100AMTo0500AM = Strings.FormatRunDailyFromTo(ScheduleLocalizer.MiniDateTime.AddHours(1.0), ScheduleLocalizer.MiniDateTime.AddHours(5.0));

		// Token: 0x040005AC RID: 1452
		public static readonly string RunDailyFrom0200AMTo0600AM = Strings.FormatRunDailyFromTo(ScheduleLocalizer.MiniDateTime.AddHours(2.0), ScheduleLocalizer.MiniDateTime.AddHours(6.0));

		// Token: 0x040005AD RID: 1453
		public static readonly string RunDailyAt0100AM = Strings.RunDailyAt(ScheduleLocalizer.MiniDateTime.AddHours(1.0));

		// Token: 0x040005AE RID: 1454
		public static readonly string RunDailyAt0200AM = Strings.RunDailyAt(ScheduleLocalizer.MiniDateTime.AddHours(2.0));

		// Token: 0x040005AF RID: 1455
		public static readonly string RunDailyAt0300AM = Strings.RunDailyAt(ScheduleLocalizer.MiniDateTime.AddHours(3.0));

		// Token: 0x040005B0 RID: 1456
		public static readonly string RunDailyAt0400AM = Strings.RunDailyAt(ScheduleLocalizer.MiniDateTime.AddHours(4.0));

		// Token: 0x040005B1 RID: 1457
		public static readonly string RunDailyAt0500AM = Strings.RunDailyAt(ScheduleLocalizer.MiniDateTime.AddHours(5.0));
	}
}
