using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.Common.ExtensionMethods
{
	// Token: 0x0200003A RID: 58
	public static class ExtensionMethods
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0000C74C File Offset: 0x0000A94C
		public static void AppendAsString(this StringBuilder sb, byte[] bytes, int offset, int length)
		{
			ToStringHelper.AppendAsString(bytes, offset, length, sb);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000C757 File Offset: 0x0000A957
		public static void AppendAsString<T>(this StringBuilder sb, T value)
		{
			ToStringHelper.AppendAsString<T>(value, sb);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000C760 File Offset: 0x0000A960
		public static string GetAsString<T>(this T value)
		{
			return ToStringHelper.GetAsString<T>(value);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C768 File Offset: 0x0000A968
		public static void SortAndRemoveDuplicates(this List<string> list, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			ValueHelper.SortAndRemoveDuplicates(list, compareInfo, compareOptions);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000C772 File Offset: 0x0000A972
		public static void SortAndRemoveDuplicates<T>(this List<T> list) where T : IComparable<T>, IEquatable<T>
		{
			ValueHelper.SortAndRemoveDuplicates<T>(list);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C77C File Offset: 0x0000A97C
		public static bool Any(this BitArray source, bool predicate)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == predicate)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C7A7 File Offset: 0x0000A9A7
		public static bool All(this BitArray source, bool predicate)
		{
			return !source.Any(!predicate);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000C7B6 File Offset: 0x0000A9B6
		public static string ValueOrEmpty(this string value)
		{
			return value ?? string.Empty;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		public static object GetBoxed(this bool value)
		{
			if (!value)
			{
				return SerializedValue.BoxedFalse;
			}
			return SerializedValue.BoxedTrue;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		public static double TotalMicroseconds(this TimeSpan value)
		{
			return (double)value.Ticks / 10.0;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000C7E6 File Offset: 0x0000A9E6
		public static TimeSpan ToTimeSpan(this Stopwatch sw)
		{
			return TimeSpan.FromTicks(StopwatchStamp.ToTimeSpanTicks(sw.ElapsedTicks));
		}

		// Token: 0x040004D1 RID: 1233
		private const long MicrosecondsPerMillisecond = 1000L;

		// Token: 0x040004D2 RID: 1234
		private const long TicksPerMicrosecond = 10L;
	}
}
