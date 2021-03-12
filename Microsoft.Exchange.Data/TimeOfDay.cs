using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public class TimeOfDay
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0002F1D0 File Offset: 0x0002D3D0
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x0002F1D8 File Offset: 0x0002D3D8
		public TimeOfDayType TimeOfDayType { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0002F1E1 File Offset: 0x0002D3E1
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x0002F1E9 File Offset: 0x0002D3E9
		public DaysOfWeek CustomPeriodDays { get; set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0002F1F2 File Offset: 0x0002D3F2
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x0002F1FA File Offset: 0x0002D3FA
		public TimeSpan? CustomPeriodStartTime { get; set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0002F203 File Offset: 0x0002D403
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0002F20B File Offset: 0x0002D40B
		public TimeSpan? CustomPeriodEndTime { get; set; }

		// Token: 0x06000F89 RID: 3977 RVA: 0x0002F214 File Offset: 0x0002D414
		private TimeOfDay()
		{
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0002F21C File Offset: 0x0002D41C
		public TimeOfDay(TimeOfDayType timeOfDayType, DaysOfWeek daysOfWeek, TimeSpan? startTime, TimeSpan? endTime)
		{
			this.TimeOfDayType = timeOfDayType;
			this.CustomPeriodDays = daysOfWeek;
			this.CustomPeriodStartTime = startTime;
			this.CustomPeriodEndTime = endTime;
			this.Validate();
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002F248 File Offset: 0x0002D448
		public static TimeOfDay Parse(string timeOfDay)
		{
			if (string.IsNullOrEmpty(timeOfDay))
			{
				throw new FormatException(DataStrings.InvalidTimeOfDayFormat);
			}
			string[] array = timeOfDay.Split(new char[]
			{
				','
			});
			if (array == null || array.Length != 4)
			{
				throw new FormatException(DataStrings.InvalidTimeOfDayFormat);
			}
			TimeOfDayType timeOfDayType = (TimeOfDayType)CallerIdItem.ValidateEnumValue(array[0], "TimeOfDayType", 1, 3);
			DaysOfWeek daysOfWeek = (DaysOfWeek)CallerIdItem.ValidateEnumValue(array[1], "DaysOfWeek", 0, 127);
			switch (timeOfDayType)
			{
			case TimeOfDayType.WorkingHours:
			case TimeOfDayType.NonWorkingHours:
				if (daysOfWeek != DaysOfWeek.None || !string.IsNullOrEmpty(array[2]) || !string.IsNullOrEmpty(array[3]))
				{
					throw new FormatException(DataStrings.InvalidTimeOfDayFormatWorkingHours);
				}
				return new TimeOfDay(timeOfDayType, daysOfWeek, null, null);
			case TimeOfDayType.CustomPeriod:
				if (string.IsNullOrEmpty(array[2]) || string.IsNullOrEmpty(array[3]))
				{
					throw new FormatException(DataStrings.InvalidTimeOfDayFormatCustomWorkingHours);
				}
				return new TimeOfDay(timeOfDayType, daysOfWeek, new TimeSpan?(TimeSpan.Parse(array[2])), new TimeSpan?(TimeSpan.Parse(array[3])));
			default:
				throw new Exception("Unknown enum type.");
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0002F36C File Offset: 0x0002D56C
		public void Validate()
		{
			switch (this.TimeOfDayType)
			{
			case TimeOfDayType.WorkingHours:
			case TimeOfDayType.NonWorkingHours:
				if (this.CustomPeriodDays != DaysOfWeek.None || this.CustomPeriodStartTime != null || this.CustomPeriodEndTime != null)
				{
					throw new FormatException(DataStrings.InvalidTimeOfDayFormatWorkingHours);
				}
				break;
			case TimeOfDayType.CustomPeriod:
				if (this.CustomPeriodStartTime == null || this.CustomPeriodEndTime == null || this.CustomPeriodDays == DaysOfWeek.None || this.CustomPeriodStartTime.Value > this.CustomPeriodEndTime.Value || this.CustomPeriodStartTime.Value < TimeSpan.FromDays(0.0) || this.CustomPeriodStartTime.Value > TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L) || this.CustomPeriodEndTime.Value < TimeSpan.FromDays(0.0) || this.CustomPeriodEndTime.Value > TimeSpan.FromDays(1.0) - TimeSpan.FromTicks(1L) || this.CustomPeriodStartTime == this.CustomPeriodEndTime)
				{
					throw new FormatException(DataStrings.InvalidTimeOfDayFormatCustomWorkingHours);
				}
				break;
			default:
				throw new Exception("Unknown TimeOfDayType");
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0002F53C File Offset: 0x0002D73C
		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3}", new object[]
			{
				(int)this.TimeOfDayType,
				(int)this.CustomPeriodDays,
				(this.CustomPeriodStartTime != null) ? this.CustomPeriodStartTime.Value.ToString() : string.Empty,
				(this.CustomPeriodEndTime != null) ? this.CustomPeriodEndTime.Value.ToString() : string.Empty
			});
		}

		// Token: 0x0400093E RID: 2366
		private const int RequiredNumberOfTokens = 4;
	}
}
