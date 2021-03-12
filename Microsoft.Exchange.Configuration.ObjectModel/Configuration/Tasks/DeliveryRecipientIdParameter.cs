using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200017A RID: 378
	[Serializable]
	public class DeliveryRecipientIdParameter : RecipientIdParameter
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00028D07 File Offset: 0x00026F07
		public DeliveryRecipientIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00028D10 File Offset: 0x00026F10
		public DeliveryRecipientIdParameter()
		{
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00028D18 File Offset: 0x00026F18
		public DeliveryRecipientIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00028D21 File Offset: 0x00026F21
		public DeliveryRecipientIdParameter(ADPresentationObject recipient) : base(recipient)
		{
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00028D2A File Offset: 0x00026F2A
		public DeliveryRecipientIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00028D33 File Offset: 0x00026F33
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return DeliveryRecipientIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00028D3A File Offset: 0x00026F3A
		public new static DeliveryRecipientIdParameter Parse(string identity)
		{
			return new DeliveryRecipientIdParameter(identity);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00028D42 File Offset: 0x00026F42
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailboxOrMailUser(id);
		}

		// Token: 0x040002FB RID: 763
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.DynamicDistributionGroup,
			RecipientType.UserMailbox,
			RecipientType.MailContact,
			RecipientType.MailUniversalDistributionGroup,
			RecipientType.MailUniversalSecurityGroup,
			RecipientType.MailNonUniversalGroup,
			RecipientType.MailUser,
			RecipientType.PublicFolder,
			RecipientType.MicrosoftExchange,
			RecipientType.User
		};
	}
}
