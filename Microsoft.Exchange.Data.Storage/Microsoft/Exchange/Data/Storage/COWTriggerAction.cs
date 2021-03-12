using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000638 RID: 1592
	public enum COWTriggerAction
	{
		// Token: 0x04002424 RID: 9252
		Create,
		// Token: 0x04002425 RID: 9253
		Update,
		// Token: 0x04002426 RID: 9254
		ItemBind,
		// Token: 0x04002427 RID: 9255
		Submit,
		// Token: 0x04002428 RID: 9256
		Copy,
		// Token: 0x04002429 RID: 9257
		Move,
		// Token: 0x0400242A RID: 9258
		MoveToDeletedItems,
		// Token: 0x0400242B RID: 9259
		SoftDelete,
		// Token: 0x0400242C RID: 9260
		HardDelete,
		// Token: 0x0400242D RID: 9261
		DoneWithMessageDelete,
		// Token: 0x0400242E RID: 9262
		FolderBind
	}
}
