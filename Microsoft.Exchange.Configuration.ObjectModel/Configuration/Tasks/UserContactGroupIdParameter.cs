using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200016F RID: 367
	[Serializable]
	public class UserContactGroupIdParameter : RecipientIdParameter
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x000284F2 File Offset: 0x000266F2
		public UserContactGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000284FB File Offset: 0x000266FB
		public UserContactGroupIdParameter()
		{
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00028503 File Offset: 0x00026703
		public UserContactGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002850C File Offset: 0x0002670C
		public UserContactGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00028515 File Offset: 0x00026715
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return UserContactGroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002851C File Offset: 0x0002671C
		public new static UserContactGroupIdParameter Parse(string identity)
		{
			return new UserContactGroupIdParameter(identity);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00028524 File Offset: 0x00026724
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeUserContactGroupIdParameter(id);
		}

		// Token: 0x040002EF RID: 751
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.User,
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.Contact,
			RecipientType.MailContact,
			RecipientType.Group,
			RecipientType.MailUniversalDistributionGroup,
			RecipientType.MailUniversalSecurityGroup,
			RecipientType.MailNonUniversalGroup
		};
	}
}
