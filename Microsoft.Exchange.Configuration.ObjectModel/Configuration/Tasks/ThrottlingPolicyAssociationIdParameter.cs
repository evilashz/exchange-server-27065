using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200019A RID: 410
	[Serializable]
	public class ThrottlingPolicyAssociationIdParameter : RecipientIdParameter
	{
		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002B360 File Offset: 0x00029560
		public ThrottlingPolicyAssociationIdParameter()
		{
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0002B368 File Offset: 0x00029568
		public ThrottlingPolicyAssociationIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0002B371 File Offset: 0x00029571
		public ThrottlingPolicyAssociationIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0002B37A File Offset: 0x0002957A
		public ThrottlingPolicyAssociationIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002B383 File Offset: 0x00029583
		public new static ThrottlingPolicyAssociationIdParameter Parse(string identity)
		{
			return new ThrottlingPolicyAssociationIdParameter(identity);
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0002B38B File Offset: 0x0002958B
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return ThrottlingPolicyAssociationIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x0400031A RID: 794
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.User,
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.MailContact,
			RecipientType.Computer
		};
	}
}
