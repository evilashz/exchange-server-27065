using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.ControlPanel.DataContracts
{
	// Token: 0x020000DA RID: 218
	[DataContract]
	public class MailboxPermissionsRow : BaseRow
	{
		// Token: 0x06001D8D RID: 7565 RVA: 0x0005A6D8 File Offset: 0x000588D8
		public MailboxPermissionsRow(MailboxAcePresentationObject mailboxpo)
		{
			this.mailboxpermissionspo = mailboxpo;
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0005A6E7 File Offset: 0x000588E7
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0005A6F4 File Offset: 0x000588F4
		[DataMember]
		public bool HasReadAccess
		{
			get
			{
				return this.HasRight(MailboxRights.ReadPermission);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0005A6FB File Offset: 0x000588FB
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x0005A704 File Offset: 0x00058904
		[DataMember]
		public bool HasFullAccess
		{
			get
			{
				return this.HasRight(MailboxRights.FullAccess);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0005A70C File Offset: 0x0005890C
		private bool HasRight(MailboxRights right)
		{
			foreach (MailboxRights mailboxRights in this.mailboxpermissionspo.AccessRights)
			{
				if ((mailboxRights & right) == right)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001BE6 RID: 7142
		private MailboxAcePresentationObject mailboxpermissionspo;
	}
}
