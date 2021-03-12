using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000081 RID: 129
	internal sealed class GlobalSettingsHandler : ExchangeDiagnosableWrapper<GlobalSettingsResult>
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00026427 File Offset: 0x00024627
		protected override string UsageText
		{
			get
			{
				return "The GlobalSettings handler is a diagnostics handler that returns information about the current values exposed through the GlobalSettings singleton. These values are not necessarily the same as the values in web.config as they can be replaced due to parse or range errors, missing values, etc...";
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0002642E File Offset: 0x0002462E
		protected override string UsageSample
		{
			get
			{
				return "Example 1: Return all values.\r\n   Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GlobalSettings\r\n\r\nExample 2: Return a specific appSetting.\r\n   Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GlobalSettings -Argument propertyName=<AppSettingName>\r\n\r\nExample 3: Return all properties matching a name pattern.\r\n   Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GlobalSettings -Argument propertyName=*PartialString*\r\n\r\nExample 4: Return all properties that are not set to default value.\r\n   Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GlobalSettings -Argument notDefault\r\n\r\nYou can also combine the two arguments together, separated by a comma such as \"propertyName=*Timeout*,notDefault";
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00026438 File Offset: 0x00024638
		public static GlobalSettingsHandler GetInstance()
		{
			if (GlobalSettingsHandler.instance == null)
			{
				lock (GlobalSettingsHandler.lockObject)
				{
					if (GlobalSettingsHandler.instance == null)
					{
						GlobalSettingsHandler.instance = new GlobalSettingsHandler();
					}
				}
			}
			return GlobalSettingsHandler.instance;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00026490 File Offset: 0x00024690
		private GlobalSettingsHandler()
		{
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00026498 File Offset: 0x00024698
		protected override string ComponentName
		{
			get
			{
				return "GlobalSettings";
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000264A0 File Offset: 0x000246A0
		internal override GlobalSettingsResult GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			string text = null;
			bool returnOnlySettingsThatAreNotDefault = false;
			if (!string.IsNullOrEmpty(argument.Argument))
			{
				string[] array = argument.Argument.Split(new char[]
				{
					','
				});
				foreach (string text2 in array)
				{
					if (text2.StartsWith("propertyname=", StringComparison.OrdinalIgnoreCase))
					{
						text = text2.Substring("propertyname=".Length);
						text = base.RemoveQuotes(text);
					}
					else if (text2.StartsWith("notdefault"))
					{
						returnOnlySettingsThatAreNotDefault = true;
					}
				}
			}
			return GlobalSettingsResult.Create(text, returnOnlySettingsThatAreNotDefault);
		}

		// Token: 0x040004C1 RID: 1217
		private const string propertyNamePrefix = "propertyname=";

		// Token: 0x040004C2 RID: 1218
		private const string notDefaultPrefix = "notdefault";

		// Token: 0x040004C3 RID: 1219
		private static GlobalSettingsHandler instance;

		// Token: 0x040004C4 RID: 1220
		private static object lockObject = new object();
	}
}
