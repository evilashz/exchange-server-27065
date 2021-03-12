using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200007E RID: 126
	[DataContract]
	public class MailboxFolderPermissionIdentity : Identity
	{
		// Token: 0x06001B69 RID: 7017 RVA: 0x00057034 File Offset: 0x00055234
		public MailboxFolderPermissionIdentity(MailboxFolderPermission permission) : base((permission.User.ADRecipient != null) ? permission.User.ADRecipient.Guid.ToString() : permission.User.ToString(), permission.User.ToString())
		{
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0005708A File Offset: 0x0005528A
		public MailboxFolderPermissionIdentity(Identity userId, Identity mailboxFolderId) : base(userId.RawIdentity, userId.DisplayName)
		{
			this.MailboxFolderId = mailboxFolderId;
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000570A5 File Offset: 0x000552A5
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x000570AD File Offset: 0x000552AD
		[DataMember]
		public Identity MailboxFolderId { get; set; }
	}
}
