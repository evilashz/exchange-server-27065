using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C71 RID: 3185
	[Flags]
	internal enum ContextFlags
	{
		// Token: 0x04003B00 RID: 15104
		Zero = 0,
		// Token: 0x04003B01 RID: 15105
		Delegate = 1,
		// Token: 0x04003B02 RID: 15106
		MutualAuth = 2,
		// Token: 0x04003B03 RID: 15107
		ReplayDetect = 4,
		// Token: 0x04003B04 RID: 15108
		SequenceDetect = 8,
		// Token: 0x04003B05 RID: 15109
		Confidentiality = 16,
		// Token: 0x04003B06 RID: 15110
		UseSessionKey = 32,
		// Token: 0x04003B07 RID: 15111
		AllocateMemory = 256,
		// Token: 0x04003B08 RID: 15112
		Connection = 2048,
		// Token: 0x04003B09 RID: 15113
		InitExtendedError = 16384,
		// Token: 0x04003B0A RID: 15114
		AcceptExtendedError = 32768,
		// Token: 0x04003B0B RID: 15115
		InitStream = 32768,
		// Token: 0x04003B0C RID: 15116
		AcceptStream = 65536,
		// Token: 0x04003B0D RID: 15117
		InitIntegrity = 65536,
		// Token: 0x04003B0E RID: 15118
		AcceptIntegrity = 131072,
		// Token: 0x04003B0F RID: 15119
		InitNullSession = 262144,
		// Token: 0x04003B10 RID: 15120
		AcceptNullSession = 1048576,
		// Token: 0x04003B11 RID: 15121
		AcceptAllowNonUserLogons = 2097152,
		// Token: 0x04003B12 RID: 15122
		AcceptNoToken = 16777216,
		// Token: 0x04003B13 RID: 15123
		InitManualCredValidation = 524288,
		// Token: 0x04003B14 RID: 15124
		InitUseSuppliedCreds = 128,
		// Token: 0x04003B15 RID: 15125
		InitIdentify = 131072,
		// Token: 0x04003B16 RID: 15126
		AcceptIdentify = 524288,
		// Token: 0x04003B17 RID: 15127
		AllowMissingBindings = 268435456,
		// Token: 0x04003B18 RID: 15128
		ProxyBindings = 67108864
	}
}
