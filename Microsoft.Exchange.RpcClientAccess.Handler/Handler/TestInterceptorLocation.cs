using System;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000021 RID: 33
	[Flags]
	internal enum TestInterceptorLocation
	{
		// Token: 0x04000083 RID: 131
		None = 0,
		// Token: 0x04000084 RID: 132
		ExceptionTranslator_TryExecuteCatchAndTranslateExceptions = 1,
		// Token: 0x04000085 RID: 133
		Logon_CreateStoreSession = 2,
		// Token: 0x04000086 RID: 134
		AsyncOperationExecutor_SegmentedOperation = 4,
		// Token: 0x04000087 RID: 135
		EmptyFolderSegmentedOperation_CoreFolderBind = 8,
		// Token: 0x04000088 RID: 136
		EmptyFolderSegmentedOperation_CoreFolderQuery = 16,
		// Token: 0x04000089 RID: 137
		EmptyFolderSegmentedOperation_CoreFolderDeleteMessages = 32,
		// Token: 0x0400008A RID: 138
		EmptyFolderSegmentedOperation_EmptyFolderHierarchy = 64,
		// Token: 0x0400008B RID: 139
		NotificationQueue_FromNotificationData = 128,
		// Token: 0x0400008C RID: 140
		CopyFolderSegmentedOperation_CoreFolderDeletedCreatingNewSubFolder = 256,
		// Token: 0x0400008D RID: 141
		CopyFolderSegmentedOperation_CoreFolderDeletedAboutToQueryingSubFolders = 512,
		// Token: 0x0400008E RID: 142
		CopyFolderSegmentedOperation_CoreFolderDeletedAboutToPeruseSubFolders = 1024,
		// Token: 0x0400008F RID: 143
		CopyFolderSegmentedOperation_CoreFolderDeletedAboutToCopySubFolder = 2048,
		// Token: 0x04000090 RID: 144
		CopyFolderSegmentedOperation_CoreFolderDeletedAboutToCreateNewSubFolder = 4096,
		// Token: 0x04000091 RID: 145
		CopyFolderSegmentedOperation_CoreFolderDeletedWhenAboutToCopyMessages = 8192,
		// Token: 0x04000092 RID: 146
		CopyFolderSegmentedOperation_CoreFolderDeletedWhenDoingCopyMessages = 16384,
		// Token: 0x04000093 RID: 147
		CopyFolderSegmentedOperation_CoreFolderDeletedWhenDoingNextCopyMessages = 32768,
		// Token: 0x04000094 RID: 148
		SetReadFlagsSegmentedOperation_InternalDoNextBatchOperation = 65536,
		// Token: 0x04000095 RID: 149
		RopHandler_SetReadFlags = 131072,
		// Token: 0x04000096 RID: 150
		DeleteMessagesSegmentedOperation_InternalDoNextBatchOperation = 262144,
		// Token: 0x04000097 RID: 151
		Logon_FindExchangePrincipal = 524288
	}
}
