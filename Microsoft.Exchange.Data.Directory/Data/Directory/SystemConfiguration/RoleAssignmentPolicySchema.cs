using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000574 RID: 1396
	internal class RoleAssignmentPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002A42 RID: 10818
		internal static readonly ExchangeObjectVersion Exchange2009_R4 = new ExchangeObjectVersion(0, 11, 14, 0, 509, 0);

		// Token: 0x04002A43 RID: 10819
		public static readonly ADPropertyDefinition AssociatedUsers = new ADPropertyDefinition("AssociatedUsers", RoleAssignmentPolicySchema.Exchange2009_R4, typeof(ADObjectId), "msExchRBACPolicyBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A44 RID: 10820
		public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("Flags", RoleAssignmentPolicySchema.Exchange2009_R4, typeof(int), "msExchRBACPolicyFlags", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.DoNotProvisionalClone, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A45 RID: 10821
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", RoleAssignmentPolicySchema.Exchange2009_R4, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RoleAssignmentPolicySchema.Flags
		}, new CustomFilterBuilderDelegate(RoleAssignmentPolicy.IsDefaultFilterBuilder), ADObject.FlagGetterDelegate(RoleAssignmentPolicySchema.Flags, 1), ADObject.FlagSetterDelegate(RoleAssignmentPolicySchema.Flags, 1), null, null);

		// Token: 0x04002A46 RID: 10822
		public static readonly ADPropertyDefinition RawDescription = SharedPropertyDefinitions.RawDescription;

		// Token: 0x04002A47 RID: 10823
		public static readonly ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, new ProviderPropertyDefinition[]
		{
			RoleAssignmentPolicySchema.RawDescription
		}, null, new GetterDelegate(RoleAssignmentPolicy.DescriptionGetter), new SetterDelegate(RoleAssignmentPolicy.DescriptionSetter), null, null);

		// Token: 0x04002A48 RID: 10824
		public static readonly ADPropertyDefinition RoleAssignments = new ADPropertyDefinition("RoleAssignments", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated | ADPropertyDefinitionFlags.ValidateInSharedConfig, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A49 RID: 10825
		public static readonly ADPropertyDefinition AssignedRoles = new ADPropertyDefinition("AssignedRoles", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated | ADPropertyDefinitionFlags.ValidateInSharedConfig, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
