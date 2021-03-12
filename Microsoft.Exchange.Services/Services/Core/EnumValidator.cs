using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000A1 RID: 161
	internal class EnumValidator
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00012C85 File Offset: 0x00010E85
		[Conditional("DEBUG")]
		public static void AssertValid<ENUM_TYPE>(ENUM_TYPE valueToCheck)
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00012C87 File Offset: 0x00010E87
		public static bool IsValidEnum<ENUM_TYPE>(ENUM_TYPE valueToCheck)
		{
			return EnumValidator.IsValidEnumIntValue<ENUM_TYPE>(EnumValidator.ToUInt64(valueToCheck));
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00012C9C File Offset: 0x00010E9C
		public static bool IsValidEnumIntValue<ENUM_TYPE>(ulong valueToCheck)
		{
			EnumValidator.EnumStats enumStats = EnumValidator.GetEnumStats(typeof(ENUM_TYPE));
			if (enumStats.IsFlags)
			{
				return valueToCheck >= enumStats.LowVal && (valueToCheck & ~enumStats.AllFlags) == 0UL;
			}
			return valueToCheck >= enumStats.LowVal && valueToCheck <= enumStats.HighVal && enumStats.Values.ContainsKey(valueToCheck);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00012CFC File Offset: 0x00010EFC
		private static EnumValidator.EnumStats CreateEnumStats(Type enumType)
		{
			Array values = Enum.GetValues(enumType);
			ulong[] array = new ulong[values.Length];
			ulong num = ulong.MaxValue;
			ulong num2 = 0UL;
			Attribute customAttribute = enumType.GetTypeInfo().GetCustomAttribute(typeof(FlagsAttribute));
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = EnumValidator.ToUInt64(values.GetValue(i));
				num = Math.Min(num, array[i]);
				num2 = Math.Max(num2, array[i]);
			}
			return new EnumValidator.EnumStats(num2, num, customAttribute != null, array);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00012D88 File Offset: 0x00010F88
		private static EnumValidator.EnumStats GetEnumStats(Type enumType)
		{
			EnumValidator.EnumStats enumStats;
			if (!EnumValidator.enumStats.TryGetValue(enumType, out enumStats))
			{
				lock (EnumValidator.enumStatsLock)
				{
					if (!EnumValidator.enumStats.TryGetValue(enumType, out enumStats))
					{
						Dictionary<Type, EnumValidator.EnumStats> dictionary = new Dictionary<Type, EnumValidator.EnumStats>(EnumValidator.enumStats);
						enumStats = EnumValidator.CreateEnumStats(enumType);
						dictionary.Add(enumType, enumStats);
						EnumValidator.enumStats = dictionary;
					}
				}
			}
			return enumStats;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00012E00 File Offset: 0x00011000
		public static ulong ToUInt64(object value)
		{
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				return (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return Convert.ToUInt64(value, CultureInfo.InvariantCulture);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04000616 RID: 1558
		private static Dictionary<Type, EnumValidator.EnumStats> enumStats = new Dictionary<Type, EnumValidator.EnumStats>();

		// Token: 0x04000617 RID: 1559
		private static readonly object enumStatsLock = new object();

		// Token: 0x020000A2 RID: 162
		private class EnumStats
		{
			// Token: 0x060003C3 RID: 963 RVA: 0x00012E7C File Offset: 0x0001107C
			public EnumStats(ulong highVal, ulong lowVal, bool isFlags, params ulong[] values)
			{
				this.HighVal = highVal;
				this.LowVal = lowVal;
				this.IsFlags = isFlags;
				this.Values = new Dictionary<ulong, ulong>();
				foreach (ulong num in values)
				{
					this.AllFlags |= num;
					if (!this.Values.ContainsKey(num))
					{
						this.Values.Add(num, num);
					}
				}
			}

			// Token: 0x04000618 RID: 1560
			public readonly ulong HighVal;

			// Token: 0x04000619 RID: 1561
			public readonly ulong LowVal;

			// Token: 0x0400061A RID: 1562
			public readonly bool IsFlags;

			// Token: 0x0400061B RID: 1563
			public readonly ulong AllFlags;

			// Token: 0x0400061C RID: 1564
			public readonly Dictionary<ulong, ulong> Values;
		}
	}
}
