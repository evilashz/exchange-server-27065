using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006AA RID: 1706
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class IRMConfiguration : ADContainer
	{
		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x00121E22 File Offset: 0x00120022
		internal override ADObjectSchema Schema
		{
			get
			{
				return IRMConfiguration.adSchema;
			}
		}

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06004EC0 RID: 20160 RVA: 0x00121E29 File Offset: 0x00120029
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchControlPointConfig";
			}
		}

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x00121E30 File Offset: 0x00120030
		internal override ADObjectId ParentPath
		{
			get
			{
				return IRMConfiguration.parentPath;
			}
		}

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x00121E37 File Offset: 0x00120037
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x00121E3F File Offset: 0x0012003F
		// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x00121E6C File Offset: 0x0012006C
		[Parameter(Mandatory = false)]
		public TransportDecryptionSetting TransportDecryptionSetting
		{
			get
			{
				if ((bool)this[IRMConfigurationSchema.TransportDecryptionOptional])
				{
					return TransportDecryptionSetting.Optional;
				}
				if ((bool)this[IRMConfigurationSchema.TransportDecryptionMandatory])
				{
					return TransportDecryptionSetting.Mandatory;
				}
				return TransportDecryptionSetting.Disabled;
			}
			set
			{
				if (value == TransportDecryptionSetting.Optional)
				{
					this[IRMConfigurationSchema.TransportDecryptionOptional] = true;
					this[IRMConfigurationSchema.TransportDecryptionMandatory] = false;
					return;
				}
				if (value == TransportDecryptionSetting.Mandatory)
				{
					this[IRMConfigurationSchema.TransportDecryptionMandatory] = true;
					this[IRMConfigurationSchema.TransportDecryptionOptional] = false;
					return;
				}
				this[IRMConfigurationSchema.TransportDecryptionMandatory] = false;
				this[IRMConfigurationSchema.TransportDecryptionOptional] = false;
			}
		}

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x00121EE9 File Offset: 0x001200E9
		// (set) Token: 0x06004EC6 RID: 20166 RVA: 0x00121EFB File Offset: 0x001200FB
		[Parameter]
		public Uri ServiceLocation
		{
			get
			{
				return (Uri)this[IRMConfigurationSchema.ServiceLocation];
			}
			set
			{
				this[IRMConfigurationSchema.ServiceLocation] = value;
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x00121F09 File Offset: 0x00120109
		// (set) Token: 0x06004EC8 RID: 20168 RVA: 0x00121F1B File Offset: 0x0012011B
		[Parameter]
		public Uri PublishingLocation
		{
			get
			{
				return (Uri)this[IRMConfigurationSchema.PublishingLocation];
			}
			set
			{
				this[IRMConfigurationSchema.PublishingLocation] = value;
			}
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x00121F29 File Offset: 0x00120129
		// (set) Token: 0x06004ECA RID: 20170 RVA: 0x00121F3B File Offset: 0x0012013B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Uri> LicensingLocation
		{
			get
			{
				return (MultiValuedProperty<Uri>)this[IRMConfigurationSchema.LicensingLocation];
			}
			set
			{
				this[IRMConfigurationSchema.LicensingLocation] = value;
			}
		}

		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x00121F49 File Offset: 0x00120149
		// (set) Token: 0x06004ECC RID: 20172 RVA: 0x00121F5B File Offset: 0x0012015B
		[Parameter(Mandatory = false)]
		public bool JournalReportDecryptionEnabled
		{
			get
			{
				return (bool)this[IRMConfigurationSchema.JournalReportDecryptionEnabled];
			}
			set
			{
				this[IRMConfigurationSchema.JournalReportDecryptionEnabled] = value;
			}
		}

		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x00121F6E File Offset: 0x0012016E
		// (set) Token: 0x06004ECE RID: 20174 RVA: 0x00121F80 File Offset: 0x00120180
		[Parameter(Mandatory = false)]
		public bool ExternalLicensingEnabled
		{
			get
			{
				return (bool)this[IRMConfigurationSchema.ExternalLicensingEnabled];
			}
			set
			{
				this[IRMConfigurationSchema.ExternalLicensingEnabled] = value;
			}
		}

		// Token: 0x170019DA RID: 6618
		// (get) Token: 0x06004ECF RID: 20175 RVA: 0x00121F93 File Offset: 0x00120193
		// (set) Token: 0x06004ED0 RID: 20176 RVA: 0x00121FA5 File Offset: 0x001201A5
		[Parameter(Mandatory = false)]
		public bool InternalLicensingEnabled
		{
			get
			{
				return (bool)this[IRMConfigurationSchema.InternalLicensingEnabled];
			}
			set
			{
				this[IRMConfigurationSchema.InternalLicensingEnabled] = value;
			}
		}

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x00121FB8 File Offset: 0x001201B8
		// (set) Token: 0x06004ED2 RID: 20178 RVA: 0x00121FCA File Offset: 0x001201CA
		[Parameter(Mandatory = false)]
		public bool SearchEnabled
		{
			get
			{
				return (bool)this[IRMConfigurationSchema.SearchEnabled];
			}
			set
			{
				this[IRMConfigurationSchema.SearchEnabled] = value;
			}
		}

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x00121FDD File Offset: 0x001201DD
		// (set) Token: 0x06004ED4 RID: 20180 RVA: 0x00121FEF File Offset: 0x001201EF
		[Parameter(Mandatory = false)]
		public bool ClientAccessServerEnabled
		{
			get
			{
				return (bool)this[IRMConfigurationSchema.ClientAccessServerEnabled];
			}
			set
			{
				this[IRMConfigurationSchema.ClientAccessServerEnabled] = value;
			}
		}

		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x00122002 File Offset: 0x00120202
		// (set) Token: 0x06004ED6 RID: 20182 RVA: 0x00122014 File Offset: 0x00120214
		public bool InternetConfidentialEnabled
		{
			get
			{
				return (bool)this[IRMConfigurationSchema.InternetConfidentialEnabled];
			}
			set
			{
				this[IRMConfigurationSchema.InternetConfidentialEnabled] = value;
			}
		}

		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x00122027 File Offset: 0x00120227
		// (set) Token: 0x06004ED8 RID: 20184 RVA: 0x0012203C File Offset: 0x0012023C
		[Parameter(Mandatory = false)]
		public bool EDiscoverySuperUserEnabled
		{
			get
			{
				return !(bool)this[IRMConfigurationSchema.EDiscoverySuperUserDisabled];
			}
			set
			{
				this[IRMConfigurationSchema.EDiscoverySuperUserDisabled] = !value;
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x00122052 File Offset: 0x00120252
		// (set) Token: 0x06004EDA RID: 20186 RVA: 0x00122064 File Offset: 0x00120264
		[Parameter(Mandatory = false)]
		public Uri RMSOnlineKeySharingLocation
		{
			get
			{
				return (Uri)this[IRMConfigurationSchema.RMSOnlineKeySharingLocation];
			}
			set
			{
				this[IRMConfigurationSchema.RMSOnlineKeySharingLocation] = value;
			}
		}

		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x00122072 File Offset: 0x00120272
		// (set) Token: 0x06004EDC RID: 20188 RVA: 0x00122084 File Offset: 0x00120284
		public int ServerCertificatesVersion
		{
			get
			{
				return (int)this[IRMConfigurationSchema.ServerCertificatesVersion];
			}
			set
			{
				this[IRMConfigurationSchema.ServerCertificatesVersion] = value;
			}
		}

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x00122097 File Offset: 0x00120297
		// (set) Token: 0x06004EDE RID: 20190 RVA: 0x001220A9 File Offset: 0x001202A9
		public string SharedServerBoxRacIdentity
		{
			get
			{
				return (string)this[IRMConfigurationSchema.SharedServerBoxRacIdentity];
			}
			set
			{
				this[IRMConfigurationSchema.SharedServerBoxRacIdentity] = value;
			}
		}

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x001220B7 File Offset: 0x001202B7
		// (set) Token: 0x06004EE0 RID: 20192 RVA: 0x001220C9 File Offset: 0x001202C9
		public string RMSOnlineVersion
		{
			get
			{
				return (string)this[IRMConfigurationSchema.RMSOnlineVersion];
			}
			set
			{
				this[IRMConfigurationSchema.RMSOnlineVersion] = value;
			}
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x001220D8 File Offset: 0x001202D8
		internal static IRMConfiguration Read(IConfigurationSession session)
		{
			bool flag;
			return IRMConfiguration.Read(session, out flag);
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x001220F0 File Offset: 0x001202F0
		internal static IRMConfiguration Read(IConfigurationSession session, out bool inMemory)
		{
			inMemory = false;
			IRMConfiguration[] array = session.Find<IRMConfiguration>(null, QueryScope.SubTree, null, null, 1);
			if (array != null && array.Length != 0 && array[0] != null)
			{
				return array[0];
			}
			inMemory = true;
			IRMConfiguration irmconfiguration = new IRMConfiguration();
			irmconfiguration.SetId(session, "ControlPoint Config");
			irmconfiguration.OrganizationId = session.SessionSettings.CurrentOrganizationId;
			if (session.SessionSettings.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
			{
				irmconfiguration.ExternalLicensingEnabled = true;
			}
			return irmconfiguration;
		}

		// Token: 0x040035CC RID: 13772
		private const string MostDerivedClassInternal = "msExchControlPointConfig";

		// Token: 0x040035CD RID: 13773
		private const string singletonName = "ControlPoint Config";

		// Token: 0x040035CE RID: 13774
		private static readonly IRMConfigurationSchema adSchema = ObjectSchema.GetInstance<IRMConfigurationSchema>();

		// Token: 0x040035CF RID: 13775
		private static readonly ADObjectId parentPath = new ADObjectId("CN=Transport Settings");
	}
}
