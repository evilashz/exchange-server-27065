using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000073 RID: 115
	[Flags]
	public enum EventFlags
	{
		// Token: 0x04000449 RID: 1097
		None = 0,
		// Token: 0x0400044A RID: 1098
		Associated = 1,
		// Token: 0x0400044B RID: 1099
		ContentOnly = 16,
		// Token: 0x0400044C RID: 1100
		ModifiedByMove = 128,
		// Token: 0x0400044D RID: 1101
		Source = 256,
		// Token: 0x0400044E RID: 1102
		Destination = 512,
		// Token: 0x0400044F RID: 1103
		SearchFolder = 2048,
		// Token: 0x04000450 RID: 1104
		Conversation = 4096,
		// Token: 0x04000451 RID: 1105
		CommonCategorizationPropertyChanged = 8192
	}
}
