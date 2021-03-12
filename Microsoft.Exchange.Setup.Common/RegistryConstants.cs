using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000050 RID: 80
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RegistryConstants
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000C527 File Offset: 0x0000A727
		public static string SetupKey
		{
			get
			{
				return Path.Combine("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\", "Setup");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000C538 File Offset: 0x0000A738
		public static string SetupBackupKey
		{
			get
			{
				return Path.Combine("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\", "Setup-save");
			}
		}

		// Token: 0x040000F4 RID: 244
		public const string BaseExchangeKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040000F5 RID: 245
		public const string BaseInstallKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\";

		// Token: 0x040000F6 RID: 246
		public const string LanguageBundlePathKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\LanguageBundlePath";

		// Token: 0x040000F7 RID: 247
		public const string RegistryPathForLanguagePack = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language Packs\\";

		// Token: 0x040000F8 RID: 248
		public const string RegistryKeyForLanguagePack = "LanguagePackBundlePath";

		// Token: 0x040000F9 RID: 249
		public const string UmLanguagePackKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\LanguagePacks\\";

		// Token: 0x040000FA RID: 250
		public const string TeleLanguagePackKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TeleLanguagePacks\\";

		// Token: 0x040000FB RID: 251
		public const string TransLanguagePackKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TransLanguagePacks\\";

		// Token: 0x040000FC RID: 252
		public const string TtsLanguagePackKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\TtsLanguagePacks\\";

		// Token: 0x040000FD RID: 253
		public const string BaseMsiUninstallKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}";

		// Token: 0x040000FE RID: 254
		public const string ProfilesDirectoryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList";

		// Token: 0x040000FF RID: 255
		public const string ShellFoldersSubKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Folders";

		// Token: 0x04000100 RID: 256
		public const string MshRegistryRoot = "\\SOFTWARE\\Microsoft\\PowerShell";

		// Token: 0x04000101 RID: 257
		public const string MshRegistryBak = "\\SOFTWARE\\Microsoft\\MSH-save";

		// Token: 0x04000102 RID: 258
		public const string InstallSource = "InstallSource";

		// Token: 0x04000103 RID: 259
		public const string InstallPath = "MsiInstallPath";

		// Token: 0x04000104 RID: 260
		public const string VersionMajor = "MsiProductMajor";

		// Token: 0x04000105 RID: 261
		public const string VersionMinor = "MsiProductMinor";

		// Token: 0x04000106 RID: 262
		public const string VersionBuild = "MsiBuildMajor";

		// Token: 0x04000107 RID: 263
		public const string VersionRevision = "MsiBuildMinor";

		// Token: 0x04000108 RID: 264
		public const string LanguageBundlePath = "LanguageBundlePath";

		// Token: 0x04000109 RID: 265
		public const string ProfilesDirectory = "ProfilesDirectory";
	}
}
