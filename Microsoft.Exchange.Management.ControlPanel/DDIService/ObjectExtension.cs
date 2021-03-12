using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200012D RID: 301
	public static class ObjectExtension
	{
		// Token: 0x060020AB RID: 8363 RVA: 0x00062EBB File Offset: 0x000610BB
		public static bool IsNullValue(this object item)
		{
			return item == null || DBNull.Value.Equals(item);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00062ED0 File Offset: 0x000610D0
		public static bool IsTrue(this object item)
		{
			if (item.IsNullValue())
			{
				return false;
			}
			if (item is bool)
			{
				return (bool)item;
			}
			throw new ArgumentException("item is not bool or Nullable<bool>");
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00062EF5 File Offset: 0x000610F5
		public static bool IsFalse(this object item)
		{
			if (item.IsNullValue())
			{
				return false;
			}
			if (item is bool)
			{
				return !(bool)item;
			}
			throw new ArgumentException("value is not bool or Nullable<bool>");
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00062F20 File Offset: 0x00061120
		public static void Perform<T>(this IEnumerable<T> sequence, Action<T> action)
		{
			foreach (T obj in sequence)
			{
				action(obj);
			}
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00062F68 File Offset: 0x00061168
		public static string FromMB(this string size)
		{
			if (string.IsNullOrWhiteSpace(size))
			{
				throw new ArgumentNullException("size");
			}
			string result;
			try
			{
				result = ByteQuantifiedSize.FromBytes(checked((ulong)Math.Round(unchecked(Convert.ToDouble(size) * 1048576.0), 0))).ToString();
			}
			catch (FormatException)
			{
				throw new ArgumentException("String '" + size + "' is not of the expected number format.");
			}
			catch (OverflowException)
			{
				throw new ArgumentException("String '" + size + "' is outside the allowable numeric range.");
			}
			return result;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00063000 File Offset: 0x00061200
		public static string ToMB(this ByteQuantifiedSize size, int precision)
		{
			return Math.Round(size.ToBytes() / 1048576.0, precision).ToString();
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00063030 File Offset: 0x00061230
		public static string FromTimeSpan(this string span, TimeUnit factor)
		{
			if (string.IsNullOrWhiteSpace(span))
			{
				throw new ArgumentNullException("span");
			}
			if (factor < TimeUnit.Second || factor > TimeUnit.Day)
			{
				throw new ArgumentOutOfRangeException("factor");
			}
			string result;
			try
			{
				switch (factor)
				{
				case TimeUnit.Second:
					result = EnhancedTimeSpan.FromSeconds(Math.Round(Convert.ToDouble(span), 0)).ToString();
					break;
				case TimeUnit.Minute:
					result = EnhancedTimeSpan.FromSeconds(Math.Round(Convert.ToDouble(span) * 60.0, 0)).ToString();
					break;
				case TimeUnit.Hour:
					result = EnhancedTimeSpan.FromSeconds(Math.Round(Convert.ToDouble(span) * 3600.0, 0)).ToString();
					break;
				default:
					result = EnhancedTimeSpan.FromSeconds(Math.Round(Convert.ToDouble(span) * 86400.0, 0)).ToString();
					break;
				}
			}
			catch (FormatException)
			{
				throw new ArgumentException("String '" + span + "' is not of the expected number format.");
			}
			catch (OverflowException)
			{
				throw new ArgumentException("String '" + span + "' is outside the allowable numeric range.");
			}
			return result;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x00063178 File Offset: 0x00061378
		public static string ToString(this EnhancedTimeSpan span, TimeUnit factor, int precision)
		{
			if (factor < TimeUnit.Second || factor > TimeUnit.Day)
			{
				throw new ArgumentOutOfRangeException("factor");
			}
			switch (factor)
			{
			case TimeUnit.Second:
				return Math.Round(span.TotalSeconds, precision).ToString();
			case TimeUnit.Minute:
				return Math.Round(span.TotalMinutes, precision).ToString();
			case TimeUnit.Hour:
				return Math.Round(span.TotalHours, precision).ToString();
			default:
				return Math.Round(span.TotalDays, precision).ToString();
			}
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00063206 File Offset: 0x00061406
		public static bool? Or(this bool? oldVal, bool? newVal)
		{
			if (newVal != null)
			{
				if (oldVal != null)
				{
					oldVal = new bool?(oldVal.Value || newVal.Value);
				}
				else
				{
					oldVal = newVal;
				}
			}
			return oldVal;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x0006323A File Offset: 0x0006143A
		public static bool? And(this bool? oldVal, bool? newVal)
		{
			if (newVal != null)
			{
				if (oldVal != null)
				{
					oldVal = new bool?(oldVal.Value && newVal.Value);
				}
				else
				{
					oldVal = newVal;
				}
			}
			return oldVal;
		}

		// Token: 0x04001CF3 RID: 7411
		private const double DaysUnitFactor = 86400.0;

		// Token: 0x04001CF4 RID: 7412
		private const double HoursUnitFactor = 3600.0;

		// Token: 0x04001CF5 RID: 7413
		private const double MinutesUnitFactor = 60.0;

		// Token: 0x04001CF6 RID: 7414
		private const double MBUnitFactor = 1048576.0;
	}
}
