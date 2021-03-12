using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000171 RID: 369
	[Serializable]
	public class MailboxUserContactIdParameter : RecipientIdParameter
	{
		// Token: 0x06000D46 RID: 3398 RVA: 0x000285F2 File Offset: 0x000267F2
		public MailboxUserContactIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000285FB File Offset: 0x000267FB
		public MailboxUserContactIdParameter()
		{
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00028603 File Offset: 0x00026803
		public MailboxUserContactIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002860C File Offset: 0x0002680C
		public MailboxUserContactIdParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002861A File Offset: 0x0002681A
		public MailboxUserContactIdParameter(Mailbox user) : base(user.Id)
		{
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00028628 File Offset: 0x00026828
		public MailboxUserContactIdParameter(MailContact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00028636 File Offset: 0x00026836
		public MailboxUserContactIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0002863F File Offset: 0x0002683F
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailboxUserContactIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00028646 File Offset: 0x00026846
		public new static MailboxUserContactIdParameter Parse(string identity)
		{
			return new MailboxUserContactIdParameter(identity);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002864E File Offset: 0x0002684E
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailboxUserContact(id);
		}

		// Token: 0x040002F1 RID: 753
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.MailContact
		};
	}
}
