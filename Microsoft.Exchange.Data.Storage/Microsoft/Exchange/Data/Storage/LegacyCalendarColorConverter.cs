using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003D6 RID: 982
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class LegacyCalendarColorConverter
	{
		// Token: 0x06002C5E RID: 11358 RVA: 0x000B5884 File Offset: 0x000B3A84
		public static CalendarColor FromLegacyCalendarColor(LegacyCalendarColor legacyCalendarColor)
		{
			EnumValidator.ThrowIfInvalid<LegacyCalendarColor>(legacyCalendarColor, "legacyCalendarColor");
			if (legacyCalendarColor == LegacyCalendarColor.MaxColor)
			{
				throw new ArgumentException("MaxColor entry cannot be converted into a corresponding O15 calendar color.");
			}
			if (legacyCalendarColor == LegacyCalendarColor.Auto || legacyCalendarColor == LegacyCalendarColor.NoneSet)
			{
				return CalendarColor.Auto;
			}
			return (CalendarColor)LegacyCalendarColorConverter.LegacyToNewMappings[(int)legacyCalendarColor];
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000B58B3 File Offset: 0x000B3AB3
		public static LegacyCalendarColor ToLegacyCalendarColor(CalendarColor calendarColor)
		{
			EnumValidator.ThrowIfInvalid<CalendarColor>(calendarColor, "calendarColor");
			if (calendarColor == CalendarColor.MaxColor)
			{
				throw new ArgumentException("MaxColor entry cannot be converted into the corresponding Legacy (O12/14) calendar color.");
			}
			if (calendarColor == CalendarColor.Auto)
			{
				return LegacyCalendarColor.Auto;
			}
			return (LegacyCalendarColor)LegacyCalendarColorConverter.NewToLegacyMappings[(int)calendarColor];
		}

		// Token: 0x040018D6 RID: 6358
		private static readonly int[] LegacyToNewMappings = new int[]
		{
			0,
			1,
			8,
			3,
			5,
			6,
			1,
			8,
			2,
			6,
			7,
			1,
			5,
			4,
			0
		};

		// Token: 0x040018D7 RID: 6359
		private static readonly int[] NewToLegacyMappings = new int[]
		{
			0,
			1,
			8,
			3,
			13,
			4,
			5,
			10,
			2
		};
	}
}
