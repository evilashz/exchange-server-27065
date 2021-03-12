using System;
using System.Linq;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000257 RID: 599
	internal class OWAMiniRecipientSchema : StorageMiniRecipientSchema
	{
		// Token: 0x04000DD6 RID: 3542
		public static readonly ADPropertyDefinition PhoneticDisplayName = ADRecipientSchema.PhoneticDisplayName;

		// Token: 0x04000DD7 RID: 3543
		public static readonly ADPropertyDefinition WebPage = ADRecipientSchema.WebPage;

		// Token: 0x04000DD8 RID: 3544
		public static readonly ADPropertyDefinition ActiveSyncEnabled = ADUserSchema.ActiveSyncEnabled;

		// Token: 0x04000DD9 RID: 3545
		public static readonly ADPropertyDefinition ExternalOofOptions = ADMailboxRecipientSchema.ExternalOofOptions;

		// Token: 0x04000DDA RID: 3546
		public static readonly ADPropertyDefinition RecipientDisplayType = ADRecipientSchema.RecipientDisplayType;

		// Token: 0x04000DDB RID: 3547
		public static readonly ADPropertyDefinition MobilePhoneNumber = ADUserSchema.MobilePhone;

		// Token: 0x04000DDC RID: 3548
		internal static readonly ADPropertyDefinition[] AdditionalProperties = new ADPropertyDefinition[]
		{
			OWAMiniRecipientSchema.PhoneticDisplayName,
			OWAMiniRecipientSchema.WebPage,
			OWAMiniRecipientSchema.ActiveSyncEnabled,
			MiniRecipientSchema.EmailAddresses,
			OWAMiniRecipientSchema.ExternalOofOptions,
			OWAMiniRecipientSchema.RecipientDisplayType,
			StorageMiniRecipientSchema.Alias,
			OWAMiniRecipientSchema.MobilePhoneNumber,
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.ImmutableId,
			ADRecipientSchema.RawOnPremisesObjectId
		};

		// Token: 0x04000DDD RID: 3549
		internal static readonly PropertyDefinition[] AdditionalPropertiesWithClientAccessRules = ObjectSchema.GetInstance<ClientAccessRulesRecipientFilterSchema>().AllProperties.Union(OWAMiniRecipientSchema.AdditionalProperties).ToArray<PropertyDefinition>();
	}
}
