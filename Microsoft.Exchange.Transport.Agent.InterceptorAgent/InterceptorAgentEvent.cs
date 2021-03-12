using System;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000010 RID: 16
	[Flags]
	public enum InterceptorAgentEvent : ushort
	{
		// Token: 0x0400005D RID: 93
		Invalid = 0,
		// Token: 0x0400005E RID: 94
		OnMailFrom = 1,
		// Token: 0x0400005F RID: 95
		OnRcptTo = 2,
		// Token: 0x04000060 RID: 96
		OnEndOfHeaders = 4,
		// Token: 0x04000061 RID: 97
		OnEndOfData = 8,
		// Token: 0x04000062 RID: 98
		OnSubmittedMessage = 16,
		// Token: 0x04000063 RID: 99
		OnResolvedMessage = 32,
		// Token: 0x04000064 RID: 100
		OnRoutedMessage = 64,
		// Token: 0x04000065 RID: 101
		OnCategorizedMessage = 128,
		// Token: 0x04000066 RID: 102
		OnInitMsg = 256,
		// Token: 0x04000067 RID: 103
		OnPromotedMessage = 512,
		// Token: 0x04000068 RID: 104
		OnCreatedMessage = 1024,
		// Token: 0x04000069 RID: 105
		OnDemotedMessage = 8192,
		// Token: 0x0400006A RID: 106
		OnLoadedMessage = 16384
	}
}
