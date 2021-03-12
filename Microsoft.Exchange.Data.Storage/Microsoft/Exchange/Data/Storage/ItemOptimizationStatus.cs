using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000240 RID: 576
	[Flags]
	internal enum ItemOptimizationStatus
	{
		// Token: 0x04001124 RID: 4388
		None = 0,
		// Token: 0x04001125 RID: 4389
		LeafNode = 1,
		// Token: 0x04001126 RID: 4390
		Extracted = 2,
		// Token: 0x04001127 RID: 4391
		Opened = 4,
		// Token: 0x04001128 RID: 4392
		SummaryConstructed = 8,
		// Token: 0x04001129 RID: 4393
		BodyTagNotPresent = 16,
		// Token: 0x0400112A RID: 4394
		BodyTagMismatched = 32,
		// Token: 0x0400112B RID: 4395
		BodyFormatMismatched = 64,
		// Token: 0x0400112C RID: 4396
		NonMsHeader = 128,
		// Token: 0x0400112D RID: 4397
		ExtraPropertiesNeeded = 256,
		// Token: 0x0400112E RID: 4398
		ParticipantNotFound = 512,
		// Token: 0x0400112F RID: 4399
		AttachmentPresnet = 1024,
		// Token: 0x04001130 RID: 4400
		PossibleInlines = 2048,
		// Token: 0x04001131 RID: 4401
		IrmProtected = 4096,
		// Token: 0x04001132 RID: 4402
		MapiAttachmentPresent = 8192
	}
}
