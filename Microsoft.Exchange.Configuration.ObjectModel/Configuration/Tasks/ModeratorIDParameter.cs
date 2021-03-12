using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000177 RID: 375
	[Serializable]
	public class ModeratorIDParameter : RecipientIdParameter
	{
		// Token: 0x06000D84 RID: 3460 RVA: 0x00028B48 File Offset: 0x00026D48
		public ModeratorIDParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00028B51 File Offset: 0x00026D51
		public ModeratorIDParameter()
		{
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00028B59 File Offset: 0x00026D59
		public ModeratorIDParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00028B62 File Offset: 0x00026D62
		public ModeratorIDParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00028B70 File Offset: 0x00026D70
		public ModeratorIDParameter(MailContact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00028B7E File Offset: 0x00026D7E
		public ModeratorIDParameter(Mailbox user) : base(user.Id)
		{
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00028B8C File Offset: 0x00026D8C
		public ModeratorIDParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00028B95 File Offset: 0x00026D95
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return ModeratorIDParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00028B9C File Offset: 0x00026D9C
		public new static ModeratorIDParameter Parse(string identity)
		{
			return new ModeratorIDParameter(identity);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00028BA4 File Offset: 0x00026DA4
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailboxOrMailUser(id);
		}

		// Token: 0x040002F8 RID: 760
		internal new static readonly RecipientType[] AllowedRecipientTypes = ADRecipient.AllowedModeratorsRecipientTypes;
	}
}
