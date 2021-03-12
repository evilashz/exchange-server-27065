using System;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x0200000C RID: 12
	[Flags]
	public enum SetupOperations
	{
		// Token: 0x04000079 RID: 121
		None = 0,
		// Token: 0x0400007A RID: 122
		Install = 1,
		// Token: 0x0400007B RID: 123
		Uninstall = 2,
		// Token: 0x0400007C RID: 124
		AllUIInstallations = 11,
		// Token: 0x0400007D RID: 125
		RecoverServer = 4,
		// Token: 0x0400007E RID: 126
		Upgrade = 8,
		// Token: 0x0400007F RID: 127
		AllModeOperations = 15,
		// Token: 0x04000080 RID: 128
		PrepareAD = 256,
		// Token: 0x04000081 RID: 129
		PrepareSchema = 512,
		// Token: 0x04000082 RID: 130
		PrepareDomain = 2048,
		// Token: 0x04000083 RID: 131
		LanguagePack = 262144,
		// Token: 0x04000084 RID: 132
		AllClientAndServerLanguagePackOperations = 262144,
		// Token: 0x04000085 RID: 133
		AddUmLanguagePack = 16384,
		// Token: 0x04000086 RID: 134
		RemoveUmLanguagePack = 32768,
		// Token: 0x04000087 RID: 135
		AllUmLanguagePackOperations = 49152,
		// Token: 0x04000088 RID: 136
		AllPrepareOperations = 92928,
		// Token: 0x04000089 RID: 137
		AllSetupOperations = 387855,
		// Token: 0x0400008A RID: 138
		AllMSIInstallOperations = 13,
		// Token: 0x0400008B RID: 139
		NewProvisionedServer = 8192,
		// Token: 0x0400008C RID: 140
		RemoveProvisionedServer = 16384,
		// Token: 0x0400008D RID: 141
		PrepareSCT = 65536
	}
}
