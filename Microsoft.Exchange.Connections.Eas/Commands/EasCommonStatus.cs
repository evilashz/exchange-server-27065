using System;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x0200000E RID: 14
	public enum EasCommonStatus
	{
		// Token: 0x0400007B RID: 123
		InvalidContent = 4197,
		// Token: 0x0400007C RID: 124
		ServerError = 8302,
		// Token: 0x0400007D RID: 125
		MaximumDevicesReached = 8369,
		// Token: 0x0400007E RID: 126
		CompositeStatusError = 510,
		// Token: 0x0400007F RID: 127
		StatusOutOfRange,
		// Token: 0x04000080 RID: 128
		LowOrderByte = 255,
		// Token: 0x04000081 RID: 129
		HighOrderByte = 65280,
		// Token: 0x04000082 RID: 130
		TransientError = 256,
		// Token: 0x04000083 RID: 131
		RespondsToSyncKeyReset = 512,
		// Token: 0x04000084 RID: 132
		RequiresSyncKeyReset = 1024,
		// Token: 0x04000085 RID: 133
		RequiresFolderSync = 2048,
		// Token: 0x04000086 RID: 134
		PermanentError = 4096,
		// Token: 0x04000087 RID: 135
		BackOff = 8192
	}
}
