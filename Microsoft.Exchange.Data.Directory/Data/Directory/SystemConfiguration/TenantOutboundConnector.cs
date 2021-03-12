using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006B5 RID: 1717
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class TenantOutboundConnector : ADConfigurationObject
	{
		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x00123D1D File Offset: 0x00121F1D
		internal override ADObjectSchema Schema
		{
			get
			{
				return TenantOutboundConnector.schema;
			}
		}

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x06004F3B RID: 20283 RVA: 0x00123D24 File Offset: 0x00121F24
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TenantOutboundConnector.mostDerivedClass;
			}
		}

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x06004F3C RID: 20284 RVA: 0x00123D2B File Offset: 0x00121F2B
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x00123D3E File Offset: 0x00121F3E
		internal override ADObjectId ParentPath
		{
			get
			{
				return TenantOutboundConnector.RootId;
			}
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x00123D48 File Offset: 0x00121F48
		internal static object RecipientDomainsGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<SmtpDomainWithSubdomains> multiValuedProperty = null;
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[TenantOutboundConnectorSchema.RecipientDomains];
			if (multiValuedProperty2 != null)
			{
				multiValuedProperty = new MultiValuedProperty<SmtpDomainWithSubdomains>();
				foreach (string text in multiValuedProperty2)
				{
					SmtpDomainWithSubdomains smtpDomainWithSubdomains = null;
					if (!SmtpDomainWithSubdomains.TryParse(text, out smtpDomainWithSubdomains))
					{
						AddressSpace addressSpace = null;
						if (AddressSpace.TryParse(text, out addressSpace) && addressSpace.IsSmtpType)
						{
							smtpDomainWithSubdomains = addressSpace.DomainWithSubdomains;
						}
					}
					if (smtpDomainWithSubdomains != null)
					{
						multiValuedProperty.Add(smtpDomainWithSubdomains);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x00123DE4 File Offset: 0x00121FE4
		internal static void RecipientDomainsSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = null;
			MultiValuedProperty<SmtpDomainWithSubdomains> multiValuedProperty2 = (MultiValuedProperty<SmtpDomainWithSubdomains>)value;
			if (multiValuedProperty2 != null)
			{
				multiValuedProperty = new MultiValuedProperty<string>();
				foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in multiValuedProperty2)
				{
					multiValuedProperty.Add(smtpDomainWithSubdomains.ToString());
				}
			}
			propertyBag[TenantOutboundConnectorSchema.RecipientDomains] = multiValuedProperty;
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x00123E58 File Offset: 0x00122058
		internal static object TlsAuthLevelGetter(IPropertyBag propertyBag)
		{
			return SmtpSendConnectorConfig.TlsAuthLevelGetter(propertyBag, TenantOutboundConnectorSchema.OutboundConnectorFlags);
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x00123E65 File Offset: 0x00122065
		internal static void TlsAuthLevelSetter(object value, IPropertyBag propertyBag)
		{
			SmtpSendConnectorConfig.TlsAuthLevelSetter(value, propertyBag, TenantOutboundConnectorSchema.OutboundConnectorFlags);
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x00123E7C File Offset: 0x0012207C
		internal static object SmartHostsGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[TenantOutboundConnectorSchema.SmartHostsString];
			if (string.IsNullOrEmpty(text))
			{
				return new MultiValuedProperty<SmartHost>(false, TenantOutboundConnectorSchema.SmartHosts, new SmartHost[0]);
			}
			List<SmartHost> routingHostsFromString = RoutingHost.GetRoutingHostsFromString<SmartHost>(text, (RoutingHost routingHost) => new SmartHost(routingHost));
			return new MultiValuedProperty<SmartHost>(false, TenantOutboundConnectorSchema.SmartHosts, routingHostsFromString);
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x00123EEC File Offset: 0x001220EC
		internal static void SmartHostsSetter(object value, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				propertyBag[TenantOutboundConnectorSchema.SmartHostsString] = string.Empty;
				return;
			}
			MultiValuedProperty<SmartHost> routingHostWrappers = (MultiValuedProperty<SmartHost>)value;
			string value2 = RoutingHost.ConvertRoutingHostsToString<SmartHost>(routingHostWrappers, (SmartHost host) => host.InnerRoutingHost);
			propertyBag[TenantOutboundConnectorSchema.SmartHostsString] = value2;
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x00123F44 File Offset: 0x00122144
		internal override void InitializeSchema()
		{
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x00123F46 File Offset: 0x00122146
		public TenantOutboundConnector()
		{
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x00123F4E File Offset: 0x0012214E
		internal TenantOutboundConnector(IConfigurationSession session, string tenantId)
		{
			this.m_Session = session;
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x00123F69 File Offset: 0x00122169
		internal TenantOutboundConnector(string tenantId)
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x00123F7D File Offset: 0x0012217D
		// (set) Token: 0x06004F49 RID: 20297 RVA: 0x00123F8F File Offset: 0x0012218F
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[TenantOutboundConnectorSchema.Enabled];
			}
			set
			{
				this[TenantOutboundConnectorSchema.Enabled] = value;
			}
		}

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06004F4A RID: 20298 RVA: 0x00123FA2 File Offset: 0x001221A2
		// (set) Token: 0x06004F4B RID: 20299 RVA: 0x00123FB4 File Offset: 0x001221B4
		[Parameter(Mandatory = false)]
		public bool UseMXRecord
		{
			get
			{
				return (bool)this[TenantOutboundConnectorSchema.UseMxRecord];
			}
			set
			{
				this[TenantOutboundConnectorSchema.UseMxRecord] = value;
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x00123FC7 File Offset: 0x001221C7
		// (set) Token: 0x06004F4D RID: 20301 RVA: 0x00123FD9 File Offset: 0x001221D9
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)this[TenantOutboundConnectorSchema.Comment];
			}
			set
			{
				this[TenantOutboundConnectorSchema.Comment] = value;
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x06004F4E RID: 20302 RVA: 0x00123FE7 File Offset: 0x001221E7
		// (set) Token: 0x06004F4F RID: 20303 RVA: 0x00123FF9 File Offset: 0x001221F9
		[Parameter(Mandatory = false)]
		public TenantConnectorType ConnectorType
		{
			get
			{
				return (TenantConnectorType)this[TenantOutboundConnectorSchema.ConnectorType];
			}
			set
			{
				this[TenantOutboundConnectorSchema.ConnectorType] = (int)value;
			}
		}

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x06004F50 RID: 20304 RVA: 0x0012400C File Offset: 0x0012220C
		// (set) Token: 0x06004F51 RID: 20305 RVA: 0x0012401E File Offset: 0x0012221E
		[Parameter(Mandatory = false)]
		public TenantConnectorSource ConnectorSource
		{
			get
			{
				return (TenantConnectorSource)this[TenantOutboundConnectorSchema.ConnectorSourceFlags];
			}
			set
			{
				this[TenantOutboundConnectorSchema.ConnectorSourceFlags] = (int)value;
			}
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x06004F52 RID: 20306 RVA: 0x00124031 File Offset: 0x00122231
		// (set) Token: 0x06004F53 RID: 20307 RVA: 0x00124043 File Offset: 0x00122243
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpDomainWithSubdomains> RecipientDomains
		{
			get
			{
				return (MultiValuedProperty<SmtpDomainWithSubdomains>)this[TenantOutboundConnectorSchema.RecipientDomainsEx];
			}
			set
			{
				this[TenantOutboundConnectorSchema.RecipientDomainsEx] = value;
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x00124051 File Offset: 0x00122251
		// (set) Token: 0x06004F55 RID: 20309 RVA: 0x00124063 File Offset: 0x00122263
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmartHost> SmartHosts
		{
			get
			{
				return (MultiValuedProperty<SmartHost>)this[TenantOutboundConnectorSchema.SmartHosts];
			}
			set
			{
				this[TenantOutboundConnectorSchema.SmartHosts] = value;
			}
		}

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x00124071 File Offset: 0x00122271
		// (set) Token: 0x06004F57 RID: 20311 RVA: 0x00124083 File Offset: 0x00122283
		[Parameter(Mandatory = false)]
		public SmtpDomainWithSubdomains TlsDomain
		{
			get
			{
				return (SmtpDomainWithSubdomains)this[TenantOutboundConnectorSchema.TlsDomain];
			}
			set
			{
				this[TenantOutboundConnectorSchema.TlsDomain] = value;
			}
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x06004F58 RID: 20312 RVA: 0x00124091 File Offset: 0x00122291
		// (set) Token: 0x06004F59 RID: 20313 RVA: 0x001240A3 File Offset: 0x001222A3
		[Parameter(Mandatory = false)]
		public TlsAuthLevel? TlsSettings
		{
			get
			{
				return (TlsAuthLevel?)this[TenantOutboundConnectorSchema.TlsSettings];
			}
			set
			{
				this[TenantOutboundConnectorSchema.TlsSettings] = value;
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x06004F5A RID: 20314 RVA: 0x001240B6 File Offset: 0x001222B6
		// (set) Token: 0x06004F5B RID: 20315 RVA: 0x001240C8 File Offset: 0x001222C8
		[Parameter(Mandatory = false)]
		public bool IsTransportRuleScoped
		{
			get
			{
				return (bool)this[TenantOutboundConnectorSchema.IsTransportRuleScoped];
			}
			set
			{
				this[TenantOutboundConnectorSchema.IsTransportRuleScoped] = value;
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x001240DB File Offset: 0x001222DB
		// (set) Token: 0x06004F5D RID: 20317 RVA: 0x001240ED File Offset: 0x001222ED
		[Parameter(Mandatory = false)]
		public bool RouteAllMessagesViaOnPremises
		{
			get
			{
				return (bool)this[TenantOutboundConnectorSchema.RouteAllMessagesViaOnPremises];
			}
			set
			{
				this[TenantOutboundConnectorSchema.RouteAllMessagesViaOnPremises] = value;
			}
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x06004F5E RID: 20318 RVA: 0x00124100 File Offset: 0x00122300
		// (set) Token: 0x06004F5F RID: 20319 RVA: 0x00124112 File Offset: 0x00122312
		[Parameter(Mandatory = false)]
		public bool CloudServicesMailEnabled
		{
			get
			{
				return (bool)this[TenantOutboundConnectorSchema.CloudServicesMailEnabled];
			}
			set
			{
				this[TenantOutboundConnectorSchema.CloudServicesMailEnabled] = value;
			}
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x06004F60 RID: 20320 RVA: 0x00124125 File Offset: 0x00122325
		// (set) Token: 0x06004F61 RID: 20321 RVA: 0x00124137 File Offset: 0x00122337
		[Parameter(Mandatory = false)]
		public bool AllAcceptedDomains
		{
			get
			{
				return (bool)this[TenantOutboundConnectorSchema.AllAcceptedDomains];
			}
			set
			{
				this[TenantOutboundConnectorSchema.AllAcceptedDomains] = value;
			}
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x0012414C File Offset: 0x0012234C
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			if (!(this.IsTransportRuleScoped ^ (!MultiValuedPropertyBase.IsNullOrEmpty(this.RecipientDomains) || this.AllAcceptedDomains)))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.OutboundConnectorIncorrectTransportRuleScopedParameters, TenantOutboundConnectorSchema.IsTransportRuleScoped, this));
			}
			if (this.TlsDomain != null && this.TlsSettings != TlsAuthLevel.DomainValidation)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.OutboundConnectorTlsSettingsInvalidTlsDomainWithoutDomainValidation, TenantOutboundConnectorSchema.TlsSettings, this));
			}
			if (this.TlsDomain == null && this.TlsSettings == TlsAuthLevel.DomainValidation)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.OutboundConnectorTlsSettingsInvalidDomainValidationWithoutTlsDomain, TenantOutboundConnectorSchema.TlsSettings, this));
			}
			this.ValidateSmartHosts(errors);
			if (this.ConnectorType != TenantConnectorType.OnPremises && this.RouteAllMessagesViaOnPremises)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.OutboundConnectorIncorrectRouteAllMessagesViaOnPremises, TenantOutboundConnectorSchema.RouteAllMessagesViaOnPremises, this));
			}
			if (this.ConnectorType != TenantConnectorType.OnPremises && this.CloudServicesMailEnabled)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.OutboundConnectorIncorrectCloudServicesMailEnabled, TenantOutboundConnectorSchema.CloudServicesMailEnabled, this));
			}
			if (this.ConnectorType != TenantConnectorType.OnPremises && this.AllAcceptedDomains)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InboundConnectorIncorrectAllAcceptedDomains, TenantOutboundConnectorSchema.AllAcceptedDomains, this));
			}
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x00124284 File Offset: 0x00122484
		private void ValidateSmartHosts(List<ValidationError> errors)
		{
			if (this.UseMXRecord && !MultiValuedPropertyBase.IsNullOrEmpty(this.SmartHosts))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.OutboundConnectorUseMXRecordShouldBeFalseIfSmartHostsIsPresent, TenantOutboundConnectorSchema.UseMxRecord, this));
			}
			if (!this.UseMXRecord && MultiValuedPropertyBase.IsNullOrEmpty(this.SmartHosts))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.OutboundConnectorSmartHostShouldBePresentIfUseMXRecordFalse, TenantOutboundConnectorSchema.SmartHostsString, this));
			}
		}

		// Token: 0x0400362A RID: 13866
		private static readonly ADObjectId RootId = new ADObjectId("CN=Transport Settings");

		// Token: 0x0400362B RID: 13867
		private static readonly TenantOutboundConnectorSchema schema = ObjectSchema.GetInstance<TenantOutboundConnectorSchema>();

		// Token: 0x0400362C RID: 13868
		private static string mostDerivedClass = "msExchSMTPOutboundConnector";
	}
}
