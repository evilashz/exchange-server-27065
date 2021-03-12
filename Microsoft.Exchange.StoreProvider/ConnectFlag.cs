using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200000E RID: 14
	[Flags]
	internal enum ConnectFlag
	{
		// Token: 0x0400004B RID: 75
		None = 0,
		// Token: 0x0400004C RID: 76
		UseAdminPrivilege = 1,
		// Token: 0x0400004D RID: 77
		NoRpcEncryption = 2,
		// Token: 0x0400004E RID: 78
		UseSeparateConnection = 4,
		// Token: 0x0400004F RID: 79
		NoUnderCoverConnection = 8,
		// Token: 0x04000050 RID: 80
		AnonymousAccess = 16,
		// Token: 0x04000051 RID: 81
		NoNotifications = 32,
		// Token: 0x04000052 RID: 82
		NoTableNotifications = 32,
		// Token: 0x04000053 RID: 83
		NoAddressResolution = 64,
		// Token: 0x04000054 RID: 84
		RestoreDatabase = 128,
		// Token: 0x04000055 RID: 85
		UseDelegatedAuthPrivilege = 256,
		// Token: 0x04000056 RID: 86
		UseLegacyUdpNotifications = 512,
		// Token: 0x04000057 RID: 87
		UseTransportPrivilege = 1024,
		// Token: 0x04000058 RID: 88
		UseReadOnlyPrivilege = 2048,
		// Token: 0x04000059 RID: 89
		UseReadWritePrivilege = 4096,
		// Token: 0x0400005A RID: 90
		LocalRpcOnly = 8192,
		// Token: 0x0400005B RID: 91
		LowMemoryFootprint = 16384,
		// Token: 0x0400005C RID: 92
		UseHTTPS = 65536,
		// Token: 0x0400005D RID: 93
		UseNTLM = 131072,
		// Token: 0x0400005E RID: 94
		UseRpcUniqueBinding = 262144,
		// Token: 0x0400005F RID: 95
		ConnectToExchangeRpcServerOnly = 524288,
		// Token: 0x04000060 RID: 96
		UseRpcContextPool = 1048576,
		// Token: 0x04000061 RID: 97
		UseResiliency = 2097152,
		// Token: 0x04000062 RID: 98
		RemoteSystemService = 4194304,
		// Token: 0x04000063 RID: 99
		PublicFolderMigration = 8388608,
		// Token: 0x04000064 RID: 100
		IsPreExchange15 = 16777216,
		// Token: 0x04000065 RID: 101
		AllowLegacyStore = 33554432,
		// Token: 0x04000066 RID: 102
		MonitoringMailbox = 67108864,
		// Token: 0x04000067 RID: 103
		UseAnonymousInnerAuth = 134217728,
		// Token: 0x04000068 RID: 104
		IgnoreServerCertificate = 268435456
	}
}
