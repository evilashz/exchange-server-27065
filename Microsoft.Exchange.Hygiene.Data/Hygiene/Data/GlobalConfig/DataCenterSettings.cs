using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.GlobalConfig
{
	// Token: 0x02000134 RID: 308
	internal class DataCenterSettings : ADObject
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00025E3E File Offset: 0x0002403E
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00025E50 File Offset: 0x00024050
		public MultiValuedProperty<IPRange> FfoDataCenterPublicIPAddresses
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[DataCenterSettings.DataCenterSettingsSchema.FfoDataCenterPublicIPAddressesProperty];
			}
			set
			{
				this[DataCenterSettings.DataCenterSettingsSchema.FfoDataCenterPublicIPAddressesProperty] = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00025E5E File Offset: 0x0002405E
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00025E70 File Offset: 0x00024070
		public MultiValuedProperty<SmtpX509IdentifierEx> FfoFrontDoorSmtpCertificates
		{
			get
			{
				return (MultiValuedProperty<SmtpX509IdentifierEx>)this[DataCenterSettings.DataCenterSettingsSchema.FfoFrontDoorSmtpCertificatesProperty];
			}
			set
			{
				this[DataCenterSettings.DataCenterSettingsSchema.FfoFrontDoorSmtpCertificatesProperty] = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00025E7E File Offset: 0x0002407E
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00025E90 File Offset: 0x00024090
		public MultiValuedProperty<ServiceProviderSettings> ServiceProviders
		{
			get
			{
				return (MultiValuedProperty<ServiceProviderSettings>)this[DataCenterSettings.DataCenterSettingsSchema.ServiceProvidersProperty];
			}
			set
			{
				this[DataCenterSettings.DataCenterSettingsSchema.ServiceProvidersProperty] = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00025E9E File Offset: 0x0002409E
		internal override ADObjectSchema Schema
		{
			get
			{
				return DataCenterSettings.schema;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00025EA5 File Offset: 0x000240A5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DataCenterSettings.mostDerivedClass;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00025EAC File Offset: 0x000240AC
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040005FB RID: 1531
		private static readonly string mostDerivedClass = "DataCenterSettings";

		// Token: 0x040005FC RID: 1532
		private static readonly DataCenterSettings.DataCenterSettingsSchema schema = ObjectSchema.GetInstance<DataCenterSettings.DataCenterSettingsSchema>();

		// Token: 0x02000135 RID: 309
		internal class DataCenterSettingsSchema : ADObjectSchema
		{
			// Token: 0x040005FD RID: 1533
			internal static readonly HygienePropertyDefinition FfoDataCenterPublicIPAddressesProperty = new HygienePropertyDefinition("FfoDataCenterPublicIPs", typeof(IPRange), null, ADPropertyDefinitionFlags.MultiValued);

			// Token: 0x040005FE RID: 1534
			internal static readonly HygienePropertyDefinition FfoFrontDoorSmtpCertificatesProperty = new HygienePropertyDefinition("FfoFrontDoorSmtpCertificates", typeof(SmtpX509IdentifierEx), null, ADPropertyDefinitionFlags.MultiValued);

			// Token: 0x040005FF RID: 1535
			internal static readonly HygienePropertyDefinition ServiceProvidersProperty = new HygienePropertyDefinition("ServiceProviderSettings", typeof(ServiceProviderSettings), null, ADPropertyDefinitionFlags.MultiValued);
		}
	}
}
