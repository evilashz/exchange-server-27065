using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000279 RID: 633
	internal class ADDynamicGroupSchema : ADRecipientSchema
	{
		// Token: 0x0400116D RID: 4461
		public static readonly ADPropertyDefinition ExpansionServer = IADDistributionListSchema.ExpansionServer;

		// Token: 0x0400116E RID: 4462
		public static readonly ADPropertyDefinition PurportedSearchUI = SharedPropertyDefinitions.PurportedSearchUI;

		// Token: 0x0400116F RID: 4463
		public static readonly ADPropertyDefinition RecipientFilterMetadata = SharedPropertyDefinitions.RecipientFilterMetadata;

		// Token: 0x04001170 RID: 4464
		public static readonly ADPropertyDefinition RecipientContainer = new ADPropertyDefinition("RecipientContainer", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchDynamicDLBaseDN", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001171 RID: 4465
		public static readonly ADPropertyDefinition LdapRecipientFilter = new ADPropertyDefinition("LdapRecipientFilter", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchDynamicDLFilter", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001172 RID: 4466
		public static readonly ADPropertyDefinition RecipientFilter = SharedPropertyDefinitions.RecipientFilter;

		// Token: 0x04001173 RID: 4467
		public static readonly ADPropertyDefinition ManagedBy = IADDistributionListSchema.RawManagedBy;

		// Token: 0x04001174 RID: 4468
		public static readonly ADPropertyDefinition Members = IADDistributionListSchema.Members;

		// Token: 0x04001175 RID: 4469
		public static readonly ADPropertyDefinition ReportToManagerEnabled = IADDistributionListSchema.ReportToManagerEnabled;

		// Token: 0x04001176 RID: 4470
		public static readonly ADPropertyDefinition ReportToOriginatorEnabled = IADDistributionListSchema.ReportToOriginatorEnabled;

		// Token: 0x04001177 RID: 4471
		public static readonly ADPropertyDefinition SendDeliveryReportsTo = IADDistributionListSchema.SendDeliveryReportsTo;

		// Token: 0x04001178 RID: 4472
		public static readonly ADPropertyDefinition SendOofMessageToOriginatorEnabled = IADDistributionListSchema.SendOofMessageToOriginatorEnabled;

		// Token: 0x04001179 RID: 4473
		public static readonly ADPropertyDefinition IncludedRecipients = new ADPropertyDefinition("IncludedRecipients", ExchangeObjectVersion.Exchange2007, typeof(WellKnownRecipientType?), null, ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new NullableWellKnownRecipientTypeConstraint()
		}, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, new CustomFilterBuilderDelegate(ADDynamicGroup.IncludeRecipientFilterBuilder), (IPropertyBag propertyBag) => RecipientFilterHelper.IncludeRecipientGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.IncludeRecipientSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400117A RID: 4474
		public static readonly ADPropertyDefinition ConditionalDepartment = new ADPropertyDefinition("ConditionalDepartment", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 64)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.DepartmentGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalDepartment), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.DepartmentSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400117B RID: 4475
		public static readonly ADPropertyDefinition ConditionalCompany = new ADPropertyDefinition("ConditionalCompany", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CompanyGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCompany), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CompanySetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400117C RID: 4476
		public static readonly ADPropertyDefinition ConditionalStateOrProvince = new ADPropertyDefinition("ConditionalStateOrProvince", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 128)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.StateOrProvinceGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalStateOrProvince), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.StateOrProvinceSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400117D RID: 4477
		public static readonly ADPropertyDefinition RecipientFilterType = new ADPropertyDefinition("RecipientFilterType", ExchangeObjectVersion.Exchange2007, typeof(WellKnownRecipientFilterType), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, WellKnownRecipientFilterType.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.RecipientFilterTypeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter), null, null, null);

		// Token: 0x0400117E RID: 4478
		public static readonly ADPropertyDefinition ConditionalCustomAttribute1 = new ADPropertyDefinition("ConditionalCustomAttribute1", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute1), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute1, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400117F RID: 4479
		public static readonly ADPropertyDefinition ConditionalCustomAttribute2 = new ADPropertyDefinition("ConditionalCustomAttribute2", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute2), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute2, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001180 RID: 4480
		public static readonly ADPropertyDefinition ConditionalCustomAttribute3 = new ADPropertyDefinition("ConditionalCustomAttribute3", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute3), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute3, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001181 RID: 4481
		public static readonly ADPropertyDefinition ConditionalCustomAttribute4 = new ADPropertyDefinition("ConditionalCustomAttribute4", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute4), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute4, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001182 RID: 4482
		public static readonly ADPropertyDefinition ConditionalCustomAttribute5 = new ADPropertyDefinition("ConditionalCustomAttribute5", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute5), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute5, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001183 RID: 4483
		public static readonly ADPropertyDefinition ConditionalCustomAttribute6 = new ADPropertyDefinition("ConditionalCustomAttribute6", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute6), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute6, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001184 RID: 4484
		public static readonly ADPropertyDefinition ConditionalCustomAttribute7 = new ADPropertyDefinition("ConditionalCustomAttribute7", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute7), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute7, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001185 RID: 4485
		public static readonly ADPropertyDefinition ConditionalCustomAttribute8 = new ADPropertyDefinition("ConditionalCustomAttribute8", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute8), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute8, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001186 RID: 4486
		public static readonly ADPropertyDefinition ConditionalCustomAttribute9 = new ADPropertyDefinition("ConditionalCustomAttribute9", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute9), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute9, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001187 RID: 4487
		public static readonly ADPropertyDefinition ConditionalCustomAttribute10 = new ADPropertyDefinition("ConditionalCustomAttribute10", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute10), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute10, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001188 RID: 4488
		public static readonly ADPropertyDefinition ConditionalCustomAttribute11 = new ADPropertyDefinition("ConditionalCustomAttribute11", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute11), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute11, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x04001189 RID: 4489
		public static readonly ADPropertyDefinition ConditionalCustomAttribute12 = new ADPropertyDefinition("ConditionalCustomAttribute12", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute12), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute12, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400118A RID: 4490
		public static readonly ADPropertyDefinition ConditionalCustomAttribute13 = new ADPropertyDefinition("ConditionalCustomAttribute13", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute13), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute13, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400118B RID: 4491
		public static readonly ADPropertyDefinition ConditionalCustomAttribute14 = new ADPropertyDefinition("ConditionalCustomAttribute14", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute14), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute14, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400118C RID: 4492
		public static readonly ADPropertyDefinition ConditionalCustomAttribute15 = new ADPropertyDefinition("ConditionalCustomAttribute15", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			ADDynamicGroupSchema.RecipientFilterMetadata,
			ADDynamicGroupSchema.RecipientFilter,
			ADDynamicGroupSchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute15), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.ConditionalCustomAttribute15, ADDynamicGroupSchema.LdapRecipientFilter, true);
		}, null, null);

		// Token: 0x0400118D RID: 4493
		public static readonly ADPropertyDefinition FilterOnlyManagedBy = ADGroupSchema.ManagedBy;

		// Token: 0x0400118E RID: 4494
		public static readonly ADPropertyDefinition GroupMemberCount = new ADPropertyDefinition("GroupMemberCount", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchGroupMemberCount", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400118F RID: 4495
		public static readonly ADPropertyDefinition GroupExternalMemberCount = new ADPropertyDefinition("GroupExternalMemberCount", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchGroupExternalMemberCount", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
