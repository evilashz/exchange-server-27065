using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200019C RID: 412
	internal class ScheduleBuilder
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0002B306 File Offset: 0x00029506
		internal Schedule Schedule
		{
			get
			{
				return Schedule.FromByteArray(this.schedule);
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002B313 File Offset: 0x00029513
		internal ScheduleBuilder() : this(Schedule.Never)
		{
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002B320 File Offset: 0x00029520
		internal ScheduleBuilder(Schedule schedule)
		{
			this.schedule = (schedule ?? Schedule.Never).ToByteArray();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002B340 File Offset: 0x00029540
		internal bool GetStateOfInterval(DayOfWeek day, int intervalOfDay)
		{
			int bitPosition = ScheduleBuilder.GetBitPosition(day, intervalOfDay);
			int num = bitPosition / 8;
			byte b = (byte)(128 >> bitPosition % 8);
			return 0 != (this.schedule[num] & b);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002B378 File Offset: 0x00029578
		internal void SetStateOfInterval(DayOfWeek day, int intervalOfDay, bool state)
		{
			int bitPosition = ScheduleBuilder.GetBitPosition(day, intervalOfDay);
			int num = bitPosition / 8;
			byte b = (byte)(128 >> bitPosition % 8);
			if (state)
			{
				byte[] array = this.schedule;
				int num2 = num;
				array[num2] |= b;
				return;
			}
			byte[] array2 = this.schedule;
			int num3 = num;
			array2[num3] &= ~b;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002B3DC File Offset: 0x000295DC
		internal bool GetStateOfInterval(int intervalOfDay)
		{
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
			{
				if (this.GetStateOfInterval(dayOfWeek, intervalOfDay))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0002B404 File Offset: 0x00029604
		internal void SetStateOfInterval(int intervalOfDay, bool state)
		{
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
			{
				this.SetStateOfInterval(dayOfWeek, intervalOfDay, state);
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002B428 File Offset: 0x00029628
		internal bool GetStateOfHour(DayOfWeek day, int hour)
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.GetStateOfInterval(day, hour * 4 + i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002B454 File Offset: 0x00029654
		internal void SetStateOfHour(DayOfWeek day, int hour, bool state)
		{
			for (int i = 0; i < 4; i++)
			{
				this.SetStateOfInterval(day, hour * 4 + i, state);
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002B47C File Offset: 0x0002967C
		internal bool GetStateOfHour(int hour)
		{
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
			{
				if (this.GetStateOfHour(dayOfWeek, hour))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002B4A4 File Offset: 0x000296A4
		internal void SetStateOfHour(int hour, bool state)
		{
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
			{
				this.SetStateOfHour(dayOfWeek, hour, state);
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002B4C8 File Offset: 0x000296C8
		internal bool GetStateOfDay(DayOfWeek day)
		{
			for (int i = 0; i < 96; i++)
			{
				if (this.GetStateOfInterval(day, i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002B4F0 File Offset: 0x000296F0
		internal void SetStateOfDay(DayOfWeek day, bool state)
		{
			for (int i = 0; i < 96; i++)
			{
				this.SetStateOfInterval(day, i, state);
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002B514 File Offset: 0x00029714
		internal void SetEntireState(bool[] bitMap)
		{
			if (bitMap == null)
			{
				throw new ArgumentNullException("bitMap");
			}
			if (bitMap.Length != 672)
			{
				throw new ArgumentException(DataStrings.ErrorInputSchedulerBuilder(bitMap.Length, 672), "bitMap");
			}
			int num = 0;
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
			{
				for (int i = 0; i < 96; i++)
				{
					this.SetStateOfInterval(dayOfWeek, i, bitMap[num]);
					num++;
				}
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002B580 File Offset: 0x00029780
		internal bool[] GetEntireState()
		{
			bool[] array = new bool[672];
			int num = 0;
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
			{
				for (int i = 0; i < 96; i++)
				{
					array[num] = this.GetStateOfInterval(dayOfWeek, i);
					num++;
				}
			}
			return array;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0002B5C4 File Offset: 0x000297C4
		private static int GetBitPosition(DayOfWeek day, int intervalOfDay)
		{
			int hour = intervalOfDay / 4;
			int minute = intervalOfDay % 4 * 15;
			WeekDayAndTime weekDayAndTime = new WeekDayAndTime(day, hour, minute).ToUniversalTime(ScheduleInterval.WeekBitmapReference);
			return (int)(weekDayAndTime.DayOfWeek * (DayOfWeek)96 + (weekDayAndTime.Hour * 60 + weekDayAndTime.Minute) / 15);
		}

		// Token: 0x0400083D RID: 2109
		internal const int IntervalTime = 15;

		// Token: 0x0400083E RID: 2110
		internal const int MinutesOfHour = 60;

		// Token: 0x0400083F RID: 2111
		internal const int IntervalsOfHour = 4;

		// Token: 0x04000840 RID: 2112
		internal const int HoursOfDay = 24;

		// Token: 0x04000841 RID: 2113
		internal const int IntervalsOfDay = 96;

		// Token: 0x04000842 RID: 2114
		internal const int DaysOfWeek = 7;

		// Token: 0x04000843 RID: 2115
		internal const int TotalBits = 672;

		// Token: 0x04000844 RID: 2116
		private byte[] schedule;
	}
}
