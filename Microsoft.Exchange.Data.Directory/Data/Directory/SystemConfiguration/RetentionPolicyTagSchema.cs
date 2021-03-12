using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000429 RID: 1065
	internal sealed class RetentionPolicyTagSchema : ADConfigurationObjectSchema
	{
		// Token: 0x06002FE1 RID: 12257 RVA: 0x000C1613 File Offset: 0x000BF813
		internal static QueryFilter IsDefaultAutoGroupPolicyTagFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(RetentionPolicyTagSchema.PolicyTagFlags, 8UL));
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000C1627 File Offset: 0x000BF827
		internal static QueryFilter IsDefaultModeratedRecipientsPolicyTagFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(RetentionPolicyTagSchema.PolicyTagFlags, 16UL));
		}

		// Token: 0x04002053 RID: 8275
		public static readonly ADPropertyDefinition LocalizedRetentionPolicyTagName = new ADPropertyDefinition("LocalizedRetentionPolicyTagName", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchELCFolderNameLocalized", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002054 RID: 8276
		public static readonly ADPropertyDefinition Comment = SharedPropertyDefinitions.Comment;

		// Token: 0x04002055 RID: 8277
		public static readonly ADPropertyDefinition LocalizedComment = SharedPropertyDefinitions.LocalizedComment;

		// Token: 0x04002056 RID: 8278
		public static readonly ADPropertyDefinition PolicyTagFlags = SharedPropertyDefinitions.ElcFlags;

		// Token: 0x04002057 RID: 8279
		public static readonly ADPropertyDefinition PolicyIds = new ADPropertyDefinition("PolicyIds", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchELCFolderBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002058 RID: 8280
		public static readonly ADPropertyDefinition Type = new ADPropertyDefinition("Type", ExchangeObjectVersion.Exchange2007, typeof(ElcFolderType), "msExchELCFolderType", ADPropertyDefinitionFlags.PersistDefaultValue, ElcFolderType.Personal, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ElcFolderType))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002059 RID: 8281
		public static readonly ADPropertyDefinition RetentionId = new ADPropertyDefinition("RetentionId", ExchangeObjectVersion.Exchange2010, typeof(Guid), "msExchAuthoritativePolicyTagGUID", ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400205A RID: 8282
		public static readonly ADPropertyDefinition LegacyManagedFolder = new ADPropertyDefinition("LegacyManagedFolder", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchPolicyTagLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400205B RID: 8283
		public static readonly ADPropertyDefinition IsDefaultAutoGroupPolicyTag = new ADPropertyDefinition("IsDefaultAutoGroupPolicyTag", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RetentionPolicyTagSchema.PolicyTagFlags
		}, new CustomFilterBuilderDelegate(RetentionPolicyTagSchema.IsDefaultAutoGroupPolicyTagFilterBuilder), ADObject.FlagGetterDelegate(RetentionPolicyTagSchema.PolicyTagFlags, 8), ADObject.FlagSetterDelegate(RetentionPolicyTagSchema.PolicyTagFlags, 8), null, null);

		// Token: 0x0400205C RID: 8284
		public static readonly ADPropertyDefinition IsDefaultModeratedRecipientsPolicyTag = new ADPropertyDefinition("IsDefaultModeratedRecipientsPolicyTag", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RetentionPolicyTagSchema.PolicyTagFlags
		}, new CustomFilterBuilderDelegate(RetentionPolicyTagSchema.IsDefaultModeratedRecipientsPolicyTagFilterBuilder), ADObject.FlagGetterDelegate(RetentionPolicyTagSchema.PolicyTagFlags, 16), ADObject.FlagSetterDelegate(RetentionPolicyTagSchema.PolicyTagFlags, 16), null, null);
	}
}
