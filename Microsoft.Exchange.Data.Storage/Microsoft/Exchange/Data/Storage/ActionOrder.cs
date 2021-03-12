using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B6F RID: 2927
	internal enum ActionOrder
	{
		// Token: 0x04003C51 RID: 15441
		ServerReplyMessageAction = 1,
		// Token: 0x04003C52 RID: 15442
		MarkImportanceAction,
		// Token: 0x04003C53 RID: 15443
		MarkSensitivityAction,
		// Token: 0x04003C54 RID: 15444
		AssignCategoriesAction,
		// Token: 0x04003C55 RID: 15445
		FlagMessageAction,
		// Token: 0x04003C56 RID: 15446
		MarkAsReadAction,
		// Token: 0x04003C57 RID: 15447
		ForwardToRecipientsAction,
		// Token: 0x04003C58 RID: 15448
		RedirectToRecipientsAction,
		// Token: 0x04003C59 RID: 15449
		ForwardAsAttachmentToRecipientsAction,
		// Token: 0x04003C5A RID: 15450
		SendSmsAlertToRecipientsAction,
		// Token: 0x04003C5B RID: 15451
		StopProcessingAction,
		// Token: 0x04003C5C RID: 15452
		CopyToFolderAction,
		// Token: 0x04003C5D RID: 15453
		MoveToFolderAction,
		// Token: 0x04003C5E RID: 15454
		DeleteAction,
		// Token: 0x04003C5F RID: 15455
		PermanentDeleteAction
	}
}
