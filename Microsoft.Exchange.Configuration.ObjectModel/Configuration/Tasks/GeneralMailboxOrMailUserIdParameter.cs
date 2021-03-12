using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200016B RID: 363
	[Serializable]
	public class GeneralMailboxOrMailUserIdParameter : RecipientIdParameter
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x00028283 File Offset: 0x00026483
		public GeneralMailboxOrMailUserIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002828C File Offset: 0x0002648C
		public GeneralMailboxOrMailUserIdParameter()
		{
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00028294 File Offset: 0x00026494
		public GeneralMailboxOrMailUserIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002829D File Offset: 0x0002649D
		public GeneralMailboxOrMailUserIdParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000282AB File Offset: 0x000264AB
		public GeneralMailboxOrMailUserIdParameter(User user) : base(user.Id)
		{
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000282B9 File Offset: 0x000264B9
		public GeneralMailboxOrMailUserIdParameter(Mailbox user) : base(user.Id)
		{
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000282C7 File Offset: 0x000264C7
		public GeneralMailboxOrMailUserIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x000282D0 File Offset: 0x000264D0
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return GeneralMailboxOrMailUserIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000282D7 File Offset: 0x000264D7
		public new static GeneralMailboxOrMailUserIdParameter Parse(string identity)
		{
			return new GeneralMailboxOrMailUserIdParameter(identity);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000282DF File Offset: 0x000264DF
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailboxOrMailUser(id);
		}

		// Token: 0x040002EA RID: 746
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox,
			RecipientType.SystemAttendantMailbox,
			RecipientType.SystemMailbox,
			RecipientType.MailUser
		};
	}
}
