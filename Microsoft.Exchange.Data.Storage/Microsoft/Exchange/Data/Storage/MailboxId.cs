using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D88 RID: 3464
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxId
	{
		// Token: 0x06007747 RID: 30535 RVA: 0x0020E601 File Offset: 0x0020C801
		public MailboxId(string smtpAddress) : this(smtpAddress, false)
		{
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x0020E60B File Offset: 0x0020C80B
		public MailboxId(string smtpAddress, bool isArchive)
		{
			this.smtpAddress = smtpAddress;
			this.mailboxGuid = null;
			this.isVersionDependent = false;
			this.isArchive = isArchive;
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x0020E62F File Offset: 0x0020C82F
		public MailboxId(Guid mailboxGuid) : this(mailboxGuid, false)
		{
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x0020E63C File Offset: 0x0020C83C
		public MailboxId(Guid mailboxGuid, bool isArchive)
		{
			if (Guid.Empty == mailboxGuid)
			{
				throw new NonExistentMailboxGuidException(mailboxGuid);
			}
			this.smtpAddress = null;
			this.mailboxGuid = mailboxGuid.ToString().ToLowerInvariant();
			this.isVersionDependent = false;
			this.isArchive = isArchive;
		}

		// Token: 0x0600774B RID: 30539 RVA: 0x0020E690 File Offset: 0x0020C890
		public MailboxId(MailboxSession mailboxSession)
		{
			this.smtpAddress = mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			this.mailboxGuid = mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid.ToString().ToLowerInvariant();
			this.isArchive = mailboxSession.MailboxOwner.MailboxInfo.IsArchive;
			this.isVersionDependent = true;
		}

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x0600774C RID: 30540 RVA: 0x0020E70D File Offset: 0x0020C90D
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x0600774D RID: 30541 RVA: 0x0020E715 File Offset: 0x0020C915
		public string MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x0600774E RID: 30542 RVA: 0x0020E71D File Offset: 0x0020C91D
		public bool IsVersionDependent
		{
			get
			{
				return this.isVersionDependent;
			}
		}

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x0600774F RID: 30543 RVA: 0x0020E725 File Offset: 0x0020C925
		public bool IsArchive
		{
			get
			{
				return this.isArchive;
			}
		}

		// Token: 0x0400529C RID: 21148
		private string smtpAddress;

		// Token: 0x0400529D RID: 21149
		private string mailboxGuid;

		// Token: 0x0400529E RID: 21150
		private bool isVersionDependent;

		// Token: 0x0400529F RID: 21151
		private bool isArchive;
	}
}
