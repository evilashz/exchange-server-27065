using System;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x0200000E RID: 14
	internal enum Pop3ClientState
	{
		// Token: 0x04000055 RID: 85
		ProcessConnection,
		// Token: 0x04000056 RID: 86
		ProcessCapaCommand,
		// Token: 0x04000057 RID: 87
		ProcessTopCommand,
		// Token: 0x04000058 RID: 88
		ProcessStlsCommand,
		// Token: 0x04000059 RID: 89
		ProcessAuthNtlmCommand,
		// Token: 0x0400005A RID: 90
		ProcessUserCommand,
		// Token: 0x0400005B RID: 91
		ProcessPassCommand,
		// Token: 0x0400005C RID: 92
		ProcessStatCommand,
		// Token: 0x0400005D RID: 93
		ProcessUidlCommand,
		// Token: 0x0400005E RID: 94
		ProcessListCommand,
		// Token: 0x0400005F RID: 95
		ProcessRetrCommand,
		// Token: 0x04000060 RID: 96
		ProcessDeleCommand,
		// Token: 0x04000061 RID: 97
		ProcessQuitCommand,
		// Token: 0x04000062 RID: 98
		ProcessEnd
	}
}
