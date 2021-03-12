using System;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x0200003D RID: 61
	internal abstract class RusDataHandler
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00009758 File Offset: 0x00007958
		public RusDataHandler(string configurationDomainController, string recipientDomainController, string globalCatalog, NetworkCredential credential, PartitionId partitionId, ProvisioningCache provisioningCache, LogMessageDelegate logger)
		{
			this.configurationDomainController = configurationDomainController;
			this.recipientDomainController = recipientDomainController;
			this.globalCatalog = globalCatalog;
			this.credential = credential;
			this.partitionId = partitionId;
			this.provisioningCache = provisioningCache;
			this.logger = logger;
			this.CurrentOrganizationId = OrganizationId.ForestWideOrgId;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000097AB File Offset: 0x000079AB
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000097B3 File Offset: 0x000079B3
		protected OrganizationId CurrentOrganizationId { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000097BC File Offset: 0x000079BC
		public string ConfigurationDomainController
		{
			get
			{
				return this.configurationDomainController;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000097C4 File Offset: 0x000079C4
		public string RecipientDomainController
		{
			get
			{
				return this.recipientDomainController;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000097CC File Offset: 0x000079CC
		public string GlobalCatalog
		{
			get
			{
				return this.globalCatalog;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000097D4 File Offset: 0x000079D4
		public NetworkCredential Credential
		{
			get
			{
				return this.credential;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000097DC File Offset: 0x000079DC
		public ADObjectId RootOrgContainerId
		{
			get
			{
				if (this.rootOrgContainerId == null)
				{
					if (this.credential != null)
					{
						this.rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(this.configurationDomainController, this.credential);
					}
					else if (this.partitionId != null)
					{
						this.rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(this.partitionId.ForestFQDN, this.configurationDomainController, null);
					}
					else
					{
						this.rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
					}
				}
				return this.rootOrgContainerId;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009850 File Offset: 0x00007A50
		public ADSessionSettings SessionSettings
		{
			get
			{
				if (this.sessionSettings == null || !this.sessionSettings.CurrentOrganizationId.Equals(this.CurrentOrganizationId))
				{
					this.sessionSettings = this.GetLocalSessionSettings(this.CurrentOrganizationId);
				}
				return this.sessionSettings;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000988A File Offset: 0x00007A8A
		public ADSessionSettings GetLocalSessionSettings(OrganizationId organizationId)
		{
			if (this.partitionId != null)
			{
				return ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.partitionId);
			}
			return ADSessionSettings.FromOrganizationIdWithoutRbacScopes(this.RootOrgContainerId, organizationId, null, false);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000098B4 File Offset: 0x00007AB4
		public IConfigurationSession ConfigurationSession
		{
			get
			{
				if (this.configurationSession == null || this.ShouldChangeScope(this.configurationSession))
				{
					this.configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.configurationDomainController, true, ConsistencyMode.PartiallyConsistent, this.credential, this.SessionSettings, 194, "ConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\RusDataHandler.cs");
				}
				return this.configurationSession;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00009910 File Offset: 0x00007B10
		public ITopologyConfigurationSession RootOrgConfigurationSession
		{
			get
			{
				if (this.rootOrgConfigurationSession == null)
				{
					ADSessionSettings adsessionSettings = (this.partitionId == null) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.partitionId);
					bool flag = TaskHelper.ShouldPassDomainControllerToSession(this.configurationDomainController, adsessionSettings);
					this.rootOrgConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(flag ? this.configurationDomainController : null, true, ConsistencyMode.PartiallyConsistent, flag ? this.credential : null, adsessionSettings, 216, "RootOrgConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\RusDataHandler.cs");
				}
				return this.rootOrgConfigurationSession;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009994 File Offset: 0x00007B94
		public IRecipientSession RecipientSession
		{
			get
			{
				string domainController = this.recipientDomainController;
				if (this.recipientSession != null)
				{
					domainController = this.recipientSession.DomainController;
				}
				if (this.recipientSession == null || this.ShouldChangeScope(this.recipientSession))
				{
					this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, false, ConsistencyMode.PartiallyConsistent, this.credential, this.SessionSettings, 243, "RecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\RusDataHandler.cs");
				}
				return this.recipientSession;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009A08 File Offset: 0x00007C08
		public IRecipientSession GlobalCatalogSession
		{
			get
			{
				if (this.globalCatalogSession == null)
				{
					this.globalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.globalCatalog, true, ConsistencyMode.PartiallyConsistent, this.credential, (this.partitionId == null) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.partitionId), 263, "GlobalCatalogSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\RusDataHandler.cs");
					this.globalCatalogSession.UseGlobalCatalog = true;
				}
				return this.globalCatalogSession;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009A7C File Offset: 0x00007C7C
		public IRecipientSession GetTenantLocalRecipientSession(OrganizationId organizationId)
		{
			ADSessionSettings localSessionSettings = this.GetLocalSessionSettings(organizationId);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.recipientDomainController, false, ConsistencyMode.PartiallyConsistent, this.credential, localSessionSettings, 282, "GetTenantLocalRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\RusDataHandler.cs");
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009ABC File Offset: 0x00007CBC
		public IRecipientSession GetTenantLocalGlobalCatalogSession(OrganizationId organizationId)
		{
			ADSessionSettings localSessionSettings = this.GetLocalSessionSettings(organizationId);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.globalCatalog, true, ConsistencyMode.PartiallyConsistent, this.credential, localSessionSettings, 299, "GetTenantLocalGlobalCatalogSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\RusDataHandler.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009B02 File Offset: 0x00007D02
		public ProvisioningCache ProvisioningCache
		{
			get
			{
				return this.provisioningCache;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00009B0A File Offset: 0x00007D0A
		public LogMessageDelegate Logger
		{
			get
			{
				return this.logger;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009B12 File Offset: 0x00007D12
		private bool ShouldChangeScope(IDirectorySession session)
		{
			return this.CurrentOrganizationId != null && session.SessionSettings.CurrentOrganizationId != null && !this.CurrentOrganizationId.Equals(session.SessionSettings.CurrentOrganizationId);
		}

		// Token: 0x040000BD RID: 189
		private string configurationDomainController;

		// Token: 0x040000BE RID: 190
		private IConfigurationSession configurationSession;

		// Token: 0x040000BF RID: 191
		private ITopologyConfigurationSession rootOrgConfigurationSession;

		// Token: 0x040000C0 RID: 192
		private string recipientDomainController;

		// Token: 0x040000C1 RID: 193
		private IRecipientSession recipientSession;

		// Token: 0x040000C2 RID: 194
		private string globalCatalog;

		// Token: 0x040000C3 RID: 195
		private IRecipientSession globalCatalogSession;

		// Token: 0x040000C4 RID: 196
		private NetworkCredential credential;

		// Token: 0x040000C5 RID: 197
		private PartitionId partitionId;

		// Token: 0x040000C6 RID: 198
		private ProvisioningCache provisioningCache;

		// Token: 0x040000C7 RID: 199
		private ADSessionSettings sessionSettings;

		// Token: 0x040000C8 RID: 200
		private ADObjectId rootOrgContainerId;

		// Token: 0x040000C9 RID: 201
		private LogMessageDelegate logger;
	}
}
