using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000092 RID: 146
	public enum ElcEwsErrorType
	{
		// Token: 0x04000423 RID: 1059
		Unknown,
		// Token: 0x04000424 RID: 1060
		PrimaryExchangeWebServiceNotAvailable,
		// Token: 0x04000425 RID: 1061
		ArchiveExchangeWebServiceNotAvailable,
		// Token: 0x04000426 RID: 1062
		ExchangeWebServiceCallFailed,
		// Token: 0x04000427 RID: 1063
		FailedToGetUserConfiguration,
		// Token: 0x04000428 RID: 1064
		FailedToCreateUserConfiguration,
		// Token: 0x04000429 RID: 1065
		FailedToUpdateUserConfiguration,
		// Token: 0x0400042A RID: 1066
		FailedToDeleteUserConfiguration,
		// Token: 0x0400042B RID: 1067
		FailedToGetFolderById,
		// Token: 0x0400042C RID: 1068
		FailedToGetFolderByName,
		// Token: 0x0400042D RID: 1069
		FailedToGetFolderHierarchy,
		// Token: 0x0400042E RID: 1070
		FailedToCreateFolder,
		// Token: 0x0400042F RID: 1071
		FailedToCreateExistingFolder,
		// Token: 0x04000430 RID: 1072
		FailedToUpdateFolder,
		// Token: 0x04000431 RID: 1073
		FailedToExportItem,
		// Token: 0x04000432 RID: 1074
		FailedToUploadItem,
		// Token: 0x04000433 RID: 1075
		FailedToGetItem,
		// Token: 0x04000434 RID: 1076
		NotFound,
		// Token: 0x04000435 RID: 1077
		TargetOutOfSpace,
		// Token: 0x04000436 RID: 1078
		NoItemReturned
	}
}
