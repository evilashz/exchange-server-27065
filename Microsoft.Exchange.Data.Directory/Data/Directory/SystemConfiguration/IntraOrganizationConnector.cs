using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200048E RID: 1166
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class IntraOrganizationConnector : ADConfigurationObject
	{
		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000D1F31 File Offset: 0x000D0131
		// (set) Token: 0x060034D6 RID: 13526 RVA: 0x000D1F43 File Offset: 0x000D0143
		public MultiValuedProperty<SmtpDomain> TargetAddressDomains
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this[IntraOrganizationConnectorSchema.TargetAddressDomains];
			}
			set
			{
				this[IntraOrganizationConnectorSchema.TargetAddressDomains] = value;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000D1F51 File Offset: 0x000D0151
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x000D1F63 File Offset: 0x000D0163
		public Uri DiscoveryEndpoint
		{
			get
			{
				return (Uri)this[IntraOrganizationConnectorSchema.DiscoveryEndpoint];
			}
			set
			{
				this[IntraOrganizationConnectorSchema.DiscoveryEndpoint] = value;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000D1F71 File Offset: 0x000D0171
		// (set) Token: 0x060034DA RID: 13530 RVA: 0x000D1F83 File Offset: 0x000D0183
		public bool Enabled
		{
			get
			{
				return (bool)this[IntraOrganizationConnectorSchema.Enabled];
			}
			set
			{
				this[IntraOrganizationConnectorSchema.Enabled] = value;
			}
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000D1F96 File Offset: 0x000D0196
		internal static ADObjectId GetContainerId(IConfigurationSession configSession)
		{
			if (configSession == null)
			{
				throw new ArgumentNullException("configSession");
			}
			return configSession.GetOrgContainerId().GetChildId("Intra Organization Connectors");
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x000D1FB6 File Offset: 0x000D01B6
		internal override ADObjectSchema Schema
		{
			get
			{
				return IntraOrganizationConnector.SchemaObject;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000D1FBD File Offset: 0x000D01BD
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchIntraOrganizationConnector";
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000D1FC4 File Offset: 0x000D01C4
		internal override ADObjectId ParentPath
		{
			get
			{
				return IntraOrganizationConnector.Container;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000D1FCB File Offset: 0x000D01CB
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x040023FF RID: 9215
		internal const string TaskNoun = "IntraOrganizationConnector";

		// Token: 0x04002400 RID: 9216
		internal const string ContainerName = "Intra Organization Connectors";

		// Token: 0x04002401 RID: 9217
		internal const string MostDerivedClass = "msExchIntraOrganizationConnector";

		// Token: 0x04002402 RID: 9218
		internal static readonly ADObjectId Container = new ADObjectId(string.Format("CN={0}", "Intra Organization Connectors"));

		// Token: 0x04002403 RID: 9219
		private static readonly IntraOrganizationConnectorSchema SchemaObject = ObjectSchema.GetInstance<IntraOrganizationConnectorSchema>();
	}
}
