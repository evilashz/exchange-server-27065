using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000220 RID: 544
	[Flags]
	internal enum OpenMailboxSessionFlags
	{
		// Token: 0x04000FE7 RID: 4071
		None = 0,
		// Token: 0x04000FE8 RID: 4072
		RequestAdminAccess = 1,
		// Token: 0x04000FE9 RID: 4073
		RequestCachedConnection = 2,
		// Token: 0x04000FEA RID: 4074
		RequestTransportAccess = 4,
		// Token: 0x04000FEB RID: 4075
		OpenForQuotaMessageDelivery = 8,
		// Token: 0x04000FEC RID: 4076
		OpenForNormalMessageDelivery = 16,
		// Token: 0x04000FED RID: 4077
		OpenForSpecialMessageDelivery = 32,
		// Token: 0x04000FEE RID: 4078
		RequestLocalRpcConnection = 64,
		// Token: 0x04000FEF RID: 4079
		RequestExchangeRpcServer = 128,
		// Token: 0x04000FF0 RID: 4080
		OverrideHomeMdb = 256,
		// Token: 0x04000FF1 RID: 4081
		InitDefaultFolders = 512,
		// Token: 0x04000FF2 RID: 4082
		InitUserConfigurationManager = 1024,
		// Token: 0x04000FF3 RID: 4083
		InitCopyOnWrite = 2048,
		// Token: 0x04000FF4 RID: 4084
		InitDeadSessionChecking = 4096,
		// Token: 0x04000FF5 RID: 4085
		InitCheckPrivateItemsAccess = 8192,
		// Token: 0x04000FF6 RID: 4086
		SuppressFolderIdPrefetch = 16384,
		// Token: 0x04000FF7 RID: 4087
		UseNamedProperties = 32768,
		// Token: 0x04000FF8 RID: 4088
		DeferDefaultFolderIdInitialization = 65536,
		// Token: 0x04000FF9 RID: 4089
		UseRecoveryDatabase = 131072,
		// Token: 0x04000FFA RID: 4090
		NonInteractiveSession = 262144,
		// Token: 0x04000FFB RID: 4091
		DisconnectedMailbox = 524288,
		// Token: 0x04000FFC RID: 4092
		XForestMove = 1048576,
		// Token: 0x04000FFD RID: 4093
		MoveUser = 2097152,
		// Token: 0x04000FFE RID: 4094
		IgnoreForcedFolderInit = 4194304,
		// Token: 0x04000FFF RID: 4095
		ContentIndexing = 8388608,
		// Token: 0x04001000 RID: 4096
		AllowAdminLocalization = 16777216,
		// Token: 0x04001001 RID: 4097
		ReadOnly = 33554432,
		// Token: 0x04001002 RID: 4098
		OlcSync = 67108864
	}
}
