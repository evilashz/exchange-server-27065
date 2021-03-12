using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Utility
{
	// Token: 0x0200001C RID: 28
	internal static class Helper
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00005638 File Offset: 0x00003838
		public static TimeSpan GetTimeSpanPercentage(TimeSpan span, double percentage)
		{
			double num = span.TotalMilliseconds * (percentage / 100.0);
			if (num < TimeSpan.MaxValue.TotalMilliseconds && num > 0.0)
			{
				return TimeSpan.FromMilliseconds(num);
			}
			return span;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000567D File Offset: 0x0000387D
		public static int GetWaitTime(TimeSpan span)
		{
			if (span.TotalMilliseconds > 2147483647.0)
			{
				return -1;
			}
			return (int)span.TotalMilliseconds;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000569B File Offset: 0x0000389B
		public static string FromBytes(byte[] bytes)
		{
			if (bytes != null && bytes.Length > 0)
			{
				return Encoding.UTF8.GetString(bytes);
			}
			return null;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000056B3 File Offset: 0x000038B3
		public static byte[] ToBytes(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return Encoding.UTF8.GetBytes(s);
			}
			return null;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000056CA File Offset: 0x000038CA
		public static string ToDefaultString(string input, string defaultValue = null)
		{
			if (string.IsNullOrEmpty(input))
			{
				return defaultValue;
			}
			return input;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000056D7 File Offset: 0x000038D7
		public static string DumpObject(object obj)
		{
			return string.Join(", ", Helper.DumpObject(obj, null, 0).ToArray<string>());
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000056F0 File Offset: 0x000038F0
		public static IEnumerable<string> DumpObject(object obj, string prefix, int maxLevel = 3)
		{
			List<string> list = new List<string>();
			if (obj != null)
			{
				if (obj is ICollection)
				{
					list.Add(string.Concat(new object[]
					{
						prefix,
						(!string.IsNullOrEmpty(prefix)) ? " = " : string.Empty,
						"Items: ",
						((ICollection)obj).Count
					}));
				}
				else if (obj is string || obj.GetType().IsValueType)
				{
					list.Add(prefix + ((!string.IsNullOrEmpty(prefix)) ? " = " : string.Empty) + obj.ToString());
				}
				else if (maxLevel >= 0)
				{
					foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
					{
						object obj2 = null;
						try
						{
							obj2 = propertyInfo.GetValue(obj);
						}
						catch (Exception ex)
						{
							obj2 = ex.Message;
						}
						string str = (prefix == null) ? string.Empty : ".";
						list.AddRange(Helper.DumpObject(obj2, prefix + str + propertyInfo.Name, maxLevel - 1));
					}
				}
			}
			return list;
		}
	}
}
