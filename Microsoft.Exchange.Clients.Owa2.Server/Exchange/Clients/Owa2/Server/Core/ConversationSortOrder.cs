using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000D0 RID: 208
	[Flags]
	public enum ConversationSortOrder
	{
		// Token: 0x04000497 RID: 1175
		Chronological = 1,
		// Token: 0x04000498 RID: 1176
		Tree = 2,
		// Token: 0x04000499 RID: 1177
		NewestOnTop = 4,
		// Token: 0x0400049A RID: 1178
		NewestOnBottom = 8,
		// Token: 0x0400049B RID: 1179
		ChronologicalNewestOnTop = 5,
		// Token: 0x0400049C RID: 1180
		ChronologicalNewestOnBottom = 9,
		// Token: 0x0400049D RID: 1181
		TreeNewestOnBottom = 10
	}
}
