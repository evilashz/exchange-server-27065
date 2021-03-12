using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000144 RID: 324
	[ComVisible(true)]
	[Serializable]
	public abstract class TimeZone
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00038E90 File Offset: 0x00037090
		private static object InternalSyncObject
		{
			get
			{
				if (TimeZone.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref TimeZone.s_InternalSyncObject, value, null);
				}
				return TimeZone.s_InternalSyncObject;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00038EC4 File Offset: 0x000370C4
		public static TimeZone CurrentTimeZone
		{
			get
			{
				TimeZone timeZone = TimeZone.currentTimeZone;
				if (timeZone == null)
				{
					object internalSyncObject = TimeZone.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (TimeZone.currentTimeZone == null)
						{
							TimeZone.currentTimeZone = new CurrentSystemTimeZone();
						}
						timeZone = TimeZone.currentTimeZone;
					}
				}
				return timeZone;
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00038F28 File Offset: 0x00037128
		internal static void ResetTimeZone()
		{
			if (TimeZone.currentTimeZone != null)
			{
				object internalSyncObject = TimeZone.InternalSyncObject;
				lock (internalSyncObject)
				{
					TimeZone.currentTimeZone = null;
				}
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001388 RID: 5000
		public abstract string StandardName { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001389 RID: 5001
		public abstract string DaylightName { get; }

		// Token: 0x0600138A RID: 5002
		public abstract TimeSpan GetUtcOffset(DateTime time);

		// Token: 0x0600138B RID: 5003 RVA: 0x00038F74 File Offset: 0x00037174
		public virtual DateTime ToUniversalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return time;
			}
			long num = time.Ticks - this.GetUtcOffset(time).Ticks;
			if (num > 3155378975999999999L)
			{
				return new DateTime(3155378975999999999L, DateTimeKind.Utc);
			}
			if (num < 0L)
			{
				return new DateTime(0L, DateTimeKind.Utc);
			}
			return new DateTime(num, DateTimeKind.Utc);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00038FD8 File Offset: 0x000371D8
		public virtual DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool isAmbiguousDst = false;
			long utcOffsetFromUniversalTime = ((CurrentSystemTimeZone)TimeZone.CurrentTimeZone).GetUtcOffsetFromUniversalTime(time, ref isAmbiguousDst);
			return new DateTime(time.Ticks + utcOffsetFromUniversalTime, DateTimeKind.Local, isAmbiguousDst);
		}

		// Token: 0x0600138D RID: 5005
		public abstract DaylightTime GetDaylightChanges(int year);

		// Token: 0x0600138E RID: 5006 RVA: 0x00039016 File Offset: 0x00037216
		public virtual bool IsDaylightSavingTime(DateTime time)
		{
			return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0003902B File Offset: 0x0003722B
		public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
		{
			return TimeZone.CalculateUtcOffset(time, daylightTimes) != TimeSpan.Zero;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00039040 File Offset: 0x00037240
		internal static TimeSpan CalculateUtcOffset(DateTime time, DaylightTime daylightTimes)
		{
			if (daylightTimes == null)
			{
				return TimeSpan.Zero;
			}
			DateTimeKind kind = time.Kind;
			if (kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			DateTime dateTime = daylightTimes.Start + daylightTimes.Delta;
			DateTime end = daylightTimes.End;
			DateTime t;
			DateTime t2;
			if (daylightTimes.Delta.Ticks > 0L)
			{
				t = end - daylightTimes.Delta;
				t2 = end;
			}
			else
			{
				t = dateTime;
				t2 = dateTime - daylightTimes.Delta;
			}
			bool flag = false;
			if (dateTime > end)
			{
				if (time >= dateTime || time < end)
				{
					flag = true;
				}
			}
			else if (time >= dateTime && time < end)
			{
				flag = true;
			}
			if (flag && time >= t && time < t2)
			{
				flag = time.IsAmbiguousDaylightSavingTime();
			}
			if (flag)
			{
				return daylightTimes.Delta;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x040006A8 RID: 1704
		private static volatile TimeZone currentTimeZone;

		// Token: 0x040006A9 RID: 1705
		private static object s_InternalSyncObject;
	}
}
