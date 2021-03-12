using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200041D RID: 1053
	internal sealed class ELCFolderSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001FFF RID: 8191
		public static readonly ADPropertyDefinition Comment = SharedPropertyDefinitions.Comment;

		// Token: 0x04002000 RID: 8192
		public static readonly ADPropertyDefinition LocalizedComment = SharedPropertyDefinitions.LocalizedComment;

		// Token: 0x04002001 RID: 8193
		public static readonly ADPropertyDefinition FolderType = new ADPropertyDefinition("FolderType", ExchangeObjectVersion.Exchange2007, typeof(ElcFolderType), "msExchELCFolderType", ADPropertyDefinitionFlags.PersistDefaultValue, ElcFolderType.ManagedCustomFolder, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ElcFolderType))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002002 RID: 8194
		public static readonly ADPropertyDefinition FolderName = new ADPropertyDefinition("FolderName", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchELCFolderName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint()
		}, null, null);

		// Token: 0x04002003 RID: 8195
		public static readonly ADPropertyDefinition LocalizedFolderName = new ADPropertyDefinition("LocalizedFolderName", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchELCFolderNameLocalized", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002004 RID: 8196
		public static readonly ADPropertyDefinition ElcFlags = SharedPropertyDefinitions.ElcFlags;

		// Token: 0x04002005 RID: 8197
		public static readonly ADPropertyDefinition StorageQuota = new ADPropertyDefinition("StorageQuota", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchELCFolderQuota", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(1UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002006 RID: 8198
		public static readonly ADPropertyDefinition TemplateIds = new ADPropertyDefinition("TemplateIds", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchELCFolderBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002007 RID: 8199
		public static readonly ADPropertyDefinition MustDisplayComment = new ADPropertyDefinition("MustDisplayComment", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ELCFolderSchema.ElcFlags
		}, null, (IPropertyBag propertyBag) => 0 != ((int)propertyBag[ELCFolderSchema.ElcFlags] & 1), delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ELCFolderSchema.ElcFlags] = ((int)propertyBag[ELCFolderSchema.ElcFlags] | 1);
				return;
			}
			propertyBag[ELCFolderSchema.ElcFlags] = ((int)propertyBag[ELCFolderSchema.ElcFlags] & -2);
		}, null, null);

		// Token: 0x04002008 RID: 8200
		public static readonly ADPropertyDefinition BaseFolderOnly = new ADPropertyDefinition("BaseFolderOnly", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ELCFolderSchema.ElcFlags
		}, null, (IPropertyBag propertyBag) => 0 != ((int)propertyBag[ELCFolderSchema.ElcFlags] & 2), delegate(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ELCFolderSchema.ElcFlags] = ((int)propertyBag[ELCFolderSchema.ElcFlags] | 2);
				return;
			}
			propertyBag[ELCFolderSchema.ElcFlags] = ((int)propertyBag[ELCFolderSchema.ElcFlags] & -3);
		}, null, null);

		// Token: 0x04002009 RID: 8201
		public static readonly ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2007, typeof(ElcFolderCategory), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, ElcFolderCategory.ManagedCustomFolder, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ELCFolderSchema.FolderType
		}, null, (IPropertyBag propertyBag) => (ElcFolderType.ManagedCustomFolder == (ElcFolderType)propertyBag[ELCFolderSchema.FolderType]) ? ElcFolderCategory.ManagedCustomFolder : ElcFolderCategory.ManagedDefaultFolder, null, null, null);

		// Token: 0x0400200A RID: 8202
		public static readonly ADPropertyDefinition RetentionPolicyTag = new ADPropertyDefinition("RetentionPolicyTag", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchPolicyTagLinkBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
