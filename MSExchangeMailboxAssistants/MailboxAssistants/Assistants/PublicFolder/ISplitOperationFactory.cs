using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200016C RID: 364
	internal interface ISplitOperationFactory
	{
		// Token: 0x06000EB0 RID: 3760
		ISplitOperation CreateIdentifyTargetMailboxOperation(IPublicFolderSplitState splitState);

		// Token: 0x06000EB1 RID: 3761
		ISplitOperation CreatePrepareTargetMailboxOperation(IPublicFolderSplitState splitState);

		// Token: 0x06000EB2 RID: 3762
		ISplitOperation CreatePrepareSplitPlanOperation(IPublicFolderSplitState splitState);

		// Token: 0x06000EB3 RID: 3763
		ISplitOperation CreateMoveContentOperation(IPublicFolderSplitState splitState);
	}
}
