using System;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupJoinRequestMessageComposer : BaseGroupMessageComposer
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000DF48 File Offset: 0x0000C148
		public GroupJoinRequestMessageComposer(MailboxSession mailboxSession, ADUser groupAdUser, string attachedMessageBody)
		{
			this.mailboxSession = mailboxSession;
			this.groupAdUser = groupAdUser;
			this.attachedMessageBody = attachedMessageBody;
			this.groupOwners = (from g in groupAdUser.Session.FindByADObjectIds<ADUser>(groupAdUser.Owners.ToArray())
			where g.Data != null
			select g.Data).ToArray<ADUser>();
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000DFD5 File Offset: 0x0000C1D5
		protected override ADUser[] Recipients
		{
			get
			{
				return this.groupOwners;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000DFDD File Offset: 0x0000C1DD
		protected override Participant FromParticipant
		{
			get
			{
				return new Participant(this.mailboxSession.MailboxOwner);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		protected override string Subject
		{
			get
			{
				return Strings.JoinRequestMessageSubject(this.mailboxSession.MailboxOwner.MailboxInfo.DisplayName, this.groupAdUser.DisplayName).ToString(BaseGroupMessageComposer.GetPreferredCulture(new ADUser[]
				{
					this.groupAdUser
				}));
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E040 File Offset: 0x0000C240
		protected override void WriteMessageBody(StreamWriter streamWriter)
		{
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(this.groupAdUser, null);
			GroupJoinRequestMessageBodyBuilder.WriteMessageToStream(streamWriter, this.mailboxSession.MailboxOwner.MailboxInfo.DisplayName, this.groupAdUser.DisplayName, this.attachedMessageBody, new MailboxUrls(exchangePrincipal, false), BaseGroupMessageComposer.GetPreferredCulture(new ADUser[]
			{
				this.groupAdUser
			}));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		protected override void SetAdditionalMessageProperties(MessageItem message)
		{
			if (!(message is GroupMailboxJoinRequestMessageItem))
			{
				throw new ArgumentException();
			}
			GroupMailboxJoinRequestMessageItem groupMailboxJoinRequestMessageItem = message as GroupMailboxJoinRequestMessageItem;
			groupMailboxJoinRequestMessageItem.GroupSmtpAddress = this.groupAdUser.PrimarySmtpAddress.ToString();
			groupMailboxJoinRequestMessageItem.AutoResponseSuppress = AutoResponseSuppress.All;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000E0EC File Offset: 0x0000C2EC
		protected override void AddAttachments(MessageItem message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
		}

		// Token: 0x0400011F RID: 287
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000120 RID: 288
		private readonly ADUser groupAdUser;

		// Token: 0x04000121 RID: 289
		private readonly ADUser[] groupOwners;

		// Token: 0x04000122 RID: 290
		private readonly string attachedMessageBody;
	}
}
