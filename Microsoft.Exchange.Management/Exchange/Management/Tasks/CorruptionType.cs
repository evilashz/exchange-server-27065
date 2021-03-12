using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D9C RID: 3484
	public enum CorruptionType
	{
		// Token: 0x040040B3 RID: 16563
		MessageIdUniqueIndexExists,
		// Token: 0x040040B4 RID: 16564
		IncorrectRuleMessageClass,
		// Token: 0x040040B5 RID: 16565
		ExtraJunkmailRule,
		// Token: 0x040040B6 RID: 16566
		NoAclStampedOnFolder,
		// Token: 0x040040B7 RID: 16567
		WrongFolderTypeOnRestrictionFolder,
		// Token: 0x040040B8 RID: 16568
		UndeletedMessageInMidsetDeleted,
		// Token: 0x040040B9 RID: 16569
		SearchBacklinksUnsorted,
		// Token: 0x040040BA RID: 16570
		SearchFolderNotFound,
		// Token: 0x040040BB RID: 16571
		SearchBacklinkNotSearchFolder,
		// Token: 0x040040BC RID: 16572
		SearchBacklinkIsNotDynamicSearchFolder,
		// Token: 0x040040BD RID: 16573
		FolderOutOfSearchScope,
		// Token: 0x040040BE RID: 16574
		SearchBacklinksRecursiveMismatch,
		// Token: 0x040040BF RID: 16575
		SearchBacklinksDuplicatedFolder,
		// Token: 0x040040C0 RID: 16576
		AggregateCountMismatch,
		// Token: 0x040040C1 RID: 16577
		MissingSpecialFolder,
		// Token: 0x040040C2 RID: 16578
		InvalidImapID
	}
}
