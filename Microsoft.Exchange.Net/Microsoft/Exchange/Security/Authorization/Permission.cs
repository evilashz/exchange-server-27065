using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000030 RID: 48
	[Flags]
	internal enum Permission : uint
	{
		// Token: 0x040000BD RID: 189
		None = 0U,
		// Token: 0x040000BE RID: 190
		SMTPSubmit = 1U,
		// Token: 0x040000BF RID: 191
		SMTPSubmitForMLS = 2U,
		// Token: 0x040000C0 RID: 192
		SMTPAcceptAnyRecipient = 4U,
		// Token: 0x040000C1 RID: 193
		SMTPAcceptAuthenticationFlag = 8U,
		// Token: 0x040000C2 RID: 194
		SMTPAcceptAnySender = 16U,
		// Token: 0x040000C3 RID: 195
		SMTPAcceptAuthoritativeDomainSender = 32U,
		// Token: 0x040000C4 RID: 196
		BypassAntiSpam = 64U,
		// Token: 0x040000C5 RID: 197
		BypassMessageSizeLimit = 128U,
		// Token: 0x040000C6 RID: 198
		SMTPSendEXCH50 = 256U,
		// Token: 0x040000C7 RID: 199
		SMTPAcceptEXCH50 = 512U,
		// Token: 0x040000C8 RID: 200
		AcceptRoutingHeaders = 1024U,
		// Token: 0x040000C9 RID: 201
		AcceptForestHeaders = 2048U,
		// Token: 0x040000CA RID: 202
		AcceptOrganizationHeaders = 4096U,
		// Token: 0x040000CB RID: 203
		SendRoutingHeaders = 8192U,
		// Token: 0x040000CC RID: 204
		SendForestHeaders = 16384U,
		// Token: 0x040000CD RID: 205
		SendOrganizationHeaders = 32768U,
		// Token: 0x040000CE RID: 206
		SendAs = 65536U,
		// Token: 0x040000CF RID: 207
		SMTPSendXShadow = 131072U,
		// Token: 0x040000D0 RID: 208
		SMTPAcceptXShadow = 262144U,
		// Token: 0x040000D1 RID: 209
		SMTPAcceptXProxyFrom = 524288U,
		// Token: 0x040000D2 RID: 210
		SMTPAcceptXSessionParams = 1048576U,
		// Token: 0x040000D3 RID: 211
		SMTPAcceptXMessageContextADRecipientCache = 2097152U,
		// Token: 0x040000D4 RID: 212
		SMTPAcceptXMessageContextExtendedProperties = 4194304U,
		// Token: 0x040000D5 RID: 213
		SMTPAcceptXMessageContextFastIndex = 8388608U,
		// Token: 0x040000D6 RID: 214
		SMTPAcceptXAttr = 16777216U,
		// Token: 0x040000D7 RID: 215
		SMTPAcceptXSysProbe = 33554432U,
		// Token: 0x040000D8 RID: 216
		All = 67108863U
	}
}
