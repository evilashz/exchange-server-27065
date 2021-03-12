using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B6E RID: 2926
	internal enum ActionType
	{
		// Token: 0x04003C41 RID: 15425
		MoveToFolderAction = 1,
		// Token: 0x04003C42 RID: 15426
		DeleteAction,
		// Token: 0x04003C43 RID: 15427
		CopyToFolderAction,
		// Token: 0x04003C44 RID: 15428
		ForwardToRecipientsAction,
		// Token: 0x04003C45 RID: 15429
		ForwardAsAttachmentToRecipientsAction,
		// Token: 0x04003C46 RID: 15430
		RedirectToRecipientsAction,
		// Token: 0x04003C47 RID: 15431
		ServerReplyMessageAction,
		// Token: 0x04003C48 RID: 15432
		MarkImportanceAction,
		// Token: 0x04003C49 RID: 15433
		MarkSensitivityAction,
		// Token: 0x04003C4A RID: 15434
		StopProcessingAction,
		// Token: 0x04003C4B RID: 15435
		SendSmsAlertToRecipientsAction,
		// Token: 0x04003C4C RID: 15436
		AssignCategoriesAction,
		// Token: 0x04003C4D RID: 15437
		PermanentDeleteAction,
		// Token: 0x04003C4E RID: 15438
		FlagMessageAction,
		// Token: 0x04003C4F RID: 15439
		MarkAsReadAction
	}
}
