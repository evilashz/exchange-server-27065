using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000140 RID: 320
	internal static class ApplicationServices
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0006E4CC File Offset: 0x0006C6CC
		public static IApplicationServices Provider
		{
			get
			{
				return ApplicationServices.provider;
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0006E4D4 File Offset: 0x0006C6D4
		public static CtsConfigurationSetting GetSimpleConfigurationSetting(string subSectionName, string settingName)
		{
			CtsConfigurationSetting ctsConfigurationSetting = null;
			IList<CtsConfigurationSetting> configuration = ApplicationServices.Provider.GetConfiguration(subSectionName);
			foreach (CtsConfigurationSetting ctsConfigurationSetting2 in configuration)
			{
				if (string.Equals(ctsConfigurationSetting2.Name, settingName, StringComparison.OrdinalIgnoreCase))
				{
					if (ctsConfigurationSetting != null)
					{
						ApplicationServices.Provider.LogConfigurationErrorEvent();
						break;
					}
					ctsConfigurationSetting = ctsConfigurationSetting2;
				}
			}
			return ctsConfigurationSetting;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0006E544 File Offset: 0x0006C744
		internal static int ParseIntegerSetting(CtsConfigurationSetting setting, int defaultValue, int min, bool kilobytes)
		{
			if (setting.Arguments.Count != 1 || !setting.Arguments[0].Name.Equals("Value", StringComparison.OrdinalIgnoreCase))
			{
				ApplicationServices.Provider.LogConfigurationErrorEvent();
				return defaultValue;
			}
			if (setting.Arguments[0].Value.Trim().Equals("unlimited", StringComparison.OrdinalIgnoreCase))
			{
				return int.MaxValue;
			}
			int num;
			if (!int.TryParse(setting.Arguments[0].Value.Trim(), out num))
			{
				ApplicationServices.Provider.LogConfigurationErrorEvent();
				return defaultValue;
			}
			if (num < min)
			{
				ApplicationServices.Provider.LogConfigurationErrorEvent();
				return defaultValue;
			}
			if (kilobytes)
			{
				if (num > 2097151)
				{
					num = int.MaxValue;
				}
				else
				{
					num *= 1024;
				}
			}
			return num;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0006E609 File Offset: 0x0006C809
		private static IApplicationServices LoadServices()
		{
			return new DefaultApplicationServices();
		}

		// Token: 0x04000F0E RID: 3854
		private static IApplicationServices provider = ApplicationServices.LoadServices();
	}
}
