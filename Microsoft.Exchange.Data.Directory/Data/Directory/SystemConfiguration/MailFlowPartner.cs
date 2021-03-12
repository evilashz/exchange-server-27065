using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004AA RID: 1194
	[Serializable]
	public class MailFlowPartner : ADConfigurationObject
	{
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000D61CF File Offset: 0x000D43CF
		// (set) Token: 0x06003699 RID: 13977 RVA: 0x000D61E6 File Offset: 0x000D43E6
		public int? InboundConnectorId
		{
			get
			{
				return (int?)this.propertyBag[MailFlowPartner.MailFlowPartnerSchema.InboundConnectorId];
			}
			set
			{
				this.propertyBag[MailFlowPartner.MailFlowPartnerSchema.InboundConnectorId] = value;
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x000D61FE File Offset: 0x000D43FE
		// (set) Token: 0x0600369B RID: 13979 RVA: 0x000D6215 File Offset: 0x000D4415
		public int? OutboundConnectorId
		{
			get
			{
				return (int?)this.propertyBag[MailFlowPartner.MailFlowPartnerSchema.OutboundConnectorId];
			}
			set
			{
				this.propertyBag[MailFlowPartner.MailFlowPartnerSchema.OutboundConnectorId] = value;
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000D622D File Offset: 0x000D442D
		// (set) Token: 0x0600369D RID: 13981 RVA: 0x000D6244 File Offset: 0x000D4444
		[Parameter(Mandatory = false)]
		public MailFlowPartnerInternalMailContentType InternalMailContentType
		{
			get
			{
				return (MailFlowPartnerInternalMailContentType)this.propertyBag[MailFlowPartner.MailFlowPartnerSchema.InternalMailContentType];
			}
			set
			{
				this.propertyBag[MailFlowPartner.MailFlowPartnerSchema.InternalMailContentType] = value;
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000D625C File Offset: 0x000D445C
		internal override ADObjectSchema Schema
		{
			get
			{
				return MailFlowPartner.schema;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x000D6263 File Offset: 0x000D4463
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchTransportResellerSettings";
			}
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000D626A File Offset: 0x000D446A
		internal static object InboundConnectorIdGetter(IPropertyBag propertyBag)
		{
			return MailFlowPartner.ConnectorIdGetter(propertyBag, MailFlowPartner.MailFlowPartnerSchema.InboundConnectorIdString);
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000D6277 File Offset: 0x000D4477
		internal static void InboundConnectorIdSetter(object value, IPropertyBag propertyBag)
		{
			MailFlowPartner.ConnectorIdSetter(value, propertyBag, MailFlowPartner.MailFlowPartnerSchema.InboundConnectorIdString);
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000D6285 File Offset: 0x000D4485
		internal static object OutboundConnectorIdGetter(IPropertyBag propertyBag)
		{
			return MailFlowPartner.ConnectorIdGetter(propertyBag, MailFlowPartner.MailFlowPartnerSchema.OutboundConnectorIdString);
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000D6292 File Offset: 0x000D4492
		internal static void OutboundConnectorIdSetter(object value, IPropertyBag propertyBag)
		{
			MailFlowPartner.ConnectorIdSetter(value, propertyBag, MailFlowPartner.MailFlowPartnerSchema.OutboundConnectorIdString);
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000D62A0 File Offset: 0x000D44A0
		private static void ConnectorIdSetter(object value, IPropertyBag propertyBag, ADPropertyDefinition connectorIdProperty)
		{
			if (value != null)
			{
				propertyBag[connectorIdProperty] = value.ToString();
				return;
			}
			propertyBag[connectorIdProperty] = null;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000D62BC File Offset: 0x000D44BC
		private static object ConnectorIdGetter(IPropertyBag propertyBag, ADPropertyDefinition connectorIdProperty)
		{
			string text = (string)propertyBag[connectorIdProperty];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			int num;
			if (!int.TryParse(text, out num))
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ConnectorIdIsNotAnInteger, connectorIdProperty, text));
			}
			return num;
		}

		// Token: 0x040024E0 RID: 9440
		public const string MostDerivedClass = "msExchTransportResellerSettings";

		// Token: 0x040024E1 RID: 9441
		private static MailFlowPartner.MailFlowPartnerSchema schema = ObjectSchema.GetInstance<MailFlowPartner.MailFlowPartnerSchema>();

		// Token: 0x020004AB RID: 1195
		internal class MailFlowPartnerSchema : ADConfigurationObjectSchema
		{
			// Token: 0x040024E2 RID: 9442
			public static readonly ADPropertyDefinition InboundConnectorIdString = new ADPropertyDefinition("InboundConnectorIdString", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchTransportResellerSettingsInboundGatewayID", ADPropertyDefinitionFlags.WriteOnce, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
			{
				new Int32ParsableNullableStringConstraint()
			}, null, null);

			// Token: 0x040024E3 RID: 9443
			public static readonly ADPropertyDefinition OutboundConnectorIdString = new ADPropertyDefinition("OutboundConnectorIdString", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchTransportResellerSettingsOutboundGatewayID", ADPropertyDefinitionFlags.WriteOnce, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
			{
				new Int32ParsableNullableStringConstraint()
			}, null, null);

			// Token: 0x040024E4 RID: 9444
			public static readonly ADPropertyDefinition InboundConnectorId = new ADPropertyDefinition("InboundConnectorId", ExchangeObjectVersion.Exchange2003, typeof(int?), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
			{
				MailFlowPartner.MailFlowPartnerSchema.InboundConnectorIdString
			}, null, new GetterDelegate(MailFlowPartner.InboundConnectorIdGetter), new SetterDelegate(MailFlowPartner.InboundConnectorIdSetter), null, null);

			// Token: 0x040024E5 RID: 9445
			public static readonly ADPropertyDefinition OutboundConnectorId = new ADPropertyDefinition("OutboundConnectorId", ExchangeObjectVersion.Exchange2003, typeof(int?), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
			{
				MailFlowPartner.MailFlowPartnerSchema.OutboundConnectorIdString
			}, null, new GetterDelegate(MailFlowPartner.OutboundConnectorIdGetter), new SetterDelegate(MailFlowPartner.OutboundConnectorIdSetter), null, null);

			// Token: 0x040024E6 RID: 9446
			public static readonly ADPropertyDefinition InternalMailContentType = new ADPropertyDefinition("InternalMailContentType", ExchangeObjectVersion.Exchange2003, typeof(MailFlowPartnerInternalMailContentType), "msExchTransportResellerIntraTenantMailContentType", ADPropertyDefinitionFlags.None, MailFlowPartnerInternalMailContentType.None, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
			{
				new EnumValueDefinedConstraint(typeof(MailFlowPartnerInternalMailContentType))
			}, null, null);
		}
	}
}
