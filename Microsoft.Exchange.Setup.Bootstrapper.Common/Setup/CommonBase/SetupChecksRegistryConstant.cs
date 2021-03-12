using System;
using System.IO;

namespace Microsoft.Exchange.Setup.CommonBase
{
	// Token: 0x0200000A RID: 10
	public static class SetupChecksRegistryConstant
	{
		// Token: 0x04000059 RID: 89
		public const string RegistryExchangePathKey = "MsiInstallPath";

		// Token: 0x0400005A RID: 90
		public const string RegistryExchangeProductMajorVersionKey = "MsiProductMajor";

		// Token: 0x0400005B RID: 91
		public const string RegistryExchangeProductMinorVersionKey = "MsiProductMinor";

		// Token: 0x0400005C RID: 92
		public const string RegistryExchangeBuildMajorVersionKey = "MsiBuildMajor";

		// Token: 0x0400005D RID: 93
		public const string RegistryExchangeBuildMinorVersionKey = "MsiBuildMinor";

		// Token: 0x0400005E RID: 94
		public const string RegistryMicrosoft = "SOFTWARE\\Microsoft";

		// Token: 0x0400005F RID: 95
		public const string RegistryKeyForLanguagePack = "LanguagePackBundlePath";

		// Token: 0x04000060 RID: 96
		public const string RegistrySetupPath = "Setup";

		// Token: 0x04000061 RID: 97
		public const string RegistrySetupSavePath = "Setup-save";

		// Token: 0x04000062 RID: 98
		public const string HubTransportRoleName = "HubTransportRole";

		// Token: 0x04000063 RID: 99
		public const string ClientAccessRoleName = "ClientAccessRole";

		// Token: 0x04000064 RID: 100
		public const string EdgeRoleName = "Hygiene";

		// Token: 0x04000065 RID: 101
		public const string MailboxRoleName = "MailboxRole";

		// Token: 0x04000066 RID: 102
		public const string UnifiedMessagingRoleName = "UnifiedMessagingRole";

		// Token: 0x04000067 RID: 103
		public const string AdminToolsRoleName = "AdminToolsRole";

		// Token: 0x04000068 RID: 104
		public const string SetupAction = "Action";

		// Token: 0x04000069 RID: 105
		public const string UpgradeAction = "BuildToBuildUpgrade";

		// Token: 0x0400006A RID: 106
		public const string DotNetSetupRegistry = "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full";

		// Token: 0x0400006B RID: 107
		public const string DotNetVersionRegistryKey = "version";

		// Token: 0x0400006C RID: 108
		public const string PowerShellRegistry = "SOFTWARE\\Microsoft\\PowerShell\\3\\PowerShellEngine";

		// Token: 0x0400006D RID: 109
		public const string PowerShellVersionRegistryKey = "PowerShellVersion";

		// Token: 0x0400006E RID: 110
		public const string MicrosoftHostedKeyPath = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x0400006F RID: 111
		public const string MicrosoftHostedValueName = "DatacenterMode";

		// Token: 0x04000070 RID: 112
		public const string TreatPreReqErrorsAsWarningsKey = "TreatPreReqErrorsAsWarnings";

		// Token: 0x04000071 RID: 113
		public static readonly string RegistryExchangeServer = Path.Combine("SOFTWARE\\Microsoft", "ExchangeServer");

		// Token: 0x04000072 RID: 114
		public static readonly string RegistryExchangePathE12 = Path.Combine("SOFTWARE\\Microsoft", "Exchange\\v8.0");

		// Token: 0x04000073 RID: 115
		public static readonly string RegistryExchangePathE14 = Path.Combine(SetupChecksRegistryConstant.RegistryExchangeServer, "v14");

		// Token: 0x04000074 RID: 116
		public static readonly string RegistryExchangePath = Path.Combine(SetupChecksRegistryConstant.RegistryExchangeServer, "V15");

		// Token: 0x04000075 RID: 117
		public static readonly string RegistryPathForLanguagePack = Path.Combine(SetupChecksRegistryConstant.RegistryExchangeServer, "Language Packs");

		// Token: 0x04000076 RID: 118
		public static readonly Version DotNetVersion = new Version("4.5.50501");

		// Token: 0x04000077 RID: 119
		public static readonly Version PowershellVersion = new Version("3.0");
	}
}
