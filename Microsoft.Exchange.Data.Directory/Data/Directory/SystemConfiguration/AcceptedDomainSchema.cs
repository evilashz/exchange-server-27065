using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002F8 RID: 760
	internal class AcceptedDomainSchema : ADConfigurationObjectSchema
	{
		// Token: 0x06002361 RID: 9057 RVA: 0x000996F2 File Offset: 0x000978F2
		internal static GetterDelegate AuthenticationTypeGetterDelegate()
		{
			return delegate(IPropertyBag bag)
			{
				if (!AcceptedDomainSchema.IsDomainAssociatedWithLiveNamespace(bag))
				{
					return null;
				}
				return AcceptedDomainSchema.GetRawAuthenticationType(bag);
			};
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0009971E File Offset: 0x0009791E
		internal static GetterDelegate RawAuthenticationTypeGetterDelegate()
		{
			return (IPropertyBag bag) => AcceptedDomainSchema.GetRawAuthenticationType(bag);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00099740 File Offset: 0x00097940
		private static AuthenticationType GetRawAuthenticationType(IPropertyBag bag)
		{
			int num = (int)bag[AcceptedDomainSchema.AcceptedDomainFlags];
			int num2 = 32;
			if (0 == (num & num2))
			{
				return Microsoft.Exchange.Data.Directory.AuthenticationType.Managed;
			}
			return Microsoft.Exchange.Data.Directory.AuthenticationType.Federated;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000997B5 File Offset: 0x000979B5
		internal static SetterDelegate RawAuthenticationTypeSetterDelegate()
		{
			return delegate(object value, IPropertyBag bag)
			{
				ADPropertyDefinition acceptedDomainFlags = AcceptedDomainSchema.AcceptedDomainFlags;
				int num = 32;
				int num2 = (int)bag[acceptedDomainFlags];
				bag[acceptedDomainFlags] = (((AuthenticationType)value == Microsoft.Exchange.Data.Directory.AuthenticationType.Federated) ? (num2 | num) : (num2 & ~num));
			};
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000997EB File Offset: 0x000979EB
		internal static GetterDelegate LiveIdInstanceTypeGetterDelegate()
		{
			return delegate(IPropertyBag bag)
			{
				if (!AcceptedDomainSchema.IsDomainAssociatedWithLiveNamespace(bag))
				{
					return null;
				}
				return AcceptedDomainSchema.GetRawLiveIdInstanceType(bag);
			};
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x00099817 File Offset: 0x00097A17
		internal static GetterDelegate RawLiveIdInstanceTypeGetterDelegate()
		{
			return (IPropertyBag bag) => AcceptedDomainSchema.GetRawLiveIdInstanceType(bag);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x00099838 File Offset: 0x00097A38
		private static LiveIdInstanceType GetRawLiveIdInstanceType(IPropertyBag bag)
		{
			int num = (int)bag[AcceptedDomainSchema.AcceptedDomainFlags];
			int num2 = 128;
			if (0 == (num & num2))
			{
				return Microsoft.Exchange.Data.Directory.LiveIdInstanceType.Consumer;
			}
			return Microsoft.Exchange.Data.Directory.LiveIdInstanceType.Business;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000998B0 File Offset: 0x00097AB0
		internal static SetterDelegate RawLiveIdInstanceTypeSetterDelegate()
		{
			return delegate(object value, IPropertyBag bag)
			{
				ADPropertyDefinition acceptedDomainFlags = AcceptedDomainSchema.AcceptedDomainFlags;
				int num = 128;
				int num2 = (int)bag[acceptedDomainFlags];
				bag[acceptedDomainFlags] = (((LiveIdInstanceType)value == Microsoft.Exchange.Data.Directory.LiveIdInstanceType.Business) ? (num2 | num) : (num2 & ~num));
			};
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000998D0 File Offset: 0x00097AD0
		internal static bool IsDomainAssociatedWithLiveNamespace(IPropertyBag bag)
		{
			if (!Globals.IsMicrosoftHostedOnly)
			{
				return false;
			}
			OrganizationId a = (OrganizationId)bag[ADObjectSchema.OrganizationId];
			return !(a == null) && !(a == Microsoft.Exchange.Data.Directory.OrganizationId.ForestWideOrgId);
		}

		// Token: 0x040015D8 RID: 5592
		internal const int AcceptedDomainTypeShift = 0;

		// Token: 0x040015D9 RID: 5593
		internal const int AcceptedDomainTypeLength = 2;

		// Token: 0x040015DA RID: 5594
		internal const int DefaultShift = 2;

		// Token: 0x040015DB RID: 5595
		internal const int AddressBookEnabledShift = 3;

		// Token: 0x040015DC RID: 5596
		internal const int X400AddressTypeShift = 4;

		// Token: 0x040015DD RID: 5597
		internal const int AuthenticationTypeShift = 5;

		// Token: 0x040015DE RID: 5598
		internal const int PendingRemovalShift = 6;

		// Token: 0x040015DF RID: 5599
		internal const int LiveIdInstanceTypeShift = 7;

		// Token: 0x040015E0 RID: 5600
		internal const int OutboundOnlyShift = 8;

		// Token: 0x040015E1 RID: 5601
		internal const int DefaultFederatedDomain = 9;

		// Token: 0x040015E2 RID: 5602
		internal const int EnableNego2AuthBit = 10;

		// Token: 0x040015E3 RID: 5603
		internal const int CoexistenceShift = 11;

		// Token: 0x040015E4 RID: 5604
		internal const int DualProvisioningEnabledShift = 12;

		// Token: 0x040015E5 RID: 5605
		internal const int PendingFederatedAccountNamespaceShift = 13;

		// Token: 0x040015E6 RID: 5606
		internal const int PendingFederatedDomainShift = 14;

		// Token: 0x040015E7 RID: 5607
		internal const int InitialDomainShift = 15;

		// Token: 0x040015E8 RID: 5608
		internal const int MatchSubDomainsShift = 16;

		// Token: 0x040015E9 RID: 5609
		internal const int PendingCompletionShift = 17;

		// Token: 0x040015EA RID: 5610
		internal const int InitialDomainShiftValue = 32768;

		// Token: 0x040015EB RID: 5611
		public static readonly ADPropertyDefinition DomainName = new ADPropertyDefinition("DomainName", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomainWithSubdomains), "msExchAcceptedDomainName", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040015EC RID: 5612
		public static readonly ADPropertyDefinition CatchAllRecipient = new ADPropertyDefinition("CatchAllRecipient", ExchangeObjectVersion.Exchange2012, typeof(ADObjectId), "msExchCatchAllRecipientLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040015ED RID: 5613
		public static readonly ADPropertyDefinition AcceptedDomainFlags = new ADPropertyDefinition("AcceptedDomainFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchAcceptedDomainFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040015EE RID: 5614
		public static readonly ADPropertyDefinition FederatedOrganizationLink = new ADPropertyDefinition("FederatedOrganizationLink", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchFedAcceptedDomainLink", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040015EF RID: 5615
		public static readonly ADPropertyDefinition MailFlowPartner = new ADPropertyDefinition("MailFlowPartner", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchTransportResellerSettingsLink", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040015F0 RID: 5616
		public static readonly ADPropertyDefinition HomeRealmRecord = new ADPropertyDefinition("HomeRealmRecord", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOfflineOrgIdHomeRealmRecord", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040015F1 RID: 5617
		public static readonly ADPropertyDefinition AcceptedDomainType = ADObject.BitfieldProperty("AcceptedDomainType", 0, 2, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015F2 RID: 5618
		public static readonly ADPropertyDefinition Default = ADObject.BitfieldProperty("Default", 2, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015F3 RID: 5619
		public static readonly ADPropertyDefinition AddressBookEnabled = ADObject.BitfieldProperty("AddressBookEnabled", 3, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015F4 RID: 5620
		public static readonly ADPropertyDefinition X400AddressType = ADObject.BitfieldProperty("X400AddressType", 4, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015F5 RID: 5621
		public static readonly ADPropertyDefinition MatchSubDomains = ADObject.BitfieldProperty("MatchSubDomains", 16, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015F6 RID: 5622
		public static readonly ADPropertyDefinition RawAuthenticationType = new ADPropertyDefinition("RawAuthenticationType", ExchangeObjectVersion.Exchange2007, typeof(AuthenticationType), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.AuthenticationType.Managed, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AcceptedDomainSchema.AcceptedDomainFlags
		}, null, AcceptedDomainSchema.RawAuthenticationTypeGetterDelegate(), AcceptedDomainSchema.RawAuthenticationTypeSetterDelegate(), null, null);

		// Token: 0x040015F7 RID: 5623
		public static readonly ADPropertyDefinition AuthenticationType = new ADPropertyDefinition("AuthenticationType", ExchangeObjectVersion.Exchange2007, typeof(AuthenticationType?), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.AuthenticationType.Managed, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AcceptedDomainSchema.AcceptedDomainFlags,
			ADObjectSchema.OrganizationalUnitRoot,
			ADObjectSchema.ConfigurationUnit
		}, null, AcceptedDomainSchema.AuthenticationTypeGetterDelegate(), null, null, null);

		// Token: 0x040015F8 RID: 5624
		public static readonly ADPropertyDefinition RawLiveIdInstanceType = new ADPropertyDefinition("RawLiveIdInstanceType", ExchangeObjectVersion.Exchange2007, typeof(LiveIdInstanceType), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.LiveIdInstanceType.Consumer, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AcceptedDomainSchema.AcceptedDomainFlags
		}, null, AcceptedDomainSchema.RawLiveIdInstanceTypeGetterDelegate(), AcceptedDomainSchema.RawLiveIdInstanceTypeSetterDelegate(), null, null);

		// Token: 0x040015F9 RID: 5625
		public static readonly ADPropertyDefinition LiveIdInstanceType = new ADPropertyDefinition("LiveIdInstanceType", ExchangeObjectVersion.Exchange2007, typeof(LiveIdInstanceType?), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.LiveIdInstanceType.Consumer, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AcceptedDomainSchema.AcceptedDomainFlags,
			ADObjectSchema.OrganizationalUnitRoot,
			ADObjectSchema.ConfigurationUnit
		}, null, AcceptedDomainSchema.LiveIdInstanceTypeGetterDelegate(), null, null, null);

		// Token: 0x040015FA RID: 5626
		public static readonly ADPropertyDefinition PendingRemoval = ADObject.BitfieldProperty("PendingRemoval", 6, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015FB RID: 5627
		public static readonly ADPropertyDefinition PendingCompletion = ADObject.BitfieldProperty("PendingCompletion", 17, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015FC RID: 5628
		public static readonly ADPropertyDefinition DualProvisioningEnabled = ADObject.BitfieldProperty("DualProvisioningEnabled", 12, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015FD RID: 5629
		public static readonly ADPropertyDefinition OutboundOnly = ADObject.BitfieldProperty("OutboundOnly", 8, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015FE RID: 5630
		public static readonly ADPropertyDefinition IsCoexistenceDomain = ADObject.BitfieldProperty("IsCoexistenceDomain", 11, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x040015FF RID: 5631
		public static readonly ADPropertyDefinition PendingFederatedAccountNamespace = ADObject.BitfieldProperty("PendingFederatedAccountNamespace", 13, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x04001600 RID: 5632
		public static readonly ADPropertyDefinition PendingFederatedDomain = ADObject.BitfieldProperty("PendingFederatedDomain", 14, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x04001601 RID: 5633
		public static readonly ADPropertyDefinition PerimeterFlags = new ADPropertyDefinition("PerimeterFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportInboundSettings", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001602 RID: 5634
		public static readonly ADPropertyDefinition PerimeterDuplicateDetected = new ADPropertyDefinition("PerimeterDuplicateDetected", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AcceptedDomainSchema.PerimeterFlags
		}, null, ADObject.FlagGetterDelegate(AcceptedDomainSchema.PerimeterFlags, 1), ADObject.FlagSetterDelegate(AcceptedDomainSchema.PerimeterFlags, 1), null, null);

		// Token: 0x04001603 RID: 5635
		public static readonly ADPropertyDefinition IsDefaultFederatedDomain = ADObject.BitfieldProperty("IsDefaultFederatedDomain", 9, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x04001604 RID: 5636
		public static readonly ADPropertyDefinition EnableNego2Authentication = ADObject.BitfieldProperty("EnableNego2Authentication", 10, AcceptedDomainSchema.AcceptedDomainFlags);

		// Token: 0x04001605 RID: 5637
		public static readonly ADPropertyDefinition InitialDomain = new ADPropertyDefinition("InitialDomain", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AcceptedDomainSchema.AcceptedDomainFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(AcceptedDomainSchema.AcceptedDomainFlags, 32768UL)), ADObject.FlagGetterDelegate(AcceptedDomainSchema.AcceptedDomainFlags, 32768), ADObject.FlagSetterDelegate(AcceptedDomainSchema.AcceptedDomainFlags, 32768), null, null);

		// Token: 0x04001606 RID: 5638
		internal static readonly ADPropertyDefinition UsnCreated = new ADPropertyDefinition("UsnCreated", ExchangeObjectVersion.Exchange2010, typeof(long), "uSNCreated", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
