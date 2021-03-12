using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200043A RID: 1082
	internal class EmailAddressPolicySchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x040020AD RID: 8365
		public static readonly ADPropertyDefinition RecipientFilter = new ADPropertyDefinition("RecipientFilter", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchQueryFilter", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 2048)
		}, null, null);

		// Token: 0x040020AE RID: 8366
		public static readonly ADPropertyDefinition LdapRecipientFilter = SharedPropertyDefinitions.LdapRecipientFilter;

		// Token: 0x040020AF RID: 8367
		public static readonly ADPropertyDefinition PurportedSearchUI = SharedPropertyDefinitions.PurportedSearchUI;

		// Token: 0x040020B0 RID: 8368
		public static readonly ADPropertyDefinition RecipientFilterMetadata = SharedPropertyDefinitions.RecipientFilterMetadata;

		// Token: 0x040020B1 RID: 8369
		public static readonly ADPropertyDefinition RawEnabledEmailAddressTemplates = new ADPropertyDefinition("EnabledEmailAddressTemplates", ExchangeObjectVersion.Exchange2003, typeof(ProxyAddressTemplate), "gatewayProxy", ADPropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1123)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020B2 RID: 8370
		public static readonly ADPropertyDefinition DisabledEmailAddressTemplates = new ADPropertyDefinition("DisabledEmailAddressTemplates", ExchangeObjectVersion.Exchange2003, typeof(ProxyAddressTemplate), "disabledGatewayProxy", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020B3 RID: 8371
		public static readonly ADPropertyDefinition NonAuthoritativeDomains = new ADPropertyDefinition("NonAuthoritativeDomains", ExchangeObjectVersion.Exchange2003, typeof(ProxyAddressTemplate), "msExchNonAuthoritativeDomains", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020B4 RID: 8372
		public static readonly ADPropertyDefinition Priority = new ADPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2003, typeof(EmailAddressPolicyPriority), "msExchPolicyOrder", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, (EmailAddressPolicyPriority)0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EmailAddressPolicyPriority>((EmailAddressPolicyPriority)EmailAddressPolicyPriority.LenientHighestPriorityValue, EmailAddressPolicyPriority.Lowest)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020B5 RID: 8373
		public static readonly ADPropertyDefinition PolicyOptionListValue = new ADPropertyDefinition("PolicyOptionListValue", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchPolicyOptionList", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020B6 RID: 8374
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchPolicyEnabled", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020B7 RID: 8375
		public static readonly ADPropertyDefinition AdminDescription = new ADPropertyDefinition("AdminDescription", ExchangeObjectVersion.Exchange2003, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020B8 RID: 8376
		public static readonly ADPropertyDefinition LastUpdatedRecipientFilter = SharedPropertyDefinitions.LastUpdatedRecipientFilter;

		// Token: 0x040020B9 RID: 8377
		public static readonly ADPropertyDefinition RecipientFilterFlags = new ADPropertyDefinition("RecipientFilterFlags", ExchangeObjectVersion.Exchange2007, typeof(RecipientFilterableObjectFlags), "msExchRecipientFilterFlags", ADPropertyDefinitionFlags.PersistDefaultValue, RecipientFilterableObjectFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020BA RID: 8378
		public static readonly ADPropertyDefinition RecipientContainer = SharedPropertyDefinitions.RecipientContainer;

		// Token: 0x040020BB RID: 8379
		public static readonly ADPropertyDefinition IncludedRecipients = new ADPropertyDefinition("IncludedRecipients", ExchangeObjectVersion.Exchange2007, typeof(WellKnownRecipientType?), null, ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new NullableWellKnownRecipientTypeConstraint()
		}, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.IncludeRecipientGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.IncludeRecipientSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020BC RID: 8380
		public static readonly ADPropertyDefinition ConditionalDepartment = new ADPropertyDefinition("ConditionalDepartment", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 64)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.DepartmentGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalDepartment), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.DepartmentSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020BD RID: 8381
		public static readonly ADPropertyDefinition ConditionalCompany = new ADPropertyDefinition("ConditionalCompany", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CompanyGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCompany), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CompanySetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020BE RID: 8382
		public static readonly ADPropertyDefinition ConditionalStateOrProvince = new ADPropertyDefinition("ConditionalStateOrProvince", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 128)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.StateOrProvinceGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalStateOrProvince), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.StateOrProvinceSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020BF RID: 8383
		public static readonly ADPropertyDefinition ConditionalCustomAttribute1 = new ADPropertyDefinition("ConditionalCustomAttribute1", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute1), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute1, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C0 RID: 8384
		public static readonly ADPropertyDefinition ConditionalCustomAttribute2 = new ADPropertyDefinition("ConditionalCustomAttribute2", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute2), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute2, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C1 RID: 8385
		public static readonly ADPropertyDefinition ConditionalCustomAttribute3 = new ADPropertyDefinition("ConditionalCustomAttribute3", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute3), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute3, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C2 RID: 8386
		public static readonly ADPropertyDefinition ConditionalCustomAttribute4 = new ADPropertyDefinition("ConditionalCustomAttribute4", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute4), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute4, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C3 RID: 8387
		public static readonly ADPropertyDefinition ConditionalCustomAttribute5 = new ADPropertyDefinition("ConditionalCustomAttribute5", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute5), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute5, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C4 RID: 8388
		public static readonly ADPropertyDefinition ConditionalCustomAttribute6 = new ADPropertyDefinition("ConditionalCustomAttribute6", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute6), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute6, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C5 RID: 8389
		public static readonly ADPropertyDefinition ConditionalCustomAttribute7 = new ADPropertyDefinition("ConditionalCustomAttribute7", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute7), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute7, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C6 RID: 8390
		public static readonly ADPropertyDefinition ConditionalCustomAttribute8 = new ADPropertyDefinition("ConditionalCustomAttribute8", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute8), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute8, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C7 RID: 8391
		public static readonly ADPropertyDefinition ConditionalCustomAttribute9 = new ADPropertyDefinition("ConditionalCustomAttribute9", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute9), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute9, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C8 RID: 8392
		public static readonly ADPropertyDefinition ConditionalCustomAttribute10 = new ADPropertyDefinition("ConditionalCustomAttribute10", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute10), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute10, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020C9 RID: 8393
		public static readonly ADPropertyDefinition ConditionalCustomAttribute11 = new ADPropertyDefinition("ConditionalCustomAttribute11", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute11), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute11, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020CA RID: 8394
		public static readonly ADPropertyDefinition ConditionalCustomAttribute12 = new ADPropertyDefinition("ConditionalCustomAttribute12", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute12), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute12, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020CB RID: 8395
		public static readonly ADPropertyDefinition ConditionalCustomAttribute13 = new ADPropertyDefinition("ConditionalCustomAttribute13", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute13), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute13, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020CC RID: 8396
		public static readonly ADPropertyDefinition ConditionalCustomAttribute14 = new ADPropertyDefinition("ConditionalCustomAttribute14", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute14), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute14, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020CD RID: 8397
		public static readonly ADPropertyDefinition ConditionalCustomAttribute15 = new ADPropertyDefinition("ConditionalCustomAttribute15", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 2048)
		}, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter,
			EmailAddressPolicySchema.LdapRecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.CustomAttributeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute15), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.CustomAttributeSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter, EmailAddressPolicySchema.ConditionalCustomAttribute15, EmailAddressPolicySchema.LdapRecipientFilter, false);
		}, null, null);

		// Token: 0x040020CE RID: 8398
		public static readonly ADPropertyDefinition RecipientFilterType = new ADPropertyDefinition("RecipientFilterType", ExchangeObjectVersion.Exchange2007, typeof(WellKnownRecipientFilterType), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, WellKnownRecipientFilterType.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterMetadata,
			EmailAddressPolicySchema.RecipientFilter
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.RecipientFilterTypeGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterMetadata, EmailAddressPolicySchema.RecipientFilter), null, null, null);

		// Token: 0x040020CF RID: 8399
		public static readonly ADPropertyDefinition EnabledEmailAddressTemplates = new ADPropertyDefinition("EnabledEmailAddressTemplates", ExchangeObjectVersion.Exchange2003, typeof(ProxyAddressTemplate), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new EnabledEmailAddressTemplatesConstraint()
		}, new ProviderPropertyDefinition[]
		{
			EmailAddressPolicySchema.RawEnabledEmailAddressTemplates
		}, null, new GetterDelegate(EmailAddressPolicy.EnabledEmailAddressTemplatesGetter), new SetterDelegate(EmailAddressPolicy.EnabledEmailAddressTemplatesSetter), null, null);

		// Token: 0x040020D0 RID: 8400
		public static readonly ADPropertyDefinition EnabledPrimarySMTPAddressTemplate = new ADPropertyDefinition("EnabledPrimarySMTPAddressTemplate", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			EmailAddressPolicySchema.RawEnabledEmailAddressTemplates
		}, null, new GetterDelegate(EmailAddressPolicy.EnabledPrimarySMTPAddressTemplateGetter), new SetterDelegate(EmailAddressPolicy.EnabledPrimarySMTPAddressTemplateSetter), null, null);

		// Token: 0x040020D1 RID: 8401
		public static readonly ADPropertyDefinition RecipientFilterApplied = new ADPropertyDefinition("RecipientFilterApplied", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.RecipientFilterFlags
		}, null, (IPropertyBag propertyBag) => RecipientFilterHelper.RecipientFilterAppliedGetter(propertyBag, EmailAddressPolicySchema.RecipientFilterFlags), delegate(object value, IPropertyBag propertyBag)
		{
			RecipientFilterHelper.RecipientFilterAppliedSetter(value, propertyBag, EmailAddressPolicySchema.RecipientFilterFlags);
		}, null, null);

		// Token: 0x040020D2 RID: 8402
		public static readonly ADPropertyDefinition HasEmailAddressSetting = new ADPropertyDefinition("HasEmailAddressSetting", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			EmailAddressPolicySchema.PolicyOptionListValue
		}, null, (IPropertyBag propertyBag) => EmailAddressPolicy.IsOfPolicyType((MultiValuedProperty<byte[]>)propertyBag[EmailAddressPolicySchema.PolicyOptionListValue], EmailAddressPolicy.PolicyGuid), null, null, null);

		// Token: 0x040020D3 RID: 8403
		public static readonly ADPropertyDefinition HasMailboxManagerSetting = new ADPropertyDefinition("HasMailboxManagerSetting", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			EmailAddressPolicySchema.PolicyOptionListValue
		}, null, (IPropertyBag propertyBag) => EmailAddressPolicy.IsOfPolicyType((MultiValuedProperty<byte[]>)propertyBag[EmailAddressPolicySchema.PolicyOptionListValue], EmailAddressPolicy.MailboxSettingPolicyGuid), null, null, null);
	}
}
