using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000770 RID: 1904
	internal class TeamMailboxSchema : ADPresentationSchema
	{
		// Token: 0x06005D6B RID: 23915 RVA: 0x0014242F File Offset: 0x0014062F
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x04003F1E RID: 16158
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x04003F1F RID: 16159
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x04003F20 RID: 16160
		public static readonly ADPropertyDefinition TeamMailboxClosedTime = ADUserSchema.TeamMailboxClosedTime;

		// Token: 0x04003F21 RID: 16161
		public static readonly ADPropertyDefinition SharePointLinkedBy = ADUserSchema.SharePointLinkedBy;

		// Token: 0x04003F22 RID: 16162
		public static readonly ADPropertyDefinition SharePointUrl = ADMailboxRecipientSchema.SharePointUrl;

		// Token: 0x04003F23 RID: 16163
		public static readonly ADPropertyDefinition SharePointSiteInfo = ADUserSchema.SharePointSiteInfo;

		// Token: 0x04003F24 RID: 16164
		public static readonly ADPropertyDefinition SiteMailboxWebCollectionUrl = ADUserSchema.SiteMailboxWebCollectionUrl;

		// Token: 0x04003F25 RID: 16165
		public static readonly ADPropertyDefinition SiteMailboxWebId = ADUserSchema.SiteMailboxWebId;

		// Token: 0x04003F26 RID: 16166
		public static readonly ADPropertyDefinition Owners = ADUserSchema.Owners;

		// Token: 0x04003F27 RID: 16167
		public static readonly ADPropertyDefinition DelegateListLink = ADMailboxRecipientSchema.DelegateListLink;

		// Token: 0x04003F28 RID: 16168
		public static readonly ADPropertyDefinition TeamMailboxMembers = ADUserSchema.TeamMailboxMembers;

		// Token: 0x04003F29 RID: 16169
		public static readonly ADPropertyDefinition TeamMailboxShowInMyClient = ADUserSchema.TeamMailboxShowInMyClient;

		// Token: 0x04003F2A RID: 16170
		public static readonly ADPropertyDefinition SiteMailboxMessageDedupEnabled = ADUserSchema.SiteMailboxMessageDedupEnabled;

		// Token: 0x04003F2B RID: 16171
		public static readonly ADPropertyDefinition TeamMailboxUserMembership = ADUserSchema.TeamMailboxUserMembership;

		// Token: 0x04003F2C RID: 16172
		public static readonly ADPropertyDefinition RecipientTypeDetails = ADRecipientSchema.RecipientTypeDetails;

		// Token: 0x04003F2D RID: 16173
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;
	}
}
