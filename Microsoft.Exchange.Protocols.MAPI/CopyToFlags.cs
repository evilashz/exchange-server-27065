using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200004A RID: 74
	[Flags]
	public enum CopyToFlags
	{
		// Token: 0x0400011A RID: 282
		None = 0,
		// Token: 0x0400011B RID: 283
		MoveProperties = 1,
		// Token: 0x0400011C RID: 284
		DoNotReplaceProperties = 2,
		// Token: 0x0400011D RID: 285
		CopyRecipients = 4,
		// Token: 0x0400011E RID: 286
		CopyAttachments = 8,
		// Token: 0x0400011F RID: 287
		CopyHierarchy = 16,
		// Token: 0x04000120 RID: 288
		CopyContent = 32,
		// Token: 0x04000121 RID: 289
		CopyHiddenItems = 64,
		// Token: 0x04000122 RID: 290
		CopyEmbeddedMessage = 128,
		// Token: 0x04000123 RID: 291
		CopyFirstLevelEmbeddedMessage = 256
	}
}
