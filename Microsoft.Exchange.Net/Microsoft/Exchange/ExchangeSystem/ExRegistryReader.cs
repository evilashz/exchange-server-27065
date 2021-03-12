using System;
using System.Collections.Generic;
using Microsoft.Exchange.Win32;
using Microsoft.Win32;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000062 RID: 98
	internal sealed class ExRegistryReader
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		internal static IList<RegistryTimeZoneInformation> ReadTimeZones()
		{
			List<RegistryTimeZoneInformation> list = new List<RegistryTimeZoneInformation>(256);
			using (RegistryKey localMachine = Registry.LocalMachine)
			{
				using (RegistryKey registryKey = localMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"))
				{
					if (registryKey != null)
					{
						string[] subKeyNames = registryKey.GetSubKeyNames();
						foreach (string text in subKeyNames)
						{
							using (RegistryKey registryKey2 = registryKey.OpenSubKey(text))
							{
								try
								{
									RegistryTimeZoneInformation item = ExRegistryReader.ReadTimeZoneInfoFromRegistry(text, registryKey2);
									list.Add(item);
								}
								catch (InvalidTimeZoneException)
								{
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		public static RegistryTimeZoneInformation ReadTimeZoneInfoFromRegistry(string timeZoneKeyName, RegistryKey timeZoneKey)
		{
			RegistryTimeZoneInformation result;
			try
			{
				string displayName = timeZoneKey.GetValue("Display") as string;
				string standardName = timeZoneKey.GetValue("Std") as string;
				string daylightName = timeZoneKey.GetValue("Dlt") as string;
				string muiStandardName = timeZoneKey.GetValue("MUI_Std") as string;
				REG_TIMEZONE_INFO regTimeZoneRule = ExRegistryReader.GetRegTimeZoneRule(timeZoneKey, "TZI");
				RegistryTimeZoneInformation registryTimeZoneInformation = new RegistryTimeZoneInformation(timeZoneKeyName, displayName, standardName, daylightName, muiStandardName, regTimeZoneRule);
				using (RegistryKey registryKey = timeZoneKey.OpenSubKey("Dynamic DST"))
				{
					if (registryKey != null)
					{
						registryTimeZoneInformation.Rules = ExRegistryReader.LoadDynamicTimeZoneRules(registryKey);
					}
					else
					{
						registryTimeZoneInformation.Rules = new List<RegistryTimeZoneRule>(1);
						registryTimeZoneInformation.Rules.Add(new RegistryTimeZoneRule(1, registryTimeZoneInformation.RegInfo));
					}
				}
				result = registryTimeZoneInformation;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new InvalidTimeZoneException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public static string GetCurrentTimeZoneName()
		{
			return ExRegistryReader.GetCurrentTimeZoneInformation("TimeZoneKeyName");
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		public static string GetCurrentTimeZoneMuiStandardName()
		{
			return ExRegistryReader.GetCurrentTimeZoneInformation("StandardName");
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000E8DC File Offset: 0x0000CADC
		private static string GetCurrentTimeZoneInformation(string valueName)
		{
			string result = null;
			using (RegistryKey localMachine = Registry.LocalMachine)
			{
				using (RegistryKey registryKey = localMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation"))
				{
					if (registryKey != null)
					{
						result = (registryKey.GetValue(valueName) as string);
					}
				}
			}
			return result;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000E944 File Offset: 0x0000CB44
		private static IList<RegistryTimeZoneRule> LoadDynamicTimeZoneRules(RegistryKey dynamicRulesKey)
		{
			string[] valueNames = dynamicRulesKey.GetValueNames();
			List<int> list = new List<int>(valueNames.Length);
			for (int i = 0; i < valueNames.Length; i++)
			{
				int item;
				if (int.TryParse(valueNames[i], out item))
				{
					list.Add(item);
				}
			}
			list.Sort();
			List<RegistryTimeZoneRule> list2 = new List<RegistryTimeZoneRule>(list.Count);
			for (int j = 0; j < list.Count; j++)
			{
				REG_TIMEZONE_INFO regTimeZoneRule = ExRegistryReader.GetRegTimeZoneRule(dynamicRulesKey, list[j].ToString());
				list2.Add(new RegistryTimeZoneRule(list[j], regTimeZoneRule));
			}
			return list2;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		private unsafe static REG_TIMEZONE_INFO GetRegTimeZoneRule(RegistryKey timeZoneKey, string subKeyName)
		{
			byte[] array = timeZoneKey.GetValue(subKeyName, null) as byte[];
			if (array == null || array.Length != sizeof(REG_TIMEZONE_INFO))
			{
				throw new InvalidTimeZoneException("Invalid time zone");
			}
			REG_TIMEZONE_INFO result;
			fixed (IntPtr* ptr = array)
			{
				result = *(REG_TIMEZONE_INFO*)ptr;
			}
			ExRegistryReader.NormalizeTimeZoneInfo(ref result);
			return result;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		private static void NormalizeTimeZoneInfo(ref REG_TIMEZONE_INFO timeZoneInfo)
		{
			if (timeZoneInfo.StandardDate == timeZoneInfo.DaylightDate)
			{
				timeZoneInfo.StandardDate = default(NativeMethods.SystemTime);
				timeZoneInfo.DaylightDate = default(NativeMethods.SystemTime);
			}
			if (timeZoneInfo.StandardDate.Month == 0)
			{
				timeZoneInfo.DaylightBias = timeZoneInfo.StandardBias;
			}
		}

		// Token: 0x040001A2 RID: 418
		public const string RegistryTimeZoneRoot = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";

		// Token: 0x040001A3 RID: 419
		public const string IndexRegName = "Index";

		// Token: 0x040001A4 RID: 420
		public const string DltRegName = "Dlt";

		// Token: 0x040001A5 RID: 421
		public const string StdRegName = "Std";

		// Token: 0x040001A6 RID: 422
		public const string DisplayRegName = "Display";

		// Token: 0x040001A7 RID: 423
		public const string TziRegName = "TZI";

		// Token: 0x040001A8 RID: 424
		public const string MuiStdRegName = "MUI_Std";

		// Token: 0x040001A9 RID: 425
		public const string DynamicDstRegName = "Dynamic DST";

		// Token: 0x040001AA RID: 426
		public const string DaylightName = "Daylight";

		// Token: 0x040001AB RID: 427
		public const string StandardName = "Standard";

		// Token: 0x040001AC RID: 428
		public const string CurrentTimeZoneKey = "SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation";

		// Token: 0x040001AD RID: 429
		public const string CurrentTimeZoneName = "TimeZoneKeyName";

		// Token: 0x040001AE RID: 430
		public const string CurrentTimeZoneStandardName = "StandardName";
	}
}
