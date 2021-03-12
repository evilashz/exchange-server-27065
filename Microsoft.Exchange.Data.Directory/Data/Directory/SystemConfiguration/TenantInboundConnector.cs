using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006B1 RID: 1713
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class TenantInboundConnector : ADConfigurationObject
	{
		// Token: 0x06004F12 RID: 20242 RVA: 0x00123334 File Offset: 0x00121534
		internal static object SenderDomainsGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[TenantInboundConnectorSchema.SenderDomainString];
			if (string.IsNullOrEmpty(text))
			{
				return new MultiValuedProperty<AddressSpace>(false, TenantInboundConnectorSchema.SenderDomains, new AddressSpace[0]);
			}
			List<AddressSpace> senderDomainsFromString = TenantInboundConnector.GetSenderDomainsFromString(text);
			return new MultiValuedProperty<AddressSpace>(false, TenantInboundConnectorSchema.SenderDomains, senderDomainsFromString);
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x00123380 File Offset: 0x00121580
		internal static void SenderDomainsSetter(object value, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				propertyBag[TenantInboundConnectorSchema.SenderDomainString] = string.Empty;
				return;
			}
			MultiValuedProperty<AddressSpace> senderDomains = (MultiValuedProperty<AddressSpace>)value;
			string value2 = TenantInboundConnector.ConvertSenderdomainsToString(senderDomains);
			propertyBag[TenantInboundConnectorSchema.SenderDomainString] = value2;
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x001233BC File Offset: 0x001215BC
		internal static string ConvertSenderdomainsToString(IList<AddressSpace> senderDomains)
		{
			if (senderDomains == null || senderDomains.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (AddressSpace addressSpace in senderDomains)
			{
				num++;
				stringBuilder.Append(addressSpace.ToString());
				if (num < senderDomains.Count)
				{
					stringBuilder.Append(',');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x00123440 File Offset: 0x00121640
		internal static List<AddressSpace> GetSenderDomainsFromString(string senderDomainString)
		{
			List<AddressSpace> list = new List<AddressSpace>();
			if (!string.IsNullOrEmpty(senderDomainString))
			{
				string[] array = senderDomainString.Split(new char[]
				{
					TenantInboundConnector.senderDomainsDelimiter
				});
				foreach (string address in array)
				{
					AddressSpace item = null;
					if (!AddressSpace.TryParse(address, out item))
					{
						return new List<AddressSpace>();
					}
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x001234B3 File Offset: 0x001216B3
		internal override ADObjectSchema Schema
		{
			get
			{
				return TenantInboundConnector.schema;
			}
		}

		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x001234BA File Offset: 0x001216BA
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TenantInboundConnector.mostDerivedClass;
			}
		}

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x001234C1 File Offset: 0x001216C1
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x06004F19 RID: 20249 RVA: 0x001234D4 File Offset: 0x001216D4
		internal override ADObjectId ParentPath
		{
			get
			{
				return TenantInboundConnector.RootId;
			}
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x001234DB File Offset: 0x001216DB
		internal override void InitializeSchema()
		{
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x001234DD File Offset: 0x001216DD
		public TenantInboundConnector()
		{
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x001234E5 File Offset: 0x001216E5
		internal TenantInboundConnector(IConfigurationSession session, string tenantId)
		{
			this.m_Session = session;
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x00123500 File Offset: 0x00121700
		internal TenantInboundConnector(string tenantId)
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x00123514 File Offset: 0x00121714
		// (set) Token: 0x06004F1F RID: 20255 RVA: 0x00123526 File Offset: 0x00121726
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[TenantInboundConnectorSchema.Enabled];
			}
			set
			{
				this[TenantInboundConnectorSchema.Enabled] = value;
			}
		}

		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x00123539 File Offset: 0x00121739
		// (set) Token: 0x06004F21 RID: 20257 RVA: 0x0012354B File Offset: 0x0012174B
		[Parameter(Mandatory = false)]
		public TenantConnectorType ConnectorType
		{
			get
			{
				return (TenantConnectorType)this[TenantInboundConnectorSchema.ConnectorType];
			}
			set
			{
				this[TenantInboundConnectorSchema.ConnectorType] = (int)value;
			}
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x0012355E File Offset: 0x0012175E
		// (set) Token: 0x06004F23 RID: 20259 RVA: 0x00123570 File Offset: 0x00121770
		[Parameter(Mandatory = false)]
		public TenantConnectorSource ConnectorSource
		{
			get
			{
				return (TenantConnectorSource)this[TenantInboundConnectorSchema.ConnectorSourceFlags];
			}
			set
			{
				this[TenantInboundConnectorSchema.ConnectorSourceFlags] = (int)value;
			}
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x00123583 File Offset: 0x00121783
		// (set) Token: 0x06004F25 RID: 20261 RVA: 0x00123595 File Offset: 0x00121795
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)this[TenantInboundConnectorSchema.Comment];
			}
			set
			{
				this[TenantInboundConnectorSchema.Comment] = value;
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x06004F26 RID: 20262 RVA: 0x001235A3 File Offset: 0x001217A3
		// (set) Token: 0x06004F27 RID: 20263 RVA: 0x001235B5 File Offset: 0x001217B5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> SenderIPAddresses
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[TenantInboundConnectorSchema.RemoteIPRanges];
			}
			set
			{
				this[TenantInboundConnectorSchema.RemoteIPRanges] = value;
			}
		}

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x001235C3 File Offset: 0x001217C3
		// (set) Token: 0x06004F29 RID: 20265 RVA: 0x001235D5 File Offset: 0x001217D5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<AddressSpace> SenderDomains
		{
			get
			{
				return (MultiValuedProperty<AddressSpace>)this[TenantInboundConnectorSchema.SenderDomains];
			}
			set
			{
				this[TenantInboundConnectorSchema.SenderDomains] = value;
			}
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x06004F2A RID: 20266 RVA: 0x001235E3 File Offset: 0x001217E3
		// (set) Token: 0x06004F2B RID: 20267 RVA: 0x001235F5 File Offset: 0x001217F5
		public MultiValuedProperty<ADObjectId> AssociatedAcceptedDomains
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[TenantInboundConnectorSchema.AssociatedAcceptedDomains];
			}
			set
			{
				this[TenantInboundConnectorSchema.AssociatedAcceptedDomains] = value;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x06004F2C RID: 20268 RVA: 0x00123603 File Offset: 0x00121803
		// (set) Token: 0x06004F2D RID: 20269 RVA: 0x00123615 File Offset: 0x00121815
		[Parameter(Mandatory = false)]
		public bool RequireTls
		{
			get
			{
				return (bool)this[TenantInboundConnectorSchema.RequireTls];
			}
			set
			{
				this[TenantInboundConnectorSchema.RequireTls] = value;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x00123628 File Offset: 0x00121828
		// (set) Token: 0x06004F2F RID: 20271 RVA: 0x0012363A File Offset: 0x0012183A
		[Parameter(Mandatory = false)]
		public bool RestrictDomainsToIPAddresses
		{
			get
			{
				return (bool)this[TenantInboundConnectorSchema.RestrictDomainsToIPAddresses];
			}
			set
			{
				this[TenantInboundConnectorSchema.RestrictDomainsToIPAddresses] = value;
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x06004F30 RID: 20272 RVA: 0x0012364D File Offset: 0x0012184D
		// (set) Token: 0x06004F31 RID: 20273 RVA: 0x0012365F File Offset: 0x0012185F
		[Parameter(Mandatory = false)]
		public bool RestrictDomainsToCertificate
		{
			get
			{
				return (bool)this[TenantInboundConnectorSchema.RestrictDomainsToCertificate];
			}
			set
			{
				this[TenantInboundConnectorSchema.RestrictDomainsToCertificate] = value;
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x06004F32 RID: 20274 RVA: 0x00123672 File Offset: 0x00121872
		// (set) Token: 0x06004F33 RID: 20275 RVA: 0x00123684 File Offset: 0x00121884
		[Parameter(Mandatory = false)]
		public bool CloudServicesMailEnabled
		{
			get
			{
				return (bool)this[TenantInboundConnectorSchema.CloudServicesMailEnabled];
			}
			set
			{
				this[TenantInboundConnectorSchema.CloudServicesMailEnabled] = value;
			}
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x00123697 File Offset: 0x00121897
		// (set) Token: 0x06004F35 RID: 20277 RVA: 0x001236A9 File Offset: 0x001218A9
		[Parameter(Mandatory = false)]
		public TlsCertificate TlsSenderCertificateName
		{
			get
			{
				return (TlsCertificate)this[TenantInboundConnectorSchema.TlsSenderCertificateName];
			}
			set
			{
				this[TenantInboundConnectorSchema.TlsSenderCertificateName] = value;
			}
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x001236B8 File Offset: 0x001218B8
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.ConnectorType == TenantConnectorType.OnPremises && MultiValuedPropertyBase.IsNullOrEmpty(this.SenderIPAddresses) && this.TlsSenderCertificateName == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InboundConnectorMissingTlsCertificateOrSenderIP, TenantInboundConnectorSchema.RemoteIPRanges, this));
			}
			if (this.TlsSenderCertificateName != null && !this.RequireTls)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InboundConnectorRequiredTlsSettingsInvalid, TenantInboundConnectorSchema.RequireTls, this));
			}
			if (this.RestrictDomainsToIPAddresses && MultiValuedPropertyBase.IsNullOrEmpty(this.SenderIPAddresses))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InboundConnectorInvalidRestrictDomainsToIPAddresses, TenantInboundConnectorSchema.RestrictDomainsToIPAddresses, this));
			}
			if (this.RestrictDomainsToIPAddresses && this.RestrictDomainsToCertificate)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InboundConnectorInvalidIPCertificateCombinations, TenantInboundConnectorSchema.RestrictDomainsToCertificate, this));
			}
			if (this.RestrictDomainsToCertificate && this.TlsSenderCertificateName == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InboundConnectorInvalidRestrictDomainsToCertificate, TenantInboundConnectorSchema.RestrictDomainsToCertificate, this));
			}
			if (this.ConnectorType != TenantConnectorType.OnPremises && this.CloudServicesMailEnabled)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InboundConnectorIncorrectCloudServicesMailEnabled, TenantInboundConnectorSchema.CloudServicesMailEnabled, this));
			}
		}

		// Token: 0x0400360B RID: 13835
		internal const int MinCidrLength = 24;

		// Token: 0x0400360C RID: 13836
		private static readonly char senderDomainsDelimiter = ',';

		// Token: 0x0400360D RID: 13837
		private static readonly ADObjectId RootId = new ADObjectId("CN=Transport Settings");

		// Token: 0x0400360E RID: 13838
		private static readonly TenantInboundConnectorSchema schema = ObjectSchema.GetInstance<TenantInboundConnectorSchema>();

		// Token: 0x0400360F RID: 13839
		private static string mostDerivedClass = "msExchSMTPInboundConnector";
	}
}
