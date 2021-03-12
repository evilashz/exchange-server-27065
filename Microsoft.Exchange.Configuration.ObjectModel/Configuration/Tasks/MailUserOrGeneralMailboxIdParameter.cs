using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	public class MailUserOrGeneralMailboxIdParameter : GeneralMailboxIdParameter
	{
		// Token: 0x06000D79 RID: 3449 RVA: 0x00028AB7 File Offset: 0x00026CB7
		public MailUserOrGeneralMailboxIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00028AC0 File Offset: 0x00026CC0
		public MailUserOrGeneralMailboxIdParameter()
		{
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00028AC8 File Offset: 0x00026CC8
		public MailUserOrGeneralMailboxIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00028AD1 File Offset: 0x00026CD1
		public MailUserOrGeneralMailboxIdParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00028ADF File Offset: 0x00026CDF
		public MailUserOrGeneralMailboxIdParameter(User user) : base(user.Id)
		{
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00028AED File Offset: 0x00026CED
		public MailUserOrGeneralMailboxIdParameter(Mailbox user) : base(user.Id)
		{
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00028AFB File Offset: 0x00026CFB
		public MailUserOrGeneralMailboxIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00028B04 File Offset: 0x00026D04
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailUserOrGeneralMailboxIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00028B0B File Offset: 0x00026D0B
		public new static MailUserOrGeneralMailboxIdParameter Parse(string identity)
		{
			return new MailUserOrGeneralMailboxIdParameter(identity);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00028B13 File Offset: 0x00026D13
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailboxOrMailUser(id);
		}

		// Token: 0x040002F7 RID: 759
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox,
			RecipientType.SystemAttendantMailbox,
			RecipientType.SystemMailbox,
			RecipientType.MailUser
		};
	}
}
