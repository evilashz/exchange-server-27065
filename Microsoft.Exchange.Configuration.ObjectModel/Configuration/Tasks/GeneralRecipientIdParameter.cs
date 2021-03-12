using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200016C RID: 364
	[Serializable]
	public class GeneralRecipientIdParameter : RecipientIdParameter
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x00028314 File Offset: 0x00026514
		public GeneralRecipientIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002831D File Offset: 0x0002651D
		public GeneralRecipientIdParameter()
		{
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00028325 File Offset: 0x00026525
		public GeneralRecipientIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002832E File Offset: 0x0002652E
		public GeneralRecipientIdParameter(ADPresentationObject recipient) : base(recipient)
		{
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00028337 File Offset: 0x00026537
		public GeneralRecipientIdParameter(ReducedRecipient recipient) : base(recipient.Id)
		{
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00028345 File Offset: 0x00026545
		public GeneralRecipientIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002834E File Offset: 0x0002654E
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return GeneralRecipientIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00028355 File Offset: 0x00026555
		public new static GeneralRecipientIdParameter Parse(string identity)
		{
			return new GeneralRecipientIdParameter(identity);
		}

		// Token: 0x040002EB RID: 747
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
			RecipientType.MailNonUniversalGroup,
			RecipientType.DynamicDistributionGroup,
			RecipientType.PublicFolder,
			RecipientType.Computer
		};
	}
}
