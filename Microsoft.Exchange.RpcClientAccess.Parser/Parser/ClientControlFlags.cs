using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200000F RID: 15
	[Flags]
	internal enum ClientControlFlags : uint
	{
		// Token: 0x04000066 RID: 102
		None = 0U,
		// Token: 0x04000067 RID: 103
		EnablePerfSendToServer = 1U,
		// Token: 0x04000068 RID: 104
		EnablePerfSendToMailbox = 2U,
		// Token: 0x04000069 RID: 105
		EnableCompression = 4U,
		// Token: 0x0400006A RID: 106
		EnableHttpTunneling = 8U,
		// Token: 0x0400006B RID: 107
		EnablePerfSendGcData = 16U
	}
}
