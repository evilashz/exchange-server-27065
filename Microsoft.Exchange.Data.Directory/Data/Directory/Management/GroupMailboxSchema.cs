using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000717 RID: 1815
	internal class GroupMailboxSchema : ADPresentationSchema
	{
		// Token: 0x060055AD RID: 21933 RVA: 0x00135861 File Offset: 0x00133A61
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x040039C9 RID: 14793
		public static readonly ADPropertyDefinition Alias = ADRecipientSchema.Alias;

		// Token: 0x040039CA RID: 14794
		public static readonly ADPropertyDefinition Database = ADMailboxRecipientSchema.Database;

		// Token: 0x040039CB RID: 14795
		public static readonly ADPropertyDefinition DelegateListLink = ADMailboxRecipientSchema.DelegateListLink;

		// Token: 0x040039CC RID: 14796
		public static readonly ADPropertyDefinition Description = ADRecipientSchema.Description;

		// Token: 0x040039CD RID: 14797
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x040039CE RID: 14798
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x040039CF RID: 14799
		public static readonly ADPropertyDefinition ExchangeGuid = ADMailboxRecipientSchema.ExchangeGuid;

		// Token: 0x040039D0 RID: 14800
		public static readonly ADPropertyDefinition ExternalDirectoryObjectId = ADRecipientSchema.ExternalDirectoryObjectId;

		// Token: 0x040039D1 RID: 14801
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADRecipientSchema.LegacyExchangeDN;

		// Token: 0x040039D2 RID: 14802
		public static readonly ADPropertyDefinition ModernGroupType = ADRecipientSchema.ModernGroupType;

		// Token: 0x040039D3 RID: 14803
		public static readonly ADPropertyDefinition PublicToGroupSids = ADMailboxRecipientSchema.PublicToGroupSids;

		// Token: 0x040039D4 RID: 14804
		public static readonly ADPropertyDefinition Owners = ADUserSchema.Owners;

		// Token: 0x040039D5 RID: 14805
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x040039D6 RID: 14806
		public static readonly ADPropertyDefinition RecipientTypeDetails = ADRecipientSchema.RecipientTypeDetails;

		// Token: 0x040039D7 RID: 14807
		public static readonly ADPropertyDefinition RequireSenderAuthenticationEnabled = ADRecipientSchema.RequireAllSendersAreAuthenticated;

		// Token: 0x040039D8 RID: 14808
		public static readonly ADPropertyDefinition ServerName = ADMailboxRecipientSchema.ServerName;

		// Token: 0x040039D9 RID: 14809
		public static readonly ADPropertyDefinition SharePointUrl = ADMailboxRecipientSchema.SharePointUrl;

		// Token: 0x040039DA RID: 14810
		public static readonly ADPropertyDefinition SharePointSiteUrl = ADMailboxRecipientSchema.GroupMailboxSharePointSiteUrl;

		// Token: 0x040039DB RID: 14811
		public static readonly ADPropertyDefinition SharePointDocumentsUrl = ADMailboxRecipientSchema.GroupMailboxSharePointDocumentsUrl;

		// Token: 0x040039DC RID: 14812
		public static readonly ADPropertyDefinition IsMailboxConfigured = ADRecipientSchema.IsGroupMailboxConfigured;

		// Token: 0x040039DD RID: 14813
		public static readonly ADPropertyDefinition IsExternalResourcesPublished = ADRecipientSchema.GroupMailboxExternalResourcesSet;

		// Token: 0x040039DE RID: 14814
		public static readonly ADPropertyDefinition YammerGroupEmailAddress = ADMailboxRecipientSchema.YammerGroupAddress;

		// Token: 0x040039DF RID: 14815
		public static readonly ADPropertyDefinition AutoSubscribeNewGroupMembers = ADRecipientSchema.AutoSubscribeNewGroupMembers;

		// Token: 0x040039E0 RID: 14816
		public static readonly ADPropertyDefinition Languages = ADUserSchema.Languages;
	}
}
