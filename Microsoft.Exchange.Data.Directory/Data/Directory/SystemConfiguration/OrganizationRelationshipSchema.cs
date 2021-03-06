using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200052D RID: 1325
	internal class OrganizationRelationshipSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040027F5 RID: 10229
		public static readonly ADPropertyDefinition DomainNames = new ADPropertyDefinition("DomainNames", ExchangeObjectVersion.Exchange2010, typeof(SmtpDomain), "msExchFedDomainNames", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040027F6 RID: 10230
		public static readonly ADPropertyDefinition FederationEnabledActions = new ADPropertyDefinition("FederationEnabledActions", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchFedEnabledActions", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040027F7 RID: 10231
		public static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchFedIsEnabled", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040027F8 RID: 10232
		public static readonly PropertyDefinition OrganizationContact = new ADPropertyDefinition("OrganizationContact", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), "msExchFedOrgAdminContact", ADPropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, null, null);

		// Token: 0x040027F9 RID: 10233
		public static readonly ADPropertyDefinition TargetApplicationUri = new ADPropertyDefinition("TargetApplicationUri", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedTargetApplicationURI", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.RelativeOrAbsolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.RelativeOrAbsolute)
		}, null, null);

		// Token: 0x040027FA RID: 10234
		public static readonly ADPropertyDefinition TargetAutodiscoverEpr = new ADPropertyDefinition("TargetAutodiscoverEpr", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedTargetAutodiscoverEPR", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040027FB RID: 10235
		public static readonly ADPropertyDefinition TargetSharingEpr = new ADPropertyDefinition("TargetSharingEpr", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedTargetSharingEPR", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040027FC RID: 10236
		public static readonly ADPropertyDefinition TargetOwaURL = new ADPropertyDefinition("TargetOwaURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchFedTargetOwaURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040027FD RID: 10237
		public static readonly PropertyDefinition FreeBusyAccessEnabled = new ADPropertyDefinition("FreeBusyAccessEnabled ", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions
		}, null, OrganizationRelationshipHelper.GetOrganizationRelationshipState("MSExchange.SharingCalendarFreeBusyEnabled", OrganizationRelationshipSchema.FederationEnabledActions), OrganizationRelationshipHelper.SetOrganizationRelationshipState("MSExchange.SharingCalendarFreeBusyEnabled", OrganizationRelationshipSchema.FederationEnabledActions), null, null);

		// Token: 0x040027FE RID: 10238
		public static readonly PropertyDefinition FreeBusyAccessLevel = new ADPropertyDefinition("FreeBusyAccessLevel ", ExchangeObjectVersion.Exchange2010, typeof(FreeBusyAccessLevel), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.SystemConfiguration.FreeBusyAccessLevel.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions
		}, null, new GetterDelegate(OrganizationRelationshipHelper.GetFreeBusyAccessLevel), new SetterDelegate(OrganizationRelationshipHelper.SetFreeBusyAccessLevel), null, null);

		// Token: 0x040027FF RID: 10239
		public static readonly PropertyDefinition FreeBusyAccessScope = new ADPropertyDefinition("FreeBusyAccessScope ", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions,
			OrganizationRelationshipNonAdProperties.FreeBusyAccessScopeCache
		}, null, new GetterDelegate(OrganizationRelationshipHelper.GetFreeBusyAccessScope), new SetterDelegate(OrganizationRelationshipHelper.SetFreeBusyAccessScope), null, null);

		// Token: 0x04002800 RID: 10240
		public static readonly PropertyDefinition MailboxMoveEnabled = new ADPropertyDefinition("MailboxMoveEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions
		}, null, OrganizationRelationshipHelper.GetOrganizationRelationshipState("MSExchange.MailboxMove", OrganizationRelationshipSchema.FederationEnabledActions), OrganizationRelationshipHelper.SetOrganizationRelationshipState("MSExchange.MailboxMove", OrganizationRelationshipSchema.FederationEnabledActions), null, null);

		// Token: 0x04002801 RID: 10241
		public static readonly PropertyDefinition DeliveryReportEnabled = new ADPropertyDefinition("DeliveryReportEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions
		}, null, OrganizationRelationshipHelper.GetOrganizationRelationshipState("MSExchange.DeliveryReportEnabled", OrganizationRelationshipSchema.FederationEnabledActions), OrganizationRelationshipHelper.SetOrganizationRelationshipState("MSExchange.DeliveryReportEnabled", OrganizationRelationshipSchema.FederationEnabledActions), null, null);

		// Token: 0x04002802 RID: 10242
		public static readonly PropertyDefinition MailTipsAccessEnabled = new ADPropertyDefinition("MailTipsAccessEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions
		}, null, OrganizationRelationshipHelper.GetOrganizationRelationshipState("MSExchange.MailTipsAccessEnabled", OrganizationRelationshipSchema.FederationEnabledActions), OrganizationRelationshipHelper.SetOrganizationRelationshipState("MSExchange.MailTipsAccessEnabled", OrganizationRelationshipSchema.FederationEnabledActions), null, null);

		// Token: 0x04002803 RID: 10243
		public static readonly PropertyDefinition MailTipsAccessLevel = new ADPropertyDefinition("MailTipsAccessLevel", ExchangeObjectVersion.Exchange2010, typeof(MailTipsAccessLevel), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.SystemConfiguration.MailTipsAccessLevel.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions
		}, null, new GetterDelegate(OrganizationRelationshipHelper.GetMailTipsAccessLevel), new SetterDelegate(OrganizationRelationshipHelper.SetMailTipsAccessLevel), null, null);

		// Token: 0x04002804 RID: 10244
		public static readonly PropertyDefinition MailTipsAccessScope = new ADPropertyDefinition("MailTipsAccessScope", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions,
			OrganizationRelationshipNonAdProperties.MailTipsAccessScopeScopeCache
		}, null, new GetterDelegate(OrganizationRelationshipHelper.GetMailTipsAccessScope), new SetterDelegate(OrganizationRelationshipHelper.SetMailTipsAccessScope), null, null);

		// Token: 0x04002805 RID: 10245
		public static readonly PropertyDefinition ArchiveAccessEnabled = new ADPropertyDefinition("ArchiveAccessEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions
		}, null, OrganizationRelationshipHelper.GetOrganizationRelationshipState("MSExchange.ArchiveAccessEnabled", OrganizationRelationshipSchema.FederationEnabledActions), OrganizationRelationshipHelper.SetOrganizationRelationshipState("MSExchange.ArchiveAccessEnabled", OrganizationRelationshipSchema.FederationEnabledActions), null, null);

		// Token: 0x04002806 RID: 10246
		public static readonly PropertyDefinition PhotosEnabled = new ADPropertyDefinition("PhotosEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationRelationshipSchema.FederationEnabledActions
		}, null, OrganizationRelationshipHelper.GetOrganizationRelationshipState("MSExchange.PhotosEnabled", OrganizationRelationshipSchema.FederationEnabledActions), OrganizationRelationshipHelper.SetOrganizationRelationshipState("MSExchange.PhotosEnabled", OrganizationRelationshipSchema.FederationEnabledActions), null, null);

		// Token: 0x04002807 RID: 10247
		public static readonly ADPropertyDefinition OnPremisesOrganizationBackLink = new ADPropertyDefinition("OnPremisesOrganizationBackLink", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchTrustedDomainBL ", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
