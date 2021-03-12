using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F6D RID: 3949
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxOwnerFactory
	{
		// Token: 0x060086FC RID: 34556 RVA: 0x0025031D File Offset: 0x0024E51D
		public MailboxOwnerFactory(IMailboxSession session)
		{
			this.session = session;
		}

		// Token: 0x060086FD RID: 34557 RVA: 0x0025032C File Offset: 0x0024E52C
		public IMailboxOwner Create()
		{
			if (this.session.IsGroupMailbox())
			{
				return new GroupMailboxOwnerAdapter();
			}
			if (this.session.IsMoveUser)
			{
				return new SystemServiceMailboxOwnerAdapter();
			}
			return new ExchangePrincipalMailboxOwnerAdapter(this.session.MailboxOwner, this.session.MailboxOwner.GetContext(null), this.session.MailboxOwner.RecipientTypeDetails, this.session.LogonType);
		}

		// Token: 0x060086FE RID: 34558 RVA: 0x0025039C File Offset: 0x0024E59C
		public IMailboxOwner Create(MiniRecipient recipient)
		{
			if (recipient == null)
			{
				return new NullMailboxOwnerAdapter();
			}
			if (this.session.IsGroupMailbox())
			{
				return new GroupMailboxOwnerAdapter();
			}
			RecipientTypeDetails recipientTypeDetails = recipient.RecipientTypeDetails;
			return new MiniRecipientMailboxOwnerAdapter(recipient, recipient.GetContext(null), recipientTypeDetails, this.session.LogonType);
		}

		// Token: 0x04005A3B RID: 23099
		private readonly IMailboxSession session;
	}
}
