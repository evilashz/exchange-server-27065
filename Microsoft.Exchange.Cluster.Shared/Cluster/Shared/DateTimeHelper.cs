using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000068 RID: 104
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DateTimeHelper
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000D588 File Offset: 0x0000B788
		public static bool IsValidDateTime(DateTime dateTime)
		{
			return dateTime > DateTimeHelper.s_minDateTime && dateTime < DateTime.MaxValue;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		public static string ToPersistedString(DateTime dateTime)
		{
			return DateTimeHelper.ToPersistedString(dateTime);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public static string ToPersistedString(ExDateTime exDateTime)
		{
			return DateTimeHelper.ToPersistedString(exDateTime.UniversalTime);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D5BA File Offset: 0x0000B7BA
		public static DateTime ParseIntoDateTime(string dateTimeStr)
		{
			return DateTimeHelper.ParseIntoDateTime(dateTimeStr);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public static ExDateTime? ParseIntoNullableExDateTimeIfPossible(string dateTimeStr)
		{
			ExDateTime? result = null;
			DateTime dateTime;
			if (DateTimeHelper.TryParseIntoDateTime(dateTimeStr, out dateTime))
			{
				result = DateTimeHelper.ToNullableExDateTime(dateTime);
			}
			return result;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		public static DateTime? ParseIntoNullableDateTimeIfPossible(string dateTimeStr)
		{
			DateTime? result = null;
			DateTime dateTime;
			if (DateTimeHelper.TryParseIntoDateTime(dateTimeStr, out dateTime))
			{
				result = DateTimeHelper.ToNullableDateTime(dateTime);
			}
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000D614 File Offset: 0x0000B814
		public static DateTime? ParseIntoNullableLocalDateTimeIfPossible(string dateTimeStr)
		{
			DateTime? result = null;
			DateTime dateTime;
			if (DateTimeHelper.TryParseIntoDateTime(dateTimeStr, out dateTime))
			{
				result = DateTimeHelper.ToNullableLocalDateTime(dateTime);
			}
			return result;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D63C File Offset: 0x0000B83C
		public static ExDateTime? ToNullableExDateTime(DateTime dateTime)
		{
			if (DateTimeHelper.IsValidDateTime(dateTime))
			{
				return new ExDateTime?(new ExDateTime(ExTimeZone.CurrentTimeZone, dateTime));
			}
			return null;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D66C File Offset: 0x0000B86C
		public static DateTime? ToNullableDateTime(DateTime dateTime)
		{
			if (DateTimeHelper.IsValidDateTime(dateTime))
			{
				return new DateTime?(dateTime);
			}
			return null;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D694 File Offset: 0x0000B894
		public static DateTime? ToNullableLocalDateTime(DateTime dateTime)
		{
			if (DateTimeHelper.IsValidDateTime(dateTime))
			{
				return new DateTime?(dateTime.ToLocalTime());
			}
			return null;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public static DateTime? ToNullableLocalDateTime(DateTime? dateTime)
		{
			if (dateTime == null)
			{
				return null;
			}
			return DateTimeHelper.ToNullableLocalDateTime(dateTime.Value);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		public static ExDateTime ParseIntoExDateTime(string dateTimeStr)
		{
			return ExDateTime.Parse(ExTimeZone.CurrentTimeZone, dateTimeStr);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000D6F9 File Offset: 0x0000B8F9
		public static bool TryParseIntoDateTime(string dateTimeStr, out DateTime dateTimeValue)
		{
			return DateTimeHelper.TryParseIntoDateTime(dateTimeStr, ref dateTimeValue);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D702 File Offset: 0x0000B902
		public static bool TryParseIntoExDateTime(string dateTimeStr, out ExDateTime dateTimeValue)
		{
			return ExDateTime.TryParse(ExTimeZone.CurrentTimeZone, dateTimeStr, out dateTimeValue);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D710 File Offset: 0x0000B910
		public static ExDateTime ToLocalExDateTime(DateTime dateTime)
		{
			return new ExDateTime(ExTimeZone.CurrentTimeZone, dateTime);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D71D File Offset: 0x0000B91D
		public static string ConvertTimeSpanToShortString(TimeSpan timeSpan)
		{
			return DateTimeHelper.ConvertTimeSpanToShortString(timeSpan);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D725 File Offset: 0x0000B925
		public static string ConvertTimeSpanToShortString(EnhancedTimeSpan timeSpan)
		{
			return DateTimeHelper.ConvertTimeSpanToShortString(timeSpan);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D734 File Offset: 0x0000B934
		public static DateTime FromFileTimeUtc(NativeMethods.FILETIME ft)
		{
			long fileTime = (long)(((ulong)ft.DateTimeHigh << 32) + (ulong)ft.DateTimeLow);
			return DateTime.FromFileTimeUtc(fileTime);
		}

		// Token: 0x040001E0 RID: 480
		private static readonly DateTime s_minDateTime = DateTime.FromFileTimeUtc(0L);
	}
}
