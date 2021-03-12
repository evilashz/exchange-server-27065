using System;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Search
{
	// Token: 0x02000192 RID: 402
	internal class MailboxStatistics
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x0005D2C0 File Offset: 0x0005B4C0
		public MailboxStatistics(string mdbName, MailboxData mailboxData, ErrorStatistics errorStatistics, StoreStatistics storeStatistics)
		{
			this.DatabaseName = mdbName;
			this.MailboxGuid = mailboxData.MailboxGuid;
			this.ErrorStatistics = errorStatistics;
			this.StoreStatistics = storeStatistics;
			MailboxType mailboxType = MailboxType.Unknown;
			StoreMailboxDataExtended storeMailboxDataExtended = mailboxData as StoreMailboxDataExtended;
			if (storeMailboxDataExtended != null)
			{
				mailboxType = MailboxType.Default;
				if (storeMailboxDataExtended.IsPublicFolderMailbox)
				{
					mailboxType |= MailboxType.PublicFolder;
				}
				if (storeMailboxDataExtended.IsArchiveMailbox)
				{
					mailboxType |= MailboxType.Archive;
				}
				if (storeMailboxDataExtended.IsGroupMailbox)
				{
					mailboxType |= MailboxType.ModernGroup;
				}
				if (storeMailboxDataExtended.IsSharedMailbox)
				{
					mailboxType |= MailboxType.Shared;
				}
				if (storeMailboxDataExtended.IsTeamSiteMailbox)
				{
					mailboxType |= MailboxType.TeamSite;
				}
			}
			this.Type = mailboxType;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0005D348 File Offset: 0x0005B548
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x0005D350 File Offset: 0x0005B550
		public string DatabaseName { get; private set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0005D359 File Offset: 0x0005B559
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x0005D361 File Offset: 0x0005B561
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0005D36A File Offset: 0x0005B56A
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0005D372 File Offset: 0x0005B572
		public ErrorStatistics ErrorStatistics { get; private set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0005D37B File Offset: 0x0005B57B
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0005D383 File Offset: 0x0005B583
		public StoreStatistics StoreStatistics { get; private set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0005D38C File Offset: 0x0005B58C
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0005D394 File Offset: 0x0005B594
		public MailboxType Type { get; private set; }
	}
}
