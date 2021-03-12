using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000154 RID: 340
	internal class SafeConvert
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x000292CC File Offset: 0x000274CC
		internal static int ToInt32(string value, int min, int max, int defaultValue)
		{
			if (max < min)
			{
				throw new ArgumentException("Min is less than Max");
			}
			int num = defaultValue;
			if (!string.IsNullOrEmpty(value) && (!int.TryParse(value, out num) || num < min || num > max))
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00029308 File Offset: 0x00027508
		internal static TimeSpan ToTimeSpan(string value, TimeSpan min, TimeSpan max, TimeSpan defaultValue)
		{
			if (max < min)
			{
				throw new ArgumentException("Min is less than Max");
			}
			TimeSpan timeSpan = defaultValue;
			if (!string.IsNullOrEmpty(value) && (!TimeSpan.TryParse(value, out timeSpan) || timeSpan < min || timeSpan > max))
			{
				timeSpan = defaultValue;
			}
			return timeSpan;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00029354 File Offset: 0x00027554
		internal static double ToDouble(string value, double min, double max, double defaultValue)
		{
			if (max < min)
			{
				throw new ArgumentException("Min is less than Max");
			}
			double num = defaultValue;
			if (!string.IsNullOrEmpty(value) && (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out num) || num < min || num > max))
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002939C File Offset: 0x0002759C
		internal static bool ToBoolean(string value, bool defaultValue)
		{
			bool result = defaultValue;
			if (!string.IsNullOrEmpty(value) && !bool.TryParse(value, out result))
			{
				int num = 0;
				if (!int.TryParse(value, out num) || num > 1 || num < 0)
				{
					result = defaultValue;
				}
				else
				{
					result = (1 == num);
				}
			}
			return result;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000293DC File Offset: 0x000275DC
		internal static string ToString(string value, string defaultValue)
		{
			string result = defaultValue;
			if (!string.IsNullOrEmpty(value))
			{
				result = value;
			}
			return result;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000293F8 File Offset: 0x000275F8
		internal static T ToEnum<T>(string value, T defaultValue) where T : struct
		{
			T result = defaultValue;
			if (!string.IsNullOrEmpty(value) && !EnumValidator<T>.TryParse(value, EnumParseOptions.IgnoreCase, out result))
			{
				result = defaultValue;
			}
			return result;
		}
	}
}
