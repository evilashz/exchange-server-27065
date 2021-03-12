using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000014 RID: 20
	[Flags]
	public enum OpenStoreFlags : uint
	{
		// Token: 0x04000067 RID: 103
		None = 0U,
		// Token: 0x04000068 RID: 104
		UseAdminPrivilege = 1U,
		// Token: 0x04000069 RID: 105
		Public = 2U,
		// Token: 0x0400006A RID: 106
		HomeLogon = 4U,
		// Token: 0x0400006B RID: 107
		TakeOwnership = 8U,
		// Token: 0x0400006C RID: 108
		OverrideHomeMdb = 16U,
		// Token: 0x0400006D RID: 109
		Transport = 32U,
		// Token: 0x0400006E RID: 110
		RemoteTransport = 64U,
		// Token: 0x0400006F RID: 111
		InternetAnonymous = 128U,
		// Token: 0x04000070 RID: 112
		AlternateServer = 256U,
		// Token: 0x04000071 RID: 113
		IgnoreHomeMdb = 512U,
		// Token: 0x04000072 RID: 114
		NoMail = 1024U,
		// Token: 0x04000073 RID: 115
		OverrideLastModifier = 2048U,
		// Token: 0x04000074 RID: 116
		CallbackLogon = 4096U,
		// Token: 0x04000075 RID: 117
		Local = 8192U,
		// Token: 0x04000076 RID: 118
		FailIfNoMailbox = 16384U,
		// Token: 0x04000077 RID: 119
		CacheExchange = 32768U,
		// Token: 0x04000078 RID: 120
		CliWithNamedpropFix = 65536U,
		// Token: 0x04000079 RID: 121
		EnableLazyLogging = 131072U,
		// Token: 0x0400007A RID: 122
		CliWithReplidGuidMappingFix = 262144U,
		// Token: 0x0400007B RID: 123
		NoLocalization = 524288U,
		// Token: 0x0400007C RID: 124
		RestoreDatabase = 1048576U,
		// Token: 0x0400007D RID: 125
		XforestMove = 2097152U,
		// Token: 0x0400007E RID: 126
		MailboxGuid = 4194304U,
		// Token: 0x0400007F RID: 127
		ExchangeTransport = 8388608U,
		// Token: 0x04000080 RID: 128
		UsePerMdbReplidMapping = 16777216U,
		// Token: 0x04000081 RID: 129
		DeliverNormalMessage = 33554432U,
		// Token: 0x04000082 RID: 130
		DeliverSpecialMessage = 67108864U,
		// Token: 0x04000083 RID: 131
		DeliverQuotaMessage = 134217728U,
		// Token: 0x04000084 RID: 132
		NoExtendedFlags = 268435456U,
		// Token: 0x04000085 RID: 133
		SupportProgress = 536870912U,
		// Token: 0x04000086 RID: 134
		DisconnectedMailbox = 1073741824U,
		// Token: 0x04000087 RID: 135
		ApplicationIdOnly = 2147483648U
	}
}
