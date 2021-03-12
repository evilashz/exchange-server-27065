using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200005B RID: 91
	internal sealed class FolderAuditLogData
	{
		// Token: 0x0600031F RID: 799 RVA: 0x00013EDB File Offset: 0x000120DB
		internal FolderAuditLogData(ProvisionedFolder provisionedFolder, MailboxData mailboxData, string elcAction) : this(provisionedFolder, mailboxData, elcAction, string.Empty)
		{
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00013EEB File Offset: 0x000120EB
		internal FolderAuditLogData(string folderFullPath, string mailboxOwner, string elcAction)
		{
			this.folderFullPath = folderFullPath;
			this.mailboxOwner = mailboxOwner;
			this.elcAction = elcAction;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00013F08 File Offset: 0x00012108
		internal FolderAuditLogData(ProvisionedFolder provisionedFolder, MailboxData mailboxData, string elcAction, string autoCopyAddress)
		{
			this.folderFullPath = provisionedFolder.FullFolderPath;
			this.mailboxOwner = mailboxData.MailboxSmtpAddress;
			this.elcAction = elcAction;
			this.autoCopyAddress = autoCopyAddress;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00013F37 File Offset: 0x00012137
		internal string ElcAction
		{
			get
			{
				return this.elcAction;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00013F3F File Offset: 0x0001213F
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00013F47 File Offset: 0x00012147
		internal string ExpirationAction
		{
			get
			{
				return this.expirationAction;
			}
			set
			{
				this.expirationAction = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00013F50 File Offset: 0x00012150
		internal string MailboxOwner
		{
			get
			{
				return this.mailboxOwner;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00013F58 File Offset: 0x00012158
		internal string FolderFullPath
		{
			get
			{
				return this.folderFullPath;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00013F60 File Offset: 0x00012160
		internal string AutoCopyAddress
		{
			get
			{
				return this.autoCopyAddress;
			}
		}

		// Token: 0x040002AA RID: 682
		private string elcAction;

		// Token: 0x040002AB RID: 683
		private string expirationAction;

		// Token: 0x040002AC RID: 684
		private string mailboxOwner;

		// Token: 0x040002AD RID: 685
		private string folderFullPath;

		// Token: 0x040002AE RID: 686
		private string autoCopyAddress;
	}
}
