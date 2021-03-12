using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	public class MailboxIdParameter : RecipientIdParameter
	{
		// Token: 0x06000A4F RID: 2639 RVA: 0x000228CD File Offset: 0x00020ACD
		public MailboxIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000228D6 File Offset: 0x00020AD6
		public MailboxIdParameter()
		{
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000228DE File Offset: 0x00020ADE
		public MailboxIdParameter(MailboxEntry storeMailboxEntry) : this((storeMailboxEntry == null) ? null : storeMailboxEntry.Identity)
		{
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000228F4 File Offset: 0x00020AF4
		public MailboxIdParameter(MailboxId storeMailboxId) : this((null == storeMailboxId) ? null : ((Guid.Empty == storeMailboxId.MailboxGuid) ? storeMailboxId.MailboxExchangeLegacyDn : storeMailboxId.MailboxGuid.ToString()))
		{
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00022941 File Offset: 0x00020B41
		public MailboxIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002294A File Offset: 0x00020B4A
		public MailboxIdParameter(Mailbox mailbox) : base(mailbox)
		{
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00022953 File Offset: 0x00020B53
		public MailboxIdParameter(ConsumerMailbox mailbox) : base(mailbox)
		{
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002295C File Offset: 0x00020B5C
		public MailboxIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00022965 File Offset: 0x00020B65
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailboxIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002296C File Offset: 0x00020B6C
		public new static MailboxIdParameter Parse(string identity)
		{
			return new MailboxIdParameter(identity);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00022974 File Offset: 0x00020B74
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailboxUser(id);
		}

		// Token: 0x04000282 RID: 642
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox
		};
	}
}
