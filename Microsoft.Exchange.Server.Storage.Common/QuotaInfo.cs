using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200007A RID: 122
	public class QuotaInfo
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000130D9 File Offset: 0x000112D9
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x000130E1 File Offset: 0x000112E1
		public UnlimitedBytes MailboxWarningQuota { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000130EA File Offset: 0x000112EA
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x000130F2 File Offset: 0x000112F2
		public UnlimitedBytes MailboxSendQuota { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000130FB File Offset: 0x000112FB
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00013103 File Offset: 0x00011303
		public UnlimitedBytes MailboxShutoffQuota { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001310C File Offset: 0x0001130C
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00013114 File Offset: 0x00011314
		public UnlimitedBytes DumpsterWarningQuota { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001311D File Offset: 0x0001131D
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00013125 File Offset: 0x00011325
		public UnlimitedBytes DumpsterShutoffQuota { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001312E File Offset: 0x0001132E
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00013136 File Offset: 0x00011336
		public UnlimitedItems MailboxMessagesPerFolderCountWarningQuota { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001313F File Offset: 0x0001133F
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x00013147 File Offset: 0x00011347
		public UnlimitedItems MailboxMessagesPerFolderCountReceiveQuota { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00013150 File Offset: 0x00011350
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00013158 File Offset: 0x00011358
		public UnlimitedItems DumpsterMessagesPerFolderCountWarningQuota { get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00013161 File Offset: 0x00011361
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00013169 File Offset: 0x00011369
		public UnlimitedItems DumpsterMessagesPerFolderCountReceiveQuota { get; private set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00013172 File Offset: 0x00011372
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001317A File Offset: 0x0001137A
		public UnlimitedItems FolderHierarchyChildrenCountWarningQuota { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00013183 File Offset: 0x00011383
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001318B File Offset: 0x0001138B
		public UnlimitedItems FolderHierarchyChildrenCountReceiveQuota { get; private set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00013194 File Offset: 0x00011394
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001319C File Offset: 0x0001139C
		public UnlimitedItems FolderHierarchyDepthWarningQuota { get; private set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000131A5 File Offset: 0x000113A5
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x000131AD File Offset: 0x000113AD
		public UnlimitedItems FolderHierarchyDepthReceiveQuota { get; private set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000131B6 File Offset: 0x000113B6
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x000131BE File Offset: 0x000113BE
		public UnlimitedItems FoldersCountWarningQuota { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000131C7 File Offset: 0x000113C7
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x000131CF File Offset: 0x000113CF
		public UnlimitedItems FoldersCountReceiveQuota { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000131D8 File Offset: 0x000113D8
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x000131E0 File Offset: 0x000113E0
		public UnlimitedItems NamedPropertiesCountQuota { get; private set; }

		// Token: 0x060006D6 RID: 1750 RVA: 0x000131EC File Offset: 0x000113EC
		public QuotaInfo(UnlimitedBytes mailboxWarningQuota, UnlimitedBytes mailboxSendQuota, UnlimitedBytes mailboxShutoffQuota, UnlimitedBytes dumpsterWarningQuota, UnlimitedBytes dumpsterShutoffQuota, UnlimitedItems mailboxMessagesPerFolderCountWarningQuota, UnlimitedItems mailboxMessagesPerFolderCountReceiveQuota, UnlimitedItems dumpsterMessagesPerFolderCountWarningQuota, UnlimitedItems dumpsterMessagesPerFolderCountReceiveQuota, UnlimitedItems folderHierarchyChildCountWarningQuota, UnlimitedItems folderHierarchyChildCountReceiveQuota, UnlimitedItems folderHierarchyDepthWarningQuota, UnlimitedItems folderHierarchyDepthReceiveQuota, UnlimitedItems foldersCountWarningQuota, UnlimitedItems foldersCountReceiveQuota, UnlimitedItems namedPropertiesCountQuota)
		{
			this.MailboxWarningQuota = mailboxWarningQuota;
			this.MailboxSendQuota = mailboxSendQuota;
			this.MailboxShutoffQuota = mailboxShutoffQuota;
			this.DumpsterWarningQuota = dumpsterWarningQuota;
			this.DumpsterShutoffQuota = dumpsterShutoffQuota;
			this.MailboxMessagesPerFolderCountWarningQuota = mailboxMessagesPerFolderCountWarningQuota;
			this.MailboxMessagesPerFolderCountReceiveQuota = mailboxMessagesPerFolderCountReceiveQuota;
			this.DumpsterMessagesPerFolderCountWarningQuota = dumpsterMessagesPerFolderCountWarningQuota;
			this.DumpsterMessagesPerFolderCountReceiveQuota = dumpsterMessagesPerFolderCountReceiveQuota;
			this.FolderHierarchyChildrenCountWarningQuota = folderHierarchyChildCountWarningQuota;
			this.FolderHierarchyChildrenCountReceiveQuota = folderHierarchyChildCountReceiveQuota;
			this.FolderHierarchyDepthWarningQuota = folderHierarchyDepthWarningQuota;
			this.FolderHierarchyDepthReceiveQuota = folderHierarchyDepthReceiveQuota;
			this.FoldersCountWarningQuota = foldersCountWarningQuota;
			this.FoldersCountReceiveQuota = foldersCountReceiveQuota;
			this.NamedPropertiesCountQuota = namedPropertiesCountQuota;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001327C File Offset: 0x0001147C
		public QuotaInfo(UnlimitedBytes mailboxWarningQuota, UnlimitedBytes mailboxSendQuota, UnlimitedBytes mailboxShutoffQuota, UnlimitedBytes dumpsterWarningQuota, UnlimitedBytes dumpsterShutoffQuota)
		{
			this.MailboxWarningQuota = mailboxWarningQuota;
			this.MailboxSendQuota = mailboxSendQuota;
			this.MailboxShutoffQuota = mailboxShutoffQuota;
			this.DumpsterWarningQuota = dumpsterWarningQuota;
			this.DumpsterShutoffQuota = dumpsterShutoffQuota;
			this.MailboxMessagesPerFolderCountWarningQuota = UnlimitedItems.UnlimitedValue;
			this.MailboxMessagesPerFolderCountReceiveQuota = UnlimitedItems.UnlimitedValue;
			this.DumpsterMessagesPerFolderCountWarningQuota = UnlimitedItems.UnlimitedValue;
			this.DumpsterMessagesPerFolderCountReceiveQuota = UnlimitedItems.UnlimitedValue;
			this.FolderHierarchyChildrenCountWarningQuota = UnlimitedItems.UnlimitedValue;
			this.FolderHierarchyChildrenCountReceiveQuota = UnlimitedItems.UnlimitedValue;
			this.FolderHierarchyDepthWarningQuota = UnlimitedItems.UnlimitedValue;
			this.FolderHierarchyDepthReceiveQuota = UnlimitedItems.UnlimitedValue;
			this.FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue;
			this.FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue;
			this.NamedPropertiesCountQuota = UnlimitedItems.UnlimitedValue;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00013330 File Offset: 0x00011530
		public QuotaInfo(UnlimitedItems mailboxMessagesPerFolderCountWarningQuota, UnlimitedItems mailboxMessagesPerFolderCountReceiveQuota, UnlimitedItems dumpsterMessagesPerFolderCountWarningQuota, UnlimitedItems dumpsterMessagesPerFolderCountReceiveQuota, UnlimitedItems folderHierarchyChildCountWarningQuota, UnlimitedItems folderHierarchyChildCountReceiveQuota, UnlimitedItems folderHierarchyDepthWarningQuota, UnlimitedItems folderHierarchyDepthReceiveQuota, UnlimitedItems foldersCountWarningQuota, UnlimitedItems foldersCountReceiveQuota, UnlimitedItems namedPropertiesCountQuota)
		{
			this.MailboxWarningQuota = UnlimitedBytes.UnlimitedValue;
			this.MailboxSendQuota = UnlimitedBytes.UnlimitedValue;
			this.MailboxShutoffQuota = UnlimitedBytes.UnlimitedValue;
			this.DumpsterWarningQuota = UnlimitedBytes.UnlimitedValue;
			this.DumpsterShutoffQuota = UnlimitedBytes.UnlimitedValue;
			this.MailboxMessagesPerFolderCountWarningQuota = mailboxMessagesPerFolderCountWarningQuota;
			this.MailboxMessagesPerFolderCountReceiveQuota = mailboxMessagesPerFolderCountReceiveQuota;
			this.DumpsterMessagesPerFolderCountWarningQuota = dumpsterMessagesPerFolderCountWarningQuota;
			this.DumpsterMessagesPerFolderCountReceiveQuota = dumpsterMessagesPerFolderCountReceiveQuota;
			this.FolderHierarchyChildrenCountWarningQuota = folderHierarchyChildCountWarningQuota;
			this.FolderHierarchyChildrenCountReceiveQuota = folderHierarchyChildCountReceiveQuota;
			this.FolderHierarchyDepthWarningQuota = folderHierarchyDepthWarningQuota;
			this.FolderHierarchyDepthReceiveQuota = folderHierarchyDepthReceiveQuota;
			this.FoldersCountWarningQuota = foldersCountWarningQuota;
			this.FoldersCountReceiveQuota = foldersCountReceiveQuota;
			this.NamedPropertiesCountQuota = namedPropertiesCountQuota;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000133CF File Offset: 0x000115CF
		public void MergeQuotaFromAD(QuotaInfo quotaInfo)
		{
			this.MailboxWarningQuota = quotaInfo.MailboxWarningQuota;
			this.MailboxSendQuota = quotaInfo.MailboxSendQuota;
			this.MailboxShutoffQuota = quotaInfo.MailboxShutoffQuota;
			this.DumpsterWarningQuota = quotaInfo.DumpsterWarningQuota;
			this.DumpsterShutoffQuota = quotaInfo.DumpsterShutoffQuota;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00013410 File Offset: 0x00011610
		public void ResetFolderRelatedQuotaToUnlimited()
		{
			this.FolderHierarchyChildrenCountWarningQuota = UnlimitedItems.UnlimitedValue;
			this.FolderHierarchyChildrenCountReceiveQuota = UnlimitedItems.UnlimitedValue;
			this.FolderHierarchyDepthWarningQuota = UnlimitedItems.UnlimitedValue;
			this.FolderHierarchyDepthReceiveQuota = UnlimitedItems.UnlimitedValue;
			this.FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue;
			this.FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue;
		}

		// Token: 0x0400063E RID: 1598
		public static readonly QuotaInfo Unlimited = new QuotaInfo(UnlimitedBytes.UnlimitedValue, UnlimitedBytes.UnlimitedValue, UnlimitedBytes.UnlimitedValue, UnlimitedBytes.UnlimitedValue, UnlimitedBytes.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue, UnlimitedItems.UnlimitedValue);
	}
}
