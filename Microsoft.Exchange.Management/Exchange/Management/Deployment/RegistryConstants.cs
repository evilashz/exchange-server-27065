using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000234 RID: 564
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RegistryConstants
	{
		// Token: 0x04000808 RID: 2056
		public const string BaseExchangeKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04000809 RID: 2057
		public const string LanguageKey = "Language";

		// Token: 0x0400080A RID: 2058
		public const string BaseInstallKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\";

		// Token: 0x0400080B RID: 2059
		public const string BaseInstallKeyWow6432Node = "SOFTWARE\\Wow6432Node\\Microsoft\\ExchangeServer\\v15\\";

		// Token: 0x0400080C RID: 2060
		public const string BaseMsiUninstallKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}";

		// Token: 0x0400080D RID: 2061
		public const string ConfiguredVersion = "ConfiguredVersion";

		// Token: 0x0400080E RID: 2062
		public const string UnpackedVersion = "UnpackedVersion";

		// Token: 0x0400080F RID: 2063
		public const string UnpackedDatacenterVersion = "UnpackedDatacenterVersion";

		// Token: 0x04000810 RID: 2064
		public const string Action = "Action";

		// Token: 0x04000811 RID: 2065
		public const string Watermark = "Watermark";

		// Token: 0x04000812 RID: 2066
		public const string PostSetupVersion = "PostSetupVersion";

		// Token: 0x04000813 RID: 2067
		public const string ServerLanguage = "Server Language";

		// Token: 0x04000814 RID: 2068
		public const string InstallSource = "InstallSource";

		// Token: 0x04000815 RID: 2069
		public const string TeleLanguagePackKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TeleLanguagePacks\\";

		// Token: 0x04000816 RID: 2070
		public const string TransLanguagePackKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TransLanguagePacks\\";

		// Token: 0x04000817 RID: 2071
		public const string TtsLanguagePackKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TtsLanguagePacks\\";
	}
}
