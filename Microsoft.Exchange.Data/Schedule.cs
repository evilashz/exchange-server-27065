using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	public sealed class Schedule : IEquatable<Schedule>, ICollection, ICollection<object>, IEnumerable<object>, IEnumerable
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00029302 File Offset: 0x00027502
		public ReadOnlyCollection<ScheduleInterval> Intervals
		{
			get
			{
				if (this.readOnlyIntervals == null)
				{
					this.readOnlyIntervals = new ReadOnlyCollection<ScheduleInterval>(this.intervals);
				}
				return this.readOnlyIntervals;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00029323 File Offset: 0x00027523
		public ScheduleMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0002932B File Offset: 0x0002752B
		public string ScheduleName
		{
			get
			{
				return this.scheduleName;
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00029334 File Offset: 0x00027534
		static Schedule()
		{
			ScheduleInterval[] array = new ScheduleInterval[168];
			for (int i = 0; i < 7; i++)
			{
				DayOfWeek startDay = (DayOfWeek)i;
				DayOfWeek endDay = (DayOfWeek)i;
				for (int j = 0; j < 24; j++)
				{
					array[i * 24 + j] = new ScheduleInterval(startDay, j, 0, endDay, j, 15);
					array[i * 24 + j] = new ScheduleInterval(startDay, j, 30, endDay, j, 45);
				}
			}
			Schedule.EveryHalfHour = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.EveryHalfHour), array);
			ScheduleInterval[] array2 = new ScheduleInterval[168];
			for (int k = 0; k < 7; k++)
			{
				DayOfWeek startDay2 = (DayOfWeek)k;
				DayOfWeek endDay2 = (DayOfWeek)k;
				for (int l = 0; l < 24; l++)
				{
					array2[k * 24 + l] = new ScheduleInterval(startDay2, l, 0, endDay2, l, 15);
				}
			}
			Schedule.EveryHour = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.EveryHour), array2);
			ScheduleInterval[] array3 = new ScheduleInterval[84];
			for (int m = 0; m < 7; m++)
			{
				DayOfWeek startDay3 = (DayOfWeek)m;
				DayOfWeek endDay3 = (DayOfWeek)m;
				for (int n = 0; n < 12; n++)
				{
					array3[m * 12 + n] = new ScheduleInterval(startDay3, n * 2, 0, endDay3, n * 2, 15);
				}
			}
			Schedule.EveryTwoHours = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.EveryTwoHours), array3);
			ScheduleInterval[] array4 = new ScheduleInterval[42];
			for (int num = 0; num < 7; num++)
			{
				DayOfWeek startDay4 = (DayOfWeek)num;
				DayOfWeek endDay4 = (DayOfWeek)num;
				for (int num2 = 0; num2 < 6; num2++)
				{
					array4[num * 6 + num2] = new ScheduleInterval(startDay4, num2 * 4, 0, endDay4, num2 * 4, 15);
				}
			}
			Schedule.EveryFourHours = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.EveryFourHours), array4);
			Schedule.DailyFrom8AMTo5PMAtWeekDays = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom8AMTo5PMAtWeekDays), new ScheduleInterval[]
			{
				new ScheduleInterval(DayOfWeek.Monday, 8, 0, DayOfWeek.Monday, 17, 0),
				new ScheduleInterval(DayOfWeek.Tuesday, 8, 0, DayOfWeek.Tuesday, 17, 0),
				new ScheduleInterval(DayOfWeek.Wednesday, 8, 0, DayOfWeek.Wednesday, 17, 0),
				new ScheduleInterval(DayOfWeek.Thursday, 8, 0, DayOfWeek.Thursday, 17, 0),
				new ScheduleInterval(DayOfWeek.Friday, 8, 0, DayOfWeek.Friday, 17, 0)
			});
			Schedule.DailyFrom9AMTo5PMAtWeekDays = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom9AMTo5PMAtWeekDays), new ScheduleInterval[]
			{
				new ScheduleInterval(DayOfWeek.Monday, 9, 0, DayOfWeek.Monday, 17, 0),
				new ScheduleInterval(DayOfWeek.Tuesday, 9, 0, DayOfWeek.Tuesday, 17, 0),
				new ScheduleInterval(DayOfWeek.Wednesday, 9, 0, DayOfWeek.Wednesday, 17, 0),
				new ScheduleInterval(DayOfWeek.Thursday, 9, 0, DayOfWeek.Thursday, 17, 0),
				new ScheduleInterval(DayOfWeek.Friday, 9, 0, DayOfWeek.Friday, 17, 0)
			});
			Schedule.DailyFrom9AMTo6PMAtWeekDays = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom9AMTo6PMAtWeekDays), new ScheduleInterval[]
			{
				new ScheduleInterval(DayOfWeek.Monday, 9, 0, DayOfWeek.Monday, 18, 0),
				new ScheduleInterval(DayOfWeek.Tuesday, 9, 0, DayOfWeek.Tuesday, 18, 0),
				new ScheduleInterval(DayOfWeek.Wednesday, 9, 0, DayOfWeek.Wednesday, 18, 0),
				new ScheduleInterval(DayOfWeek.Thursday, 9, 0, DayOfWeek.Thursday, 18, 0),
				new ScheduleInterval(DayOfWeek.Friday, 9, 0, DayOfWeek.Friday, 18, 0)
			});
			Schedule.DailyFrom8AMTo12PMAnd1PMTo5PMAtWeekDays = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom8AMTo12PMAnd1PMTo5PMAtWeekDays), new ScheduleInterval[]
			{
				new ScheduleInterval(DayOfWeek.Monday, 8, 0, DayOfWeek.Monday, 12, 0),
				new ScheduleInterval(DayOfWeek.Tuesday, 8, 0, DayOfWeek.Tuesday, 12, 0),
				new ScheduleInterval(DayOfWeek.Wednesday, 8, 0, DayOfWeek.Wednesday, 12, 0),
				new ScheduleInterval(DayOfWeek.Thursday, 8, 0, DayOfWeek.Thursday, 12, 0),
				new ScheduleInterval(DayOfWeek.Friday, 8, 0, DayOfWeek.Friday, 12, 0),
				new ScheduleInterval(DayOfWeek.Monday, 13, 0, DayOfWeek.Monday, 17, 0),
				new ScheduleInterval(DayOfWeek.Tuesday, 13, 0, DayOfWeek.Tuesday, 17, 0),
				new ScheduleInterval(DayOfWeek.Wednesday, 13, 0, DayOfWeek.Wednesday, 17, 0),
				new ScheduleInterval(DayOfWeek.Thursday, 13, 0, DayOfWeek.Thursday, 17, 0),
				new ScheduleInterval(DayOfWeek.Friday, 13, 0, DayOfWeek.Friday, 17, 0)
			});
			Schedule.DailyFrom9AMTo12PMAnd1PMTo6PMAtWeekDays = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom9AMTo12PMAnd1PMTo6PMAtWeekDays), new ScheduleInterval[]
			{
				new ScheduleInterval(DayOfWeek.Monday, 9, 0, DayOfWeek.Monday, 12, 0),
				new ScheduleInterval(DayOfWeek.Tuesday, 9, 0, DayOfWeek.Tuesday, 12, 0),
				new ScheduleInterval(DayOfWeek.Wednesday, 9, 0, DayOfWeek.Wednesday, 12, 0),
				new ScheduleInterval(DayOfWeek.Thursday, 9, 0, DayOfWeek.Thursday, 12, 0),
				new ScheduleInterval(DayOfWeek.Friday, 9, 0, DayOfWeek.Friday, 12, 0),
				new ScheduleInterval(DayOfWeek.Monday, 13, 0, DayOfWeek.Monday, 18, 0),
				new ScheduleInterval(DayOfWeek.Tuesday, 13, 0, DayOfWeek.Tuesday, 18, 0),
				new ScheduleInterval(DayOfWeek.Wednesday, 13, 0, DayOfWeek.Wednesday, 18, 0),
				new ScheduleInterval(DayOfWeek.Thursday, 13, 0, DayOfWeek.Thursday, 18, 0),
				new ScheduleInterval(DayOfWeek.Friday, 13, 0, DayOfWeek.Friday, 18, 0)
			});
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002A704 File Offset: 0x00028904
		private static Schedule CreateCustomSchedule(string scheduleName, params ScheduleInterval[] intervals)
		{
			Schedule schedule = new Schedule(intervals, ScheduleMode.ScheduledTimes, true, scheduleName);
			if (scheduleName != null)
			{
				Schedule.customSchedules.Add(scheduleName, schedule);
			}
			return schedule;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002A72B File Offset: 0x0002892B
		private Schedule(ScheduleInterval[] intervals, ScheduleMode mode, bool normalize, string scheduleName)
		{
			if (intervals == null)
			{
				this.intervals = new ScheduleInterval[0];
			}
			else
			{
				this.intervals = (normalize ? Schedule.NormalizeIntervals(intervals) : intervals);
			}
			this.mode = mode;
			this.scheduleName = scheduleName;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002A765 File Offset: 0x00028965
		private Schedule(ScheduleInterval[] intervals, ScheduleMode mode) : this(intervals, mode, true, null)
		{
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002A771 File Offset: 0x00028971
		public Schedule(ICollection intervals) : this(Schedule.ConvertCollection(intervals))
		{
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002A780 File Offset: 0x00028980
		private static ScheduleInterval[] ConvertCollection(ICollection values)
		{
			List<ScheduleInterval> list = new List<ScheduleInterval>();
			ArrayList arrayList = new ArrayList(values);
			for (int i = 0; i < arrayList.Count; i++)
			{
				string text = arrayList[i] as string;
				if (text != null)
				{
					list.AddRange(Schedule.Parse(text).Intervals);
				}
				else
				{
					if (arrayList[i] == null)
					{
						throw new ArgumentNullException("value[" + i + "]");
					}
					if (!(arrayList[i] is ScheduleInterval))
					{
						throw new ArgumentException(DataStrings.InvalidTypeArgumentExceptionMultipleExceptedTypes("value[" + i + "]", arrayList[i].GetType(), typeof(ScheduleInterval), typeof(string)), "value[" + i + "]");
					}
					list.Add((ScheduleInterval)arrayList[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002A87B File Offset: 0x00028A7B
		public Schedule(ScheduleInterval[] intervals) : this(Schedule.NormalizeIntervals(intervals), Schedule.CalculateMode(Schedule.NormalizeIntervals(intervals)))
		{
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002A894 File Offset: 0x00028A94
		public Schedule(ScheduleInterval[] intervals, bool normalize) : this(intervals, Schedule.CalculateMode(normalize ? Schedule.NormalizeIntervals(intervals) : intervals), normalize, null)
		{
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002A8B0 File Offset: 0x00028AB0
		private static ScheduleMode CalculateMode(ScheduleInterval[] intervals)
		{
			if (intervals.Length == 0)
			{
				return ScheduleMode.Never;
			}
			if (intervals.Length == 2 && intervals[0].CompareTo(Schedule.Always.intervals[0]) == 0 && intervals[1].CompareTo(Schedule.Always.intervals[1]) == 0)
			{
				return ScheduleMode.Always;
			}
			return ScheduleMode.ScheduledTimes;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002A913 File Offset: 0x00028B13
		private static ScheduleInterval[] NormalizeIntervals(ScheduleInterval[] intervals)
		{
			return ScheduleInterval.GetIntervalsFromWeekBitmap(ScheduleInterval.GetWeekBitmapFromIntervals(intervals));
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002A920 File Offset: 0x00028B20
		public ExDateTime GetNextScheduledTime()
		{
			if (this.Mode == ScheduleMode.Never)
			{
				return Schedule.MaximumValidDateTime;
			}
			if (this.Mode == ScheduleMode.Always)
			{
				return ExDateTime.Now;
			}
			ExDateTime now = ExDateTime.Now;
			TimeSpan value = TimeSpan.MaxValue;
			foreach (ScheduleInterval scheduleInterval in this.Intervals)
			{
				ExDateTime exDateTime = new ExDateTime(ExTimeZone.CurrentTimeZone, now.Year, now.Month, now.Day, scheduleInterval.StartHour, scheduleInterval.StartMinute, 0);
				int num = scheduleInterval.StartDay - now.DayOfWeek;
				if (num < 0 || (num == 0 && scheduleInterval.StartHour < now.Hour) || (num == 0 && scheduleInterval.StartHour == now.Hour && scheduleInterval.StartMinute <= now.Minute))
				{
					num += 7;
				}
				exDateTime = exDateTime.AddDays((double)num);
				TimeSpan timeSpan = exDateTime.Subtract(now);
				if (timeSpan.Ticks < value.Ticks)
				{
					value = timeSpan;
				}
			}
			return now.Add(value);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002AA50 File Offset: 0x00028C50
		public ExDateTime GetLastScheduledTime()
		{
			if (this.Mode == ScheduleMode.Never)
			{
				return Schedule.MinimumValidDateTime;
			}
			if (this.Mode == ScheduleMode.Always)
			{
				return ExDateTime.Now;
			}
			ExDateTime now = ExDateTime.Now;
			TimeSpan value = TimeSpan.MaxValue;
			foreach (ScheduleInterval scheduleInterval in this.Intervals)
			{
				ExDateTime value2 = new ExDateTime(ExTimeZone.CurrentTimeZone, now.Year, now.Month, now.Day, scheduleInterval.StartHour, scheduleInterval.StartMinute, 0);
				int num = scheduleInterval.StartDay - now.DayOfWeek;
				if (num > 0 || (num == 0 && scheduleInterval.StartHour > now.Hour) || (num == 0 && scheduleInterval.StartHour == now.Hour && scheduleInterval.StartMinute >= now.Minute))
				{
					num -= 7;
				}
				value2 = value2.AddDays((double)num);
				TimeSpan timeSpan = now.Subtract(value2);
				if (timeSpan.Ticks < value.Ticks)
				{
					value = timeSpan;
				}
			}
			return now.Subtract(value);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002AB80 File Offset: 0x00028D80
		public static Schedule Parse(string s, bool handleCustomNames, bool normalize)
		{
			if (StringComparer.OrdinalIgnoreCase.Compare(s, ScheduleMode.Never.ToString()) == 0 || StringComparer.Ordinal.Compare(s, LocalizedDescriptionAttribute.FromEnum(typeof(ScheduleMode), ScheduleMode.Never)) == 0)
			{
				return Schedule.Never;
			}
			if (StringComparer.OrdinalIgnoreCase.Compare(s, ScheduleMode.Always.ToString()) == 0 || StringComparer.Ordinal.Compare(s, LocalizedDescriptionAttribute.FromEnum(typeof(ScheduleMode), ScheduleMode.Always)) == 0)
			{
				return Schedule.Always;
			}
			if (handleCustomNames && Schedule.customSchedules.ContainsKey(s))
			{
				return Schedule.customSchedules[s];
			}
			string[] array = s.Split(new char[]
			{
				','
			});
			if (array.Length == 1)
			{
				return Schedule.CreateCustomSchedule(null, new ScheduleInterval[]
				{
					ScheduleInterval.Parse(s)
				});
			}
			ScheduleInterval[] array2 = new ScheduleInterval[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = ScheduleInterval.Parse(array[i].Trim());
			}
			return new Schedule(array2, normalize);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002AC9E File Offset: 0x00028E9E
		public static Schedule Parse(string s)
		{
			return Schedule.Parse(s, true, true);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002ACA8 File Offset: 0x00028EA8
		public bool Contains(ScheduleInterval item)
		{
			foreach (ScheduleInterval scheduleInterval in this.intervals)
			{
				if (scheduleInterval.Contains(item.StartTime) && (scheduleInterval.Contains(item.EndTime) || scheduleInterval.EndTime == item.EndTime))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002AD14 File Offset: 0x00028F14
		public bool Contains(DateTime dateTime)
		{
			foreach (ScheduleInterval scheduleInterval in this.intervals)
			{
				if (scheduleInterval.Contains(dateTime))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002AD54 File Offset: 0x00028F54
		public static bool Equals(Schedule firstSchedule, Schedule secondSchedule)
		{
			if (firstSchedule == null)
			{
				return secondSchedule == null;
			}
			return firstSchedule.Equals(secondSchedule);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002AD68 File Offset: 0x00028F68
		public bool Equals(Schedule schedule)
		{
			if (schedule == null)
			{
				return false;
			}
			if (this.intervals.Length == schedule.intervals.Length)
			{
				IEnumerator<ScheduleInterval> enumerator = ((IList<ScheduleInterval>)this.intervals).GetEnumerator();
				IEnumerator<ScheduleInterval> enumerator2 = ((IList<ScheduleInterval>)schedule.intervals).GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator2.MoveNext();
					ScheduleInterval scheduleInterval = enumerator.Current;
					if (scheduleInterval.CompareTo(enumerator2.Current) != 0)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002ADD8 File Offset: 0x00028FD8
		public string ToString(bool handleCustomNames)
		{
			if (this.mode == ScheduleMode.Never)
			{
				return LocalizedDescriptionAttribute.FromEnum(typeof(ScheduleMode), ScheduleMode.Never);
			}
			if (this.mode == ScheduleMode.Always)
			{
				return LocalizedDescriptionAttribute.FromEnum(typeof(ScheduleMode), ScheduleMode.Always);
			}
			if (handleCustomNames && this.scheduleName != null)
			{
				return this.scheduleName;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ScheduleInterval scheduleInterval in this.intervals)
			{
				stringBuilder.Append(scheduleInterval.ToString());
				stringBuilder.Append(", ");
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002AE9B File Offset: 0x0002909B
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002AEA4 File Offset: 0x000290A4
		public byte[] ToByteArray()
		{
			return ScheduleInterval.GetWeekBitmapFromIntervals(this.intervals);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002AEB4 File Offset: 0x000290B4
		public static Schedule FromByteArray(byte[] bitmaps)
		{
			ScheduleInterval[] intervalsFromWeekBitmap = ScheduleInterval.GetIntervalsFromWeekBitmap(bitmaps);
			ScheduleMode scheduleMode = Schedule.CalculateMode(intervalsFromWeekBitmap);
			if (scheduleMode == ScheduleMode.Never)
			{
				return Schedule.Never;
			}
			if (scheduleMode == ScheduleMode.Always)
			{
				return Schedule.Always;
			}
			return new Schedule(intervalsFromWeekBitmap, scheduleMode);
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0002AEE9 File Offset: 0x000290E9
		public int Count
		{
			get
			{
				return this.intervals.Length;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002AEF3 File Offset: 0x000290F3
		void ICollection.CopyTo(Array a, int index)
		{
			this.intervals.CopyTo(a, index);
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0002AF02 File Offset: 0x00029102
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.intervals.IsSynchronized;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002AF0F File Offset: 0x0002910F
		object ICollection.SyncRoot
		{
			get
			{
				return this.intervals.SyncRoot;
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002AF1C File Offset: 0x0002911C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.intervals.GetEnumerator();
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002AF29 File Offset: 0x00029129
		IEnumerator<object> IEnumerable<object>.GetEnumerator()
		{
			return (IEnumerator<object>)this.intervals.GetEnumerator();
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0002AF3C File Offset: 0x0002913C
		public void Add(object obj)
		{
			if (this.exclusiveInput == null)
			{
				throw new NotSupportedException("Add");
			}
			if (this.exclusiveInput != string.Empty)
			{
				throw new ArgumentException(DataStrings.ErrorInputOfScheduleMustExclusive(this.exclusiveInput));
			}
			ScheduleInterval scheduleInterval;
			if (!(obj is ScheduleInterval))
			{
				if (obj is string)
				{
					if (this.changed)
					{
						scheduleInterval = ScheduleInterval.Parse((string)obj);
						goto IL_DC;
					}
					try
					{
						scheduleInterval = ScheduleInterval.Parse((string)obj);
						goto IL_DC;
					}
					catch (FormatException)
					{
						Schedule schedule = Schedule.Parse((string)obj);
						this.exclusiveInput = (string)obj;
						this.intervals = schedule.intervals;
						this.mode = schedule.mode;
						this.scheduleName = schedule.scheduleName;
						this.changed = true;
						return;
					}
				}
				throw new ArgumentException(DataStrings.ErrorInvalidScheduleType(obj.GetType().ToString()));
			}
			scheduleInterval = (ScheduleInterval)obj;
			IL_DC:
			this.changed = true;
			if (!(scheduleInterval.Length == TimeSpan.Zero))
			{
				if (this.intervals.Length == 0)
				{
					this.intervals = new ScheduleInterval[]
					{
						scheduleInterval
					};
					this.mode = ScheduleMode.ScheduledTimes;
					return;
				}
				List<ScheduleInterval> list = new List<ScheduleInterval>(this.intervals.Length + 1);
				int num = -1;
				for (int i = 0; i < this.intervals.Length; i++)
				{
					bool flag = this.intervals[i].Contains(scheduleInterval.EndTime);
					bool flag2 = scheduleInterval.Contains(this.intervals[i].EndTime);
					if (flag && flag2)
					{
						this.intervals = Schedule.Always.intervals;
						this.mode = Schedule.Always.mode;
						return;
					}
					if (flag || scheduleInterval.EndTime == this.intervals[i].EndTime)
					{
						scheduleInterval = new ScheduleInterval((this.intervals[i].Length > this.intervals[i].EndTime - scheduleInterval.StartTime) ? this.intervals[i].StartTime : scheduleInterval.StartTime, this.intervals[i].EndTime);
					}
					else if (flag2)
					{
						scheduleInterval = new ScheduleInterval((scheduleInterval.Length > scheduleInterval.EndTime - this.intervals[i].StartTime) ? scheduleInterval.StartTime : this.intervals[i].StartTime, scheduleInterval.EndTime);
					}
					else if (scheduleInterval.StartTime < this.intervals[i].StartTime && num == -1)
					{
						num = i;
						list.Add(scheduleInterval);
						list.Add(this.intervals[i]);
					}
					else
					{
						list.Add(this.intervals[i]);
					}
				}
				if (num == -1)
				{
					list.Add(scheduleInterval);
				}
				else if (scheduleInterval.StartTime > scheduleInterval.EndTime)
				{
					list.RemoveAt(0);
					list.Add(scheduleInterval);
				}
				this.intervals = list.ToArray();
				this.mode = ScheduleMode.ScheduledTimes;
				return;
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002B2A0 File Offset: 0x000294A0
		public void Clear()
		{
			throw new NotSupportedException("Clear");
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002B2AC File Offset: 0x000294AC
		public bool Contains(object obj)
		{
			throw new NotSupportedException("Contains");
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002B2B8 File Offset: 0x000294B8
		public void CopyTo(object[] array, int arrayIndex)
		{
			throw new NotSupportedException("CopyTo");
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002B2C4 File Offset: 0x000294C4
		public bool Remove(object item)
		{
			throw new NotSupportedException("Remove");
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0002B2D0 File Offset: 0x000294D0
		bool ICollection<object>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002B2D3 File Offset: 0x000294D3
		public Schedule()
		{
			this.intervals = Schedule.Never.intervals;
			this.mode = Schedule.Never.mode;
			this.exclusiveInput = string.Empty;
		}

		// Token: 0x04000818 RID: 2072
		private ScheduleInterval[] intervals;

		// Token: 0x04000819 RID: 2073
		private ScheduleMode mode;

		// Token: 0x0400081A RID: 2074
		private string scheduleName;

		// Token: 0x0400081B RID: 2075
		private static Dictionary<string, Schedule> customSchedules = new Dictionary<string, Schedule>();

		// Token: 0x0400081C RID: 2076
		private static readonly ExDateTime MaximumValidDateTime = ExDateTime.MaxValue - TimeSpan.FromDays(1.0);

		// Token: 0x0400081D RID: 2077
		private static readonly ExDateTime MinimumValidDateTime = ExDateTime.MinValue + TimeSpan.FromDays(1.0);

		// Token: 0x0400081E RID: 2078
		private ReadOnlyCollection<ScheduleInterval> readOnlyIntervals;

		// Token: 0x0400081F RID: 2079
		public static readonly Schedule Always = new Schedule(new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 0, 0, DayOfWeek.Saturday, 23, 45),
			new ScheduleInterval(DayOfWeek.Saturday, 23, 45, DayOfWeek.Sunday, 0, 0)
		}, ScheduleMode.Always, true, LocalizedDescriptionAttribute.FromEnum(typeof(ScheduleMode), ScheduleMode.Always));

		// Token: 0x04000820 RID: 2080
		public static readonly Schedule Daily10PM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.Daily10PM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 22, 0, DayOfWeek.Sunday, 22, 15),
			new ScheduleInterval(DayOfWeek.Monday, 22, 0, DayOfWeek.Monday, 22, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 22, 0, DayOfWeek.Tuesday, 22, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 22, 0, DayOfWeek.Wednesday, 22, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 22, 0, DayOfWeek.Thursday, 22, 15),
			new ScheduleInterval(DayOfWeek.Friday, 22, 0, DayOfWeek.Friday, 22, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 22, 0, DayOfWeek.Saturday, 22, 15)
		});

		// Token: 0x04000821 RID: 2081
		public static readonly Schedule Daily11PM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.Daily11PM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 23, 0, DayOfWeek.Sunday, 23, 15),
			new ScheduleInterval(DayOfWeek.Monday, 23, 0, DayOfWeek.Monday, 23, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 23, 0, DayOfWeek.Tuesday, 23, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 23, 0, DayOfWeek.Wednesday, 23, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 23, 0, DayOfWeek.Thursday, 23, 15),
			new ScheduleInterval(DayOfWeek.Friday, 23, 0, DayOfWeek.Friday, 23, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 23, 0, DayOfWeek.Saturday, 23, 15)
		});

		// Token: 0x04000822 RID: 2082
		public static readonly Schedule Daily12PM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.Daily12PM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 12, 0, DayOfWeek.Sunday, 12, 15),
			new ScheduleInterval(DayOfWeek.Monday, 12, 0, DayOfWeek.Monday, 12, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 12, 0, DayOfWeek.Tuesday, 12, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 12, 0, DayOfWeek.Wednesday, 12, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 12, 0, DayOfWeek.Thursday, 12, 15),
			new ScheduleInterval(DayOfWeek.Friday, 12, 0, DayOfWeek.Friday, 12, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 12, 0, DayOfWeek.Saturday, 12, 15)
		});

		// Token: 0x04000823 RID: 2083
		public static readonly Schedule DailyAtMidnight = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyAtMidnight), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 0, 0, DayOfWeek.Sunday, 0, 15),
			new ScheduleInterval(DayOfWeek.Monday, 0, 0, DayOfWeek.Monday, 0, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 0, 0, DayOfWeek.Tuesday, 0, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 0, 0, DayOfWeek.Wednesday, 0, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 0, 0, DayOfWeek.Thursday, 0, 15),
			new ScheduleInterval(DayOfWeek.Friday, 0, 0, DayOfWeek.Friday, 0, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 0, 0, DayOfWeek.Saturday, 0, 15)
		});

		// Token: 0x04000824 RID: 2084
		public static readonly Schedule Daily1AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.Daily1AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 1, 0, DayOfWeek.Sunday, 1, 15),
			new ScheduleInterval(DayOfWeek.Monday, 1, 0, DayOfWeek.Monday, 1, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 1, 0, DayOfWeek.Tuesday, 1, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 1, 0, DayOfWeek.Wednesday, 1, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 1, 0, DayOfWeek.Thursday, 1, 15),
			new ScheduleInterval(DayOfWeek.Friday, 1, 0, DayOfWeek.Friday, 1, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 1, 0, DayOfWeek.Saturday, 1, 15)
		});

		// Token: 0x04000825 RID: 2085
		public static readonly Schedule Daily2AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.Daily2AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 2, 0, DayOfWeek.Sunday, 2, 15),
			new ScheduleInterval(DayOfWeek.Monday, 2, 0, DayOfWeek.Monday, 2, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 2, 0, DayOfWeek.Tuesday, 2, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 2, 0, DayOfWeek.Wednesday, 2, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 2, 0, DayOfWeek.Thursday, 2, 15),
			new ScheduleInterval(DayOfWeek.Friday, 2, 0, DayOfWeek.Friday, 2, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 2, 0, DayOfWeek.Saturday, 2, 15)
		});

		// Token: 0x04000826 RID: 2086
		public static readonly Schedule Daily3AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.Daily3AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 3, 0, DayOfWeek.Sunday, 3, 15),
			new ScheduleInterval(DayOfWeek.Monday, 3, 0, DayOfWeek.Monday, 3, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 3, 0, DayOfWeek.Tuesday, 3, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 3, 0, DayOfWeek.Wednesday, 3, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 3, 0, DayOfWeek.Thursday, 3, 15),
			new ScheduleInterval(DayOfWeek.Friday, 3, 0, DayOfWeek.Friday, 3, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 3, 0, DayOfWeek.Saturday, 3, 15)
		});

		// Token: 0x04000827 RID: 2087
		public static readonly Schedule Daily4AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.Daily4AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 4, 0, DayOfWeek.Sunday, 4, 15),
			new ScheduleInterval(DayOfWeek.Monday, 4, 0, DayOfWeek.Monday, 4, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 4, 0, DayOfWeek.Tuesday, 4, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 4, 0, DayOfWeek.Wednesday, 4, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 4, 0, DayOfWeek.Thursday, 4, 15),
			new ScheduleInterval(DayOfWeek.Friday, 4, 0, DayOfWeek.Friday, 4, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 4, 0, DayOfWeek.Saturday, 4, 15)
		});

		// Token: 0x04000828 RID: 2088
		public static readonly Schedule Daily5AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.Daily5AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 5, 0, DayOfWeek.Sunday, 5, 15),
			new ScheduleInterval(DayOfWeek.Monday, 5, 0, DayOfWeek.Monday, 5, 15),
			new ScheduleInterval(DayOfWeek.Tuesday, 5, 0, DayOfWeek.Tuesday, 5, 15),
			new ScheduleInterval(DayOfWeek.Wednesday, 5, 0, DayOfWeek.Wednesday, 5, 15),
			new ScheduleInterval(DayOfWeek.Thursday, 5, 0, DayOfWeek.Thursday, 5, 15),
			new ScheduleInterval(DayOfWeek.Friday, 5, 0, DayOfWeek.Friday, 5, 15),
			new ScheduleInterval(DayOfWeek.Saturday, 5, 0, DayOfWeek.Saturday, 5, 15)
		});

		// Token: 0x04000829 RID: 2089
		public static readonly Schedule DailyFrom11PMTo3AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom11PMTo3AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 23, 0, DayOfWeek.Monday, 3, 0),
			new ScheduleInterval(DayOfWeek.Monday, 23, 0, DayOfWeek.Tuesday, 3, 0),
			new ScheduleInterval(DayOfWeek.Tuesday, 23, 0, DayOfWeek.Wednesday, 3, 0),
			new ScheduleInterval(DayOfWeek.Wednesday, 23, 0, DayOfWeek.Thursday, 3, 0),
			new ScheduleInterval(DayOfWeek.Thursday, 23, 0, DayOfWeek.Friday, 3, 0),
			new ScheduleInterval(DayOfWeek.Friday, 23, 0, DayOfWeek.Saturday, 3, 0),
			new ScheduleInterval(DayOfWeek.Saturday, 23, 0, DayOfWeek.Sunday, 3, 0)
		});

		// Token: 0x0400082A RID: 2090
		public static readonly Schedule DailyFrom11PMTo6AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom11PMTo6AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 23, 0, DayOfWeek.Monday, 6, 0),
			new ScheduleInterval(DayOfWeek.Monday, 23, 0, DayOfWeek.Tuesday, 6, 0),
			new ScheduleInterval(DayOfWeek.Tuesday, 23, 0, DayOfWeek.Wednesday, 6, 0),
			new ScheduleInterval(DayOfWeek.Wednesday, 23, 0, DayOfWeek.Thursday, 6, 0),
			new ScheduleInterval(DayOfWeek.Thursday, 23, 0, DayOfWeek.Friday, 6, 0),
			new ScheduleInterval(DayOfWeek.Friday, 23, 0, DayOfWeek.Saturday, 6, 0),
			new ScheduleInterval(DayOfWeek.Saturday, 23, 0, DayOfWeek.Sunday, 6, 0)
		});

		// Token: 0x0400082B RID: 2091
		public static readonly Schedule DailyFrom1AMTo5AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom1AMTo5AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 1, 0, DayOfWeek.Sunday, 5, 0),
			new ScheduleInterval(DayOfWeek.Monday, 1, 0, DayOfWeek.Monday, 5, 0),
			new ScheduleInterval(DayOfWeek.Tuesday, 1, 0, DayOfWeek.Tuesday, 5, 0),
			new ScheduleInterval(DayOfWeek.Wednesday, 1, 0, DayOfWeek.Wednesday, 5, 0),
			new ScheduleInterval(DayOfWeek.Thursday, 1, 0, DayOfWeek.Thursday, 5, 0),
			new ScheduleInterval(DayOfWeek.Friday, 1, 0, DayOfWeek.Friday, 5, 0),
			new ScheduleInterval(DayOfWeek.Saturday, 1, 0, DayOfWeek.Saturday, 5, 0)
		});

		// Token: 0x0400082C RID: 2092
		public static readonly Schedule DailyFrom2AMTo6AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFrom2AMTo6AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 2, 0, DayOfWeek.Sunday, 6, 0),
			new ScheduleInterval(DayOfWeek.Monday, 2, 0, DayOfWeek.Monday, 6, 0),
			new ScheduleInterval(DayOfWeek.Tuesday, 2, 0, DayOfWeek.Tuesday, 6, 0),
			new ScheduleInterval(DayOfWeek.Wednesday, 2, 0, DayOfWeek.Wednesday, 6, 0),
			new ScheduleInterval(DayOfWeek.Thursday, 2, 0, DayOfWeek.Thursday, 6, 0),
			new ScheduleInterval(DayOfWeek.Friday, 2, 0, DayOfWeek.Friday, 6, 0),
			new ScheduleInterval(DayOfWeek.Saturday, 2, 0, DayOfWeek.Saturday, 6, 0)
		});

		// Token: 0x0400082D RID: 2093
		public static readonly Schedule DailyFromMidnightTo4AM = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.DailyFromMidnightTo4AM), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 0, 0, DayOfWeek.Sunday, 4, 0),
			new ScheduleInterval(DayOfWeek.Monday, 0, 0, DayOfWeek.Monday, 4, 0),
			new ScheduleInterval(DayOfWeek.Tuesday, 0, 0, DayOfWeek.Tuesday, 4, 0),
			new ScheduleInterval(DayOfWeek.Wednesday, 0, 0, DayOfWeek.Wednesday, 4, 0),
			new ScheduleInterval(DayOfWeek.Thursday, 0, 0, DayOfWeek.Thursday, 4, 0),
			new ScheduleInterval(DayOfWeek.Friday, 0, 0, DayOfWeek.Friday, 4, 0),
			new ScheduleInterval(DayOfWeek.Saturday, 0, 0, DayOfWeek.Saturday, 4, 0)
		});

		// Token: 0x0400082E RID: 2094
		public static readonly Schedule EveryFourHours;

		// Token: 0x0400082F RID: 2095
		public static Schedule EveryHalfHour;

		// Token: 0x04000830 RID: 2096
		public static readonly Schedule EveryHour;

		// Token: 0x04000831 RID: 2097
		public static readonly Schedule EveryTwoHours;

		// Token: 0x04000832 RID: 2098
		public static readonly Schedule FridayAtMidnight = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.FridayAtMidnight), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Saturday, 0, 0, DayOfWeek.Saturday, 0, 15)
		});

		// Token: 0x04000833 RID: 2099
		public static readonly Schedule Never = new Schedule(new ScheduleInterval[0], ScheduleMode.Never);

		// Token: 0x04000834 RID: 2100
		public static readonly Schedule SaturdayAtMidnight = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.SaturdayAtMidnight), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 0, 0, DayOfWeek.Sunday, 0, 15)
		});

		// Token: 0x04000835 RID: 2101
		public static readonly Schedule SundayAtMidnight = Schedule.CreateCustomSchedule(LocalizedDescriptionAttribute.FromEnum(typeof(CustomScheduleName), CustomScheduleName.SundayAtMidnight), new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Monday, 0, 0, DayOfWeek.Monday, 0, 15)
		});

		// Token: 0x04000836 RID: 2102
		public static readonly Schedule DailyFrom8AMTo5PMAtWeekDays;

		// Token: 0x04000837 RID: 2103
		public static readonly Schedule DailyFrom9AMTo5PMAtWeekDays;

		// Token: 0x04000838 RID: 2104
		public static readonly Schedule DailyFrom9AMTo6PMAtWeekDays;

		// Token: 0x04000839 RID: 2105
		public static readonly Schedule DailyFrom8AMTo12PMAnd1PMTo5PMAtWeekDays;

		// Token: 0x0400083A RID: 2106
		public static readonly Schedule DailyFrom9AMTo12PMAnd1PMTo6PMAtWeekDays;

		// Token: 0x0400083B RID: 2107
		private string exclusiveInput;

		// Token: 0x0400083C RID: 2108
		private bool changed;
	}
}
