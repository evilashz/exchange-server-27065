using System;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B16 RID: 2838
	public enum TaskFolderActionError
	{
		// Token: 0x04002CF8 RID: 11512
		None,
		// Token: 0x04002CF9 RID: 11513
		TaskFolderActionInvalidGroupId,
		// Token: 0x04002CFA RID: 11514
		TaskFolderActionCannotSaveGroup,
		// Token: 0x04002CFB RID: 11515
		TaskFolderActionFolderIdNotTaskFolderFolder,
		// Token: 0x04002CFC RID: 11516
		TaskFolderActionInvalidTaskFolderName,
		// Token: 0x04002CFD RID: 11517
		TaskFolderActionUnableToCreateTaskFolder,
		// Token: 0x04002CFE RID: 11518
		TaskFolderActionUnableToRenameTaskFolder,
		// Token: 0x04002CFF RID: 11519
		TaskFolderActionTaskFolderAlreadyExists,
		// Token: 0x04002D00 RID: 11520
		TaskFolderActionUnableToCreateTaskFolderNode,
		// Token: 0x04002D01 RID: 11521
		TaskFolderActionInvalidItemId,
		// Token: 0x04002D02 RID: 11522
		TaskFolderActionFolderIdIsDefaultTaskFolder,
		// Token: 0x04002D03 RID: 11523
		TaskFolderActionCannotRename,
		// Token: 0x04002D04 RID: 11524
		TaskFolderActionCannotRenameTaskFolderNode,
		// Token: 0x04002D05 RID: 11525
		TaskFolderActionCannotDeleteTaskFolder,
		// Token: 0x04002D06 RID: 11526
		TaskFolderActionInvalidTaskFolderNodeOrder,
		// Token: 0x04002D07 RID: 11527
		TaskFolderActionUnableToUpdateTaskFolderNode,
		// Token: 0x04002D08 RID: 11528
		TaskFolderActionUnableToFindGroupWithId
	}
}
