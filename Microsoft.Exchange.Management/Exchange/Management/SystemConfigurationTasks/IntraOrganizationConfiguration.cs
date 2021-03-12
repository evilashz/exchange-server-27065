using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A12 RID: 2578
	[Serializable]
	public class IntraOrganizationConfiguration : ConfigurableObject
	{
		// Token: 0x06005C7A RID: 23674 RVA: 0x00185ED6 File Offset: 0x001840D6
		public IntraOrganizationConfiguration() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x06005C7B RID: 23675 RVA: 0x00185EE3 File Offset: 0x001840E3
		// (set) Token: 0x06005C7C RID: 23676 RVA: 0x00185EFA File Offset: 0x001840FA
		public Uri OnPremiseDiscoveryEndpoint
		{
			get
			{
				return (Uri)this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnPremiseDiscoveryEndpoint];
			}
			internal set
			{
				this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnPremiseDiscoveryEndpoint] = value;
			}
		}

		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x06005C7D RID: 23677 RVA: 0x00185F0D File Offset: 0x0018410D
		// (set) Token: 0x06005C7E RID: 23678 RVA: 0x00185F24 File Offset: 0x00184124
		public Uri OnPremiseWebServiceEndpoint
		{
			get
			{
				return (Uri)this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnPremiseWebServiceEndpoint];
			}
			internal set
			{
				this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnPremiseWebServiceEndpoint] = value;
			}
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x06005C7F RID: 23679 RVA: 0x00185F37 File Offset: 0x00184137
		// (set) Token: 0x06005C80 RID: 23680 RVA: 0x00185F4E File Offset: 0x0018414E
		public bool? DeploymentIsCompleteIOCReady
		{
			get
			{
				return (bool?)this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.DeploymentIsCompleteIOCReady];
			}
			internal set
			{
				this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.DeploymentIsCompleteIOCReady] = value;
			}
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x06005C81 RID: 23681 RVA: 0x00185F66 File Offset: 0x00184166
		// (set) Token: 0x06005C82 RID: 23682 RVA: 0x00185F7D File Offset: 0x0018417D
		public bool? HasNonIOCReadyExchangeCASServerVersions
		{
			get
			{
				return (bool?)this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.HasNonIOCReadyExchangeCASServerVersions];
			}
			internal set
			{
				this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.HasNonIOCReadyExchangeCASServerVersions] = value;
			}
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x06005C83 RID: 23683 RVA: 0x00185F95 File Offset: 0x00184195
		// (set) Token: 0x06005C84 RID: 23684 RVA: 0x00185FAC File Offset: 0x001841AC
		public bool? HasNonIOCReadyExchangeMailboxServerVersions
		{
			get
			{
				return (bool?)this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.HasNonIOCReadyExchangeMailboxServerVersions];
			}
			internal set
			{
				this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.HasNonIOCReadyExchangeMailboxServerVersions] = value;
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x06005C85 RID: 23685 RVA: 0x00185FC4 File Offset: 0x001841C4
		// (set) Token: 0x06005C86 RID: 23686 RVA: 0x00185FDB File Offset: 0x001841DB
		public Uri OnlineDiscoveryEndpoint
		{
			get
			{
				return (Uri)this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnlineDiscoveryEndpoint];
			}
			internal set
			{
				this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnlineDiscoveryEndpoint] = value;
			}
		}

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x06005C87 RID: 23687 RVA: 0x00185FEE File Offset: 0x001841EE
		// (set) Token: 0x06005C88 RID: 23688 RVA: 0x00186005 File Offset: 0x00184205
		public string OnlineTargetAddress
		{
			get
			{
				return (string)this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnlineTargetAddress];
			}
			internal set
			{
				this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnlineTargetAddress] = value;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x06005C89 RID: 23689 RVA: 0x00186018 File Offset: 0x00184218
		// (set) Token: 0x06005C8A RID: 23690 RVA: 0x0018602F File Offset: 0x0018422F
		public MultiValuedProperty<SmtpDomain> OnPremiseTargetAddresses
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnPremiseTargetAddresses];
			}
			internal set
			{
				this.propertyBag[IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema.OnPremiseTargetAddresses] = value;
			}
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x06005C8B RID: 23691 RVA: 0x00186042 File Offset: 0x00184242
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return IntraOrganizationConfiguration.SchemaInstance;
			}
		}

		// Token: 0x04003467 RID: 13415
		private static IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema SchemaInstance = ObjectSchema.GetInstance<IntraOrganizationConfiguration.IntraOrganizationConfigurationSchema>();

		// Token: 0x02000A13 RID: 2579
		internal class IntraOrganizationConfigurationSchema : SimpleProviderObjectSchema
		{
			// Token: 0x04003468 RID: 13416
			public static readonly SimpleProviderPropertyDefinition OnPremiseDiscoveryEndpoint = new SimpleProviderPropertyDefinition("OnPremiseDiscoveryEndpoint", ExchangeObjectVersion.Exchange2003, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003469 RID: 13417
			public static readonly SimpleProviderPropertyDefinition OnPremiseWebServiceEndpoint = new SimpleProviderPropertyDefinition("OnPremiseWebServiceEndpoint", ExchangeObjectVersion.Exchange2003, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400346A RID: 13418
			public static readonly SimpleProviderPropertyDefinition DeploymentIsCompleteIOCReady = new SimpleProviderPropertyDefinition("DeploymentIsCompleteIOCReady", ExchangeObjectVersion.Exchange2003, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400346B RID: 13419
			public static readonly SimpleProviderPropertyDefinition HasNonIOCReadyExchangeCASServerVersions = new SimpleProviderPropertyDefinition("HasNonIOCReadyExchangeCASServerVersions", ExchangeObjectVersion.Exchange2003, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400346C RID: 13420
			public static readonly SimpleProviderPropertyDefinition HasNonIOCReadyExchangeMailboxServerVersions = new SimpleProviderPropertyDefinition("HasNonIOCReadyExchangeMailboxServerVersions", ExchangeObjectVersion.Exchange2003, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400346D RID: 13421
			public static readonly SimpleProviderPropertyDefinition OnlineDiscoveryEndpoint = new SimpleProviderPropertyDefinition("OnlineDiscoveryEndpoint", ExchangeObjectVersion.Exchange2003, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400346E RID: 13422
			public static readonly SimpleProviderPropertyDefinition OnlineTargetAddress = new SimpleProviderPropertyDefinition("OnlineTargetAddress", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400346F RID: 13423
			public static readonly SimpleProviderPropertyDefinition OnPremiseTargetAddresses = new SimpleProviderPropertyDefinition("OnPremiseTargetAddresses", ExchangeObjectVersion.Exchange2003, typeof(SmtpDomain), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
