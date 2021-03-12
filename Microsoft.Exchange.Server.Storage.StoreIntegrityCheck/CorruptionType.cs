using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200001C RID: 28
	public enum CorruptionType
	{
		// Token: 0x0400004E RID: 78
		[MapToManagement(null, false)]
		MessageIdUniqueIndexExists,
		// Token: 0x0400004F RID: 79
		[MapToManagement(null, false)]
		IncorrectRuleMessageClass,
		// Token: 0x04000050 RID: 80
		[MapToManagement(null, false)]
		ExtraJunkmailRule,
		// Token: 0x04000051 RID: 81
		[MapToManagement(null, false)]
		NoAclStampedOnFolder,
		// Token: 0x04000052 RID: 82
		[MapToManagement(null, false)]
		WrongFolderTypeOnRestrictionFolder,
		// Token: 0x04000053 RID: 83
		[MapToManagement(null, false)]
		UndeletedMessageInMidsetDeleted,
		// Token: 0x04000054 RID: 84
		[MapToManagement(null, false)]
		SearchBacklinksUnsorted,
		// Token: 0x04000055 RID: 85
		[MapToManagement(null, false)]
		SearchFolderNotFound,
		// Token: 0x04000056 RID: 86
		[MapToManagement(null, false)]
		SearchBacklinkNotSearchFolder,
		// Token: 0x04000057 RID: 87
		[MapToManagement(null, false)]
		SearchBacklinkIsNotDynamicSearchFolder,
		// Token: 0x04000058 RID: 88
		[MapToManagement(null, false)]
		FolderOutOfSearchScope,
		// Token: 0x04000059 RID: 89
		[MapToManagement(null, false)]
		SearchBacklinksRecursiveMismatch,
		// Token: 0x0400005A RID: 90
		[MapToManagement(null, false)]
		SearchBacklinksDuplicatedFolder,
		// Token: 0x0400005B RID: 91
		[MapToManagement(null, false)]
		AggregateCountMismatch,
		// Token: 0x0400005C RID: 92
		[MapToManagement(null, false)]
		MissingSpecialFolder,
		// Token: 0x0400005D RID: 93
		[MapToManagement(null, false)]
		InvalidImapID,
		// Token: 0x0400005E RID: 94
		[MapToManagement(null, true)]
		FolderHierarchyRootCountMismatch,
		// Token: 0x0400005F RID: 95
		[MapToManagement(null, true)]
		FolderHierarchyTotalFolderCountMismatch,
		// Token: 0x04000060 RID: 96
		[MapToManagement(null, true)]
		FolderChildrenCountMismatch,
		// Token: 0x04000061 RID: 97
		[MapToManagement(null, true)]
		FolderInformationFidMismatch,
		// Token: 0x04000062 RID: 98
		[MapToManagement(null, true)]
		FolderInformationIsSearchFolderMismatch,
		// Token: 0x04000063 RID: 99
		[MapToManagement(null, true)]
		FolderInformationDisplayNameMismatch,
		// Token: 0x04000064 RID: 100
		[MapToManagement(null, true)]
		FolderInformationIsPartOfContentIndexingMismatch,
		// Token: 0x04000065 RID: 101
		[MapToManagement(null, true)]
		FolderInformationMessageCountMismatch
	}
}
