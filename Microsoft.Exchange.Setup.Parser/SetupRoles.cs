using System;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x0200000B RID: 11
	[Flags]
	public enum SetupRoles
	{
		// Token: 0x04000069 RID: 105
		None = 0,
		// Token: 0x0400006A RID: 106
		Mailbox = 1,
		// Token: 0x0400006B RID: 107
		Bridgehead = 2,
		// Token: 0x0400006C RID: 108
		ClientAccess = 4,
		// Token: 0x0400006D RID: 109
		UnifiedMessaging = 8,
		// Token: 0x0400006E RID: 110
		Gateway = 16,
		// Token: 0x0400006F RID: 111
		AdminTools = 32,
		// Token: 0x04000070 RID: 112
		Monitoring = 64,
		// Token: 0x04000071 RID: 113
		CentralAdmin = 128,
		// Token: 0x04000072 RID: 114
		CentralAdminDatabase = 256,
		// Token: 0x04000073 RID: 115
		Cafe = 512,
		// Token: 0x04000074 RID: 116
		FrontendTransport = 1024,
		// Token: 0x04000075 RID: 117
		OSP = 2048,
		// Token: 0x04000076 RID: 118
		CentralAdminFrontEnd = 4096,
		// Token: 0x04000077 RID: 119
		AllRoles = 8191
	}
}
