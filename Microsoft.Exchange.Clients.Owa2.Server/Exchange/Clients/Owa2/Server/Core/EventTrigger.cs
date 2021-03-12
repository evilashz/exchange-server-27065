using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C7 RID: 967
	[Flags]
	public enum EventTrigger
	{
		// Token: 0x04001185 RID: 4485
		RecipientWell = 1,
		// Token: 0x04001186 RID: 4486
		AutoSave = 2,
		// Token: 0x04001187 RID: 4487
		Save = 4,
		// Token: 0x04001188 RID: 4488
		AttachmentAdded = 8,
		// Token: 0x04001189 RID: 4489
		AttachmentRemoved = 16,
		// Token: 0x0400118A RID: 4490
		OpenDraft = 32,
		// Token: 0x0400118B RID: 4491
		Undo = 64
	}
}
