using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000090 RID: 144
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupMembershipMailboxReader : IGroupMembershipReader<GroupMailbox>
	{
		// Token: 0x06000386 RID: 902 RVA: 0x00011A18 File Offset: 0x0000FC18
		public GroupMembershipMailboxReader(UserMailboxLocator mailbox, IRecipientSession adSession, MailboxSession mailboxSession)
		{
			ArgumentValidator.ThrowIfNull("mailbox", mailbox);
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			this.mailbox = mailbox;
			this.adSession = adSession;
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011A78 File Offset: 0x0000FC78
		public IEnumerable<GroupMailbox> GetJoinedGroups()
		{
			IEnumerable<GroupMailbox> groupMailboxes = null;
			GroupMailboxAccessLayer.Execute("GroupMembershipMailboxReader", this.adSession, this.mailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
			{
				groupMailboxes = accessLayer.GetJoinedGroups(this.mailbox, true);
			});
			return groupMailboxes ?? ((IEnumerable<GroupMailbox>)new GroupMailbox[0]);
		}

		// Token: 0x040005FA RID: 1530
		private readonly IRecipientSession adSession;

		// Token: 0x040005FB RID: 1531
		private readonly MailboxSession mailboxSession;

		// Token: 0x040005FC RID: 1532
		private readonly UserMailboxLocator mailbox;
	}
}
