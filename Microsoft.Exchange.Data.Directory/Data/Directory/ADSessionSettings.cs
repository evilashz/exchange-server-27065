using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Exchange.Data.Directory.Diagnostics;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	internal class ADSessionSettings
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0002155F File Offset: 0x0001F75F
		internal static ADServerSettings ExternalServerSettings
		{
			get
			{
				if (ADSessionSettings.threadContext != null)
				{
					return ADSessionSettings.threadContext.ServerSettings;
				}
				if (ADSessionSettings.processContext != null)
				{
					return ADSessionSettings.processContext.ServerSettings;
				}
				return null;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00021588 File Offset: 0x0001F788
		private ADSessionSettings(ScopeSet scopeSet, ADObjectId rootOrgId, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId, ConfigScopes configScopes, PartitionId partitionId)
		{
			if (scopeSet == null)
			{
				throw new ArgumentNullException("scopeSet");
			}
			if (null == currentOrganizationId)
			{
				throw new ArgumentNullException("currentOrganizationId");
			}
			if (executingUserOrganizationId != null && !executingUserOrganizationId.Equals(OrganizationId.ForestWideOrgId) && !executingUserOrganizationId.Equals(currentOrganizationId) && !currentOrganizationId.OrganizationalUnit.IsDescendantOf(executingUserOrganizationId.OrganizationalUnit))
			{
				throw new ArgumentException(DirectoryStrings.ErrorInvalidExecutingOrg(executingUserOrganizationId.OrganizationalUnit.DistinguishedName, currentOrganizationId.OrganizationalUnit.DistinguishedName));
			}
			if (partitionId == null)
			{
				throw new ArgumentNullException("partitionId");
			}
			this.scopeSet = scopeSet;
			this.preferredServers = new SimpleServerSettings();
			this.rootOrgId = rootOrgId;
			this.currentOrganizationId = currentOrganizationId;
			this.executingUserOrganizationId = executingUserOrganizationId;
			this.configScopes = configScopes;
			this.partitionId = partitionId;
			this.tenantConsistencyMode = ((configScopes == ConfigScopes.AllTenants) ? TenantConsistencyMode.IgnoreRetiredTenants : TenantConsistencyMode.ExpectOnlyLiveTenants);
			if (!ADGlobalConfigSettings.SoftLinkEnabled || this.PartitionId == null || this.PartitionId.IsLocalForestPartition() || ADSessionSettings.IsForefrontObject(this.PartitionId))
			{
				this.PartitionSoftLinkMode = SoftLinkMode.Disabled;
				return;
			}
			if (this.PartitionId.ForestFQDN.EndsWith(TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				this.PartitionSoftLinkMode = SoftLinkMode.Disabled;
				return;
			}
			if (this.ConfigScopes == ConfigScopes.Database || this.ConfigScopes == ConfigScopes.Server || this.ConfigScopes == ConfigScopes.RootOrg)
			{
				this.PartitionSoftLinkMode = SoftLinkMode.Disabled;
				return;
			}
			this.PartitionSoftLinkMode = SoftLinkMode.DualMatch;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00021700 File Offset: 0x0001F900
		internal static bool IsGlsDisabled
		{
			get
			{
				if (GlsMServDirectorySession.GlsLookupMode == GlsLookupMode.MServOnly)
				{
					return true;
				}
				ADServerSettings externalServerSettings = ADSessionSettings.ExternalServerSettings;
				return externalServerSettings != null && externalServerSettings.DisableGls;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00021728 File Offset: 0x0001F928
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00021730 File Offset: 0x0001F930
		public IBudget AccountingObject
		{
			get
			{
				return this.budget;
			}
			set
			{
				this.budget = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00021739 File Offset: 0x0001F939
		public ADScope RecipientReadScope
		{
			get
			{
				return this.scopeSet.RecipientReadScope;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00021746 File Offset: 0x0001F946
		public IList<ADScopeCollection> RecipientWriteScopes
		{
			get
			{
				return this.scopeSet.RecipientWriteScopes;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00021753 File Offset: 0x0001F953
		public ADScopeCollection ExclusiveRecipientScopes
		{
			get
			{
				return this.scopeSet.ExclusiveRecipientScopes;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00021760 File Offset: 0x0001F960
		public ADScope ConfigReadScope
		{
			get
			{
				return this.scopeSet.ConfigReadScope;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0002176D File Offset: 0x0001F96D
		public ADObjectId RootOrgId
		{
			get
			{
				if (this.rootOrgId == null)
				{
					this.rootOrgId = ADSessionSettings.SessionSettingsFactory.Default.GetRootOrgContainerId(this.partitionId);
				}
				return this.rootOrgId;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00021793 File Offset: 0x0001F993
		public OrganizationId CurrentOrganizationId
		{
			get
			{
				return this.currentOrganizationId;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0002179B File Offset: 0x0001F99B
		public OrganizationId GetCurrentOrganizationIdPopulated()
		{
			this.currentOrganizationId.EnsureFullyPopulated();
			return this.currentOrganizationId;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000217AE File Offset: 0x0001F9AE
		public OrganizationId ExecutingUserOrganizationId
		{
			get
			{
				return this.executingUserOrganizationId ?? this.currentOrganizationId;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x000217C0 File Offset: 0x0001F9C0
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x000217C8 File Offset: 0x0001F9C8
		internal string ExecutingUserIdentityName { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000217D1 File Offset: 0x0001F9D1
		public ADObjectId RecipientViewRoot
		{
			get
			{
				return this.ServerSettings.RecipientViewRoot;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x000217DE File Offset: 0x0001F9DE
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x000217E6 File Offset: 0x0001F9E6
		public SoftLinkMode PartitionSoftLinkMode { get; private set; }

		// Token: 0x0600063D RID: 1597 RVA: 0x000217EF File Offset: 0x0001F9EF
		public string GetPreferredDC(ADObjectId domain)
		{
			return this.ServerSettings.GetPreferredDC(domain);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x000217FD File Offset: 0x0001F9FD
		public bool IsGlobal
		{
			get
			{
				return this.configScopes == ConfigScopes.Global;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00021808 File Offset: 0x0001FA08
		public ConfigScopes ConfigScopes
		{
			get
			{
				return this.configScopes;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00021810 File Offset: 0x0001FA10
		public PartitionId PartitionId
		{
			get
			{
				return this.partitionId;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00021818 File Offset: 0x0001FA18
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x00021820 File Offset: 0x0001FA20
		internal bool IsSharedConfigChecked { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00021829 File Offset: 0x0001FA29
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x00021831 File Offset: 0x0001FA31
		internal bool IsRedirectedToSharedConfig { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0002183A File Offset: 0x0001FA3A
		internal bool IsTenantScoped
		{
			get
			{
				return ADSessionSettings.SessionSettingsFactory.IsTenantScopedOrganization(this.CurrentOrganizationId);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00021847 File Offset: 0x0001FA47
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0002184F File Offset: 0x0001FA4F
		internal TenantConsistencyMode TenantConsistencyMode
		{
			get
			{
				return this.tenantConsistencyMode;
			}
			set
			{
				if (value == TenantConsistencyMode.IgnoreRetiredTenants && this.configScopes == ConfigScopes.TenantLocal)
				{
					throw new InvalidOperationException("TenantConsistencyMode cannot be set to IgnoreRetiredTenants for tenant scoped session.");
				}
				this.tenantConsistencyMode = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00021870 File Offset: 0x0001FA70
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00021878 File Offset: 0x0001FA78
		internal bool RetiredTenantModificationAllowed
		{
			get
			{
				return this.retiredTenantModificationAllowed;
			}
			set
			{
				this.retiredTenantModificationAllowed = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00021881 File Offset: 0x0001FA81
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00021889 File Offset: 0x0001FA89
		public bool IncludeCNFObject
		{
			get
			{
				return this.includeCNFObject;
			}
			set
			{
				this.includeCNFObject = value;
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00021894 File Offset: 0x0001FA94
		[Conditional("DEBUG")]
		internal static void DebugCheckCaller(params string[] approvedCallerTypes)
		{
			StackTrace stackTrace = new StackTrace();
			foreach (StackFrame stackFrame in stackTrace.GetFrames())
			{
				Type declaringType = stackFrame.GetMethod().DeclaringType;
				if (approvedCallerTypes.Contains(declaringType.Name))
				{
					break;
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000218E1 File Offset: 0x0001FAE1
		public ADServerSettings ServerSettings
		{
			get
			{
				return ADSessionSettings.ExternalServerSettings ?? this.preferredServers;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000218F2 File Offset: 0x0001FAF2
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x000218FA File Offset: 0x0001FAFA
		public bool IncludeSoftDeletedObjects
		{
			get
			{
				return this.includeSoftDeletedObjects;
			}
			set
			{
				this.includeSoftDeletedObjects = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00021903 File Offset: 0x0001FB03
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0002190B File Offset: 0x0001FB0B
		public bool IncludeSoftDeletedObjectLinks
		{
			get
			{
				return this.includeSoftDeletedObjectLinks;
			}
			set
			{
				this.includeSoftDeletedObjectLinks = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00021914 File Offset: 0x0001FB14
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0002191C File Offset: 0x0001FB1C
		public bool IncludeInactiveMailbox
		{
			get
			{
				return this.includeInactiveMailbox;
			}
			set
			{
				this.includeInactiveMailbox = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00021925 File Offset: 0x0001FB25
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0002192D File Offset: 0x0001FB2D
		public bool SkipCheckVirtualIndex
		{
			get
			{
				return this.skipCheckVirtualIndex;
			}
			set
			{
				this.skipCheckVirtualIndex = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00021936 File Offset: 0x0001FB36
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0002193E File Offset: 0x0001FB3E
		public bool ForceADInTemplateScope { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00021947 File Offset: 0x0001FB47
		public ScopeSet ScopeSet
		{
			get
			{
				return this.scopeSet;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00021974 File Offset: 0x0001FB74
		internal static ADSessionSettings FromOrganizationIdWithAddressListScopeServiceOnly(OrganizationId scopingOrganizationId, ADObjectId scopingAddressListId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(delegate
			{
				ADSessionSettings.CheckIfRunningOnCmdlet();
				return ADSessionSettings.SessionSettingsFactory.Default.FromOrganizationIdWithAddressListScopeServiceOnly(scopingOrganizationId, scopingAddressListId);
			}, "FromOrganizationIdWithAddressListScopeServiceOnly");
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000219D8 File Offset: 0x0001FBD8
		internal static ADSessionSettings FromOrganizationIdWithAddressListScope(ADObjectId rootOrgId, OrganizationId scopingOrganizationId, ADObjectId scopingAddressListId, OrganizationId executingUserOrganizationId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromOrganizationIdWithAddressListScope(rootOrgId, scopingOrganizationId, scopingAddressListId, executingUserOrganizationId), "FromOrganizationIdWithAddressListScope");
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00021A38 File Offset: 0x0001FC38
		internal static ADSessionSettings FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId scopingOrganizationId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromOrganizationIdWithoutRbacScopesServiceOnly(scopingOrganizationId, true), "FromOrganizationIdWithoutRbacScopesServiceOnly");
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00021A88 File Offset: 0x0001FC88
		internal static ADSessionSettings FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId scopingOrganizationId, bool allowRehoming)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromOrganizationIdWithoutRbacScopesServiceOnly(scopingOrganizationId, allowRehoming), "FromOrganizationIdWithoutRbacScopesServiceOnly");
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00021AEC File Offset: 0x0001FCEC
		internal static ADSessionSettings FromOrganizationIdWithoutRbacScopes(ADObjectId rootOrgId, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId, bool scopeToExecutingUserOrgId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromOrganizationIdWithoutRbacScopes(rootOrgId, currentOrganizationId, executingUserOrganizationId, scopeToExecutingUserOrgId, true), "FromOrganizationIdWithoutRbacScopes");
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00021B64 File Offset: 0x0001FD64
		internal static ADSessionSettings FromOrganizationIdWithoutRbacScopes(ADObjectId rootOrgId, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId, bool scopeToExecutingUserOrgId, bool allowRehoming)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromOrganizationIdWithoutRbacScopes(rootOrgId, currentOrganizationId, executingUserOrganizationId, scopeToExecutingUserOrgId, allowRehoming), "FromOrganizationIdWithoutRbacScopes");
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00021BCC File Offset: 0x0001FDCC
		internal static ADSessionSettings FromRootOrgBootStrapSession(ADObjectId configNC)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromRootOrgBootStrapSession(configNC), "FromRootOrgBootStrapSession");
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00021C30 File Offset: 0x0001FE30
		internal static ADSessionSettings FromCustomScopeSet(ScopeSet scopeSet, ADObjectId rootOrgId, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId, bool allowRehoming = true)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromCustomScopeSet(scopeSet, rootOrgId, currentOrganizationId, executingUserOrganizationId, allowRehoming), "FromCustomScopeSet");
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00021C89 File Offset: 0x0001FE89
		internal static ADSessionSettings FromRootOrgScopeSet()
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromRootOrgScopeSet(), "FromRootOrgScopeSet");
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00021CCC File Offset: 0x0001FECC
		internal static ADSessionSettings FromAccountPartitionRootOrgScopeSet(PartitionId partitionId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromAccountPartitionRootOrgScopeSet(partitionId), "FromAccountPartitionRootOrgScopeSet");
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00021D18 File Offset: 0x0001FF18
		internal static ADSessionSettings FromAccountPartitionWideScopeSet(PartitionId partitionId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromAccountPartitionWideScopeSet(partitionId), "FromAccountPartitionWideScopeSet");
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00021D64 File Offset: 0x0001FF64
		internal static ADSessionSettings FromAllTenantsOrRootOrgAutoDetect(ADObjectId id)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromAllTenantsOrRootOrgAutoDetect(id), "FromAllTenantsOrRootOrgAutoDetect");
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00021DB0 File Offset: 0x0001FFB0
		internal static ADSessionSettings FromAllTenantsOrRootOrgAutoDetect(OrganizationId orgId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromAllTenantsOrRootOrgAutoDetect(orgId), "FromAllTenantsOrRootOrgAutoDetect");
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00021DFC File Offset: 0x0001FFFC
		internal static ADSessionSettings RescopeToSubtree(ADSessionSettings sessionSettings)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.RescopeToSubtree(sessionSettings), "RescopeToSubtree");
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00021E48 File Offset: 0x00020048
		internal static ADSessionSettings RescopeToAllTenants(ADSessionSettings sessionSettings)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.RescopeToAllTenants(sessionSettings), "RescopeToAllTenants");
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00021E94 File Offset: 0x00020094
		internal static ADSessionSettings RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(string domain)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(domain), "RootOrgOrSingleTenantFromAcceptedDomainAutoDetect");
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00021EE0 File Offset: 0x000200E0
		internal static ADSessionSettings FromTenantAcceptedDomain(string domain)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromTenantAcceptedDomain(domain), "FromTenantAcceptedDomain");
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00021F2C File Offset: 0x0002012C
		internal static ADSessionSettings FromTenantMSAUser(string msaUserNetID)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromTenantMSAUser(msaUserNetID), "FromTenantMSAUser");
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00021F88 File Offset: 0x00020188
		internal static ADSessionSettings FromConsumerOrganization()
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(delegate
			{
				ExchangeConfigurationUnit localTemplateTenant = TemplateTenantConfiguration.GetLocalTemplateTenant();
				if (localTemplateTenant == null)
				{
					throw new ADTransientException(DirectoryStrings.CannotFindTemplateTenant);
				}
				return ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(localTemplateTenant.OrganizationId);
			}, "FromConsumerOrganization");
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00021FCC File Offset: 0x000201CC
		internal static ADSessionSettings FromTenantCUName(string name)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromTenantCUName(name), "FromTenantCUName");
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00022018 File Offset: 0x00020218
		internal static ADSessionSettings FromTenantPartitionHint(TenantPartitionHint partitionHint)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromTenantPartitionHint(partitionHint), "FromTenantPartitionHint");
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00022064 File Offset: 0x00020264
		internal static ADSessionSettings FromExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId), "FromExternalDirectoryOrganizationId");
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000220B4 File Offset: 0x000202B4
		internal static ADSessionSettings FromTenantForestAndCN(string exoAccountForest, string exoTenantContainer)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromTenantForestAndCN(exoAccountForest, exoTenantContainer), "FromTenantForestAndCN");
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00022108 File Offset: 0x00020308
		internal static ADSessionSettings FromAllTenantsPartitionId(PartitionId partitionId)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromAllTenantsPartitionId(partitionId), "FromAllTenantsPartitionId");
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00022154 File Offset: 0x00020354
		internal static ADSessionSettings FromAllTenantsObjectId(ADObjectId id)
		{
			return ADSessionSettings.InvokeWithAPILogging<ADSessionSettings>(() => ADSessionSettings.SessionSettingsFactory.Default.FromAllTenantsObjectId(id), "FromAllTenantsObjectId");
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00022184 File Offset: 0x00020384
		internal static bool IsForefrontObject(PartitionId id)
		{
			return DatacenterRegistry.IsForefrontForOffice() && id.ForestFQDN == "FFO.extest.microsoft.com";
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000221A0 File Offset: 0x000203A0
		internal static bool IsForefrontObject(ADObjectId id)
		{
			return DatacenterRegistry.IsForefrontForOffice() && id.Depth >= 3 && string.Equals(id.AncestorDN(id.Depth - 3).Name, "FFO", StringComparison.OrdinalIgnoreCase) && string.Equals(id.AncestorDN(id.Depth - 2).Name, "EXTEST", StringComparison.OrdinalIgnoreCase) && string.Equals(id.AncestorDN(id.Depth - 1).Name, "MICROSOFT", StringComparison.OrdinalIgnoreCase) && string.Equals(id.AncestorDN(id.Depth).Name, "COM", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0002223E File Offset: 0x0002043E
		internal static void CheckIfRunningOnService()
		{
			if (!ADSessionSettings.IsRunningOnCmdlet())
			{
				throw new InvalidOperationException("This method should only be called from non-service code");
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00022252 File Offset: 0x00020452
		internal static ADServerSettings GetProcessServerSettings()
		{
			if (ADSessionSettings.processContext == null)
			{
				return null;
			}
			return ADSessionSettings.processContext.ServerSettings;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00022267 File Offset: 0x00020467
		internal static ADDriverContext GetProcessADContext()
		{
			return ADSessionSettings.processContext;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00022270 File Offset: 0x00020470
		internal static void SetProcessADContext(ADDriverContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.Mode != ContextMode.Setup && context.Mode != ContextMode.Test && context.Mode != ContextMode.TopologyService)
			{
				throw new ArgumentException("Only Setup,Test context and Topology Service modes are supported");
			}
			if (context.ServerSettings == null)
			{
				throw new ArgumentException("context.ServerSettings cannot be null");
			}
			if (context.Mode != ContextMode.TopologyService && TopologyProvider.CurrentTopologyMode != TopologyMode.Ldap)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionSetPreferredDCsOnlyForManagement);
			}
			ADSessionSettings.processContext = context;
			ADSessionSettings.LogEventProcessADContextChanged();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000222F0 File Offset: 0x000204F0
		internal static void ClearProcessADContext()
		{
			if (ADSessionSettings.processContext != null)
			{
				ADSessionSettings.processContext = null;
				ADSessionSettings.LogEventProcessADContextChanged();
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00022304 File Offset: 0x00020504
		internal static ADServerSettings GetThreadServerSettings()
		{
			if (ADSessionSettings.threadContext == null)
			{
				return null;
			}
			return ADSessionSettings.threadContext.ServerSettings;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00022319 File Offset: 0x00020519
		internal static ADDriverContext GetThreadADContext()
		{
			return ADSessionSettings.threadContext;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00022320 File Offset: 0x00020520
		internal static void SetThreadADContext(ADDriverContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.Mode != ContextMode.Cmdlet && context.Mode != ContextMode.Test && context.Mode != ContextMode.TopologyService)
			{
				throw new ArgumentException("Only Cmdlet, Test and Topology Service context modes are supported");
			}
			if (context.ServerSettings == null)
			{
				throw new ArgumentException("context.ServerSettings cannot be null");
			}
			ADSessionSettings.threadContext = context;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0002237A File Offset: 0x0002057A
		internal static void ClearThreadADContext()
		{
			if (ADSessionSettings.threadContext != null)
			{
				ADSessionSettings.threadContext = null;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0002238C File Offset: 0x0002058C
		internal static void CloneSettableProperties(ADSessionSettings oldSessionSettings, ADSessionSettings newSessionSettings)
		{
			newSessionSettings.IsSharedConfigChecked = oldSessionSettings.IsSharedConfigChecked;
			newSessionSettings.IsRedirectedToSharedConfig = oldSessionSettings.IsRedirectedToSharedConfig;
			newSessionSettings.RetiredTenantModificationAllowed = oldSessionSettings.RetiredTenantModificationAllowed;
			newSessionSettings.IncludeInactiveMailbox = oldSessionSettings.IncludeInactiveMailbox;
			newSessionSettings.IncludeSoftDeletedObjectLinks = oldSessionSettings.IncludeSoftDeletedObjectLinks;
			newSessionSettings.IncludeSoftDeletedObjects = oldSessionSettings.IncludeSoftDeletedObjects;
			if (newSessionSettings.ConfigScopes != ConfigScopes.TenantLocal || oldSessionSettings.TenantConsistencyMode != TenantConsistencyMode.IgnoreRetiredTenants)
			{
				newSessionSettings.TenantConsistencyMode = oldSessionSettings.TenantConsistencyMode;
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000223FF File Offset: 0x000205FF
		internal static ADSessionSettings RescopeToOrganization(ADSessionSettings sessionSettings, OrganizationId orgId, bool rehomeDataSession = true)
		{
			return ADSessionSettings.RescopeToOrganization(sessionSettings, orgId, true, rehomeDataSession);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0002240C File Offset: 0x0002060C
		internal static ADSessionSettings RescopeToOrganization(ADSessionSettings sessionSettings, OrganizationId orgId, bool checkOrgScope, bool rehomeDataSession = true)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			if (orgId != null && orgId.OrganizationalUnit == null && sessionSettings.RecipientReadScope.Root == null)
			{
				return sessionSettings;
			}
			if (sessionSettings.CurrentOrganizationId != null && sessionSettings.CurrentOrganizationId.Equals(orgId))
			{
				return sessionSettings;
			}
			ScopeSet scopeSet = ScopeSet.ResolveUnderScope(orgId, sessionSettings.ScopeSet, checkOrgScope);
			ADSessionSettings adsessionSettings = ADSessionSettings.FromCustomScopeSet(scopeSet, sessionSettings.RootOrgId, orgId, checkOrgScope ? sessionSettings.ExecutingUserOrganizationId : OrganizationId.ForestWideOrgId, rehomeDataSession);
			ADSessionSettings.CloneSettableProperties(sessionSettings, adsessionSettings);
			return adsessionSettings;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0002249A File Offset: 0x0002069A
		internal string GetAccountOrResourceForestFqdn()
		{
			if (this.PartitionId != null && !ADSessionSettings.IsForefrontObject(this.PartitionId))
			{
				return this.PartitionId.ForestFQDN;
			}
			return TopologyProvider.LocalForestFqdn;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000224C8 File Offset: 0x000206C8
		private static void CheckIfRunningOnCmdlet()
		{
			if (ADSessionSettings.IsRunningOnCmdlet())
			{
				throw new InvalidOperationException("This method should never be called from non-service code");
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000224DC File Offset: 0x000206DC
		internal static bool IsRunningOnCmdlet()
		{
			return (ADSessionSettings.processContext != null && ADSessionSettings.processContext.Mode == ContextMode.Setup) || (ADSessionSettings.threadContext != null && ADSessionSettings.threadContext.Mode == ContextMode.Cmdlet);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002250C File Offset: 0x0002070C
		private static void LogEventProcessADContextChanged()
		{
			string text = "<null>";
			string text2 = text;
			string text3 = text;
			string text4 = text;
			if (ADSessionSettings.processContext != null)
			{
				ADServerSettings serverSettings = ADSessionSettings.processContext.ServerSettings;
				string text5 = serverSettings.PreferredGlobalCatalog(TopologyProvider.LocalForestFqdn);
				if (!string.IsNullOrEmpty(text5))
				{
					text3 = text5;
				}
				text5 = serverSettings.ConfigurationDomainController(TopologyProvider.LocalForestFqdn);
				if (!string.IsNullOrEmpty(text5))
				{
					text4 = text5;
				}
				if (serverSettings.PreferredDomainControllers.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (Fqdn fqdn in serverSettings.PreferredDomainControllers)
					{
						stringBuilder.AppendLine(fqdn);
					}
					text2 = stringBuilder.ToString();
				}
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_PREFERRED_TOPOLOGY, null, new object[]
			{
				text2,
				text3,
				text4
			});
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00022628 File Offset: 0x00020828
		private static T InvokeWithAPILogging<T>(Func<T> action, [CallerMemberName] string memberName = null) where T : ADSessionSettings
		{
			Func<string> func = null;
			if (ADSessionSettings.SessionSettingsFactory.Default is ADSessionSettingsFactory)
			{
				DateTime utcNow = DateTime.UtcNow;
				Guid activityId = default(Guid);
				string className = ADSessionSettings.ClassName;
				string caller = "";
				Func<T> action2 = () => action();
				if (func == null)
				{
					func = (() => null);
				}
				return ADScenarioLog.InvokeWithAPILog<T>(utcNow, memberName, activityId, className, caller, action2, func);
			}
			return action();
		}

		// Token: 0x04000274 RID: 628
		private static string ClassName = "ADSessionSettings";

		// Token: 0x04000275 RID: 629
		[ThreadStatic]
		private static ADDriverContext threadContext = null;

		// Token: 0x04000276 RID: 630
		private static ADDriverContext processContext;

		// Token: 0x04000277 RID: 631
		private ScopeSet scopeSet;

		// Token: 0x04000278 RID: 632
		private ADServerSettings preferredServers;

		// Token: 0x04000279 RID: 633
		private ADObjectId rootOrgId;

		// Token: 0x0400027A RID: 634
		private OrganizationId currentOrganizationId;

		// Token: 0x0400027B RID: 635
		private OrganizationId executingUserOrganizationId;

		// Token: 0x0400027C RID: 636
		private ConfigScopes configScopes;

		// Token: 0x0400027D RID: 637
		private bool includeSoftDeletedObjects;

		// Token: 0x0400027E RID: 638
		private bool includeSoftDeletedObjectLinks;

		// Token: 0x0400027F RID: 639
		private bool includeInactiveMailbox;

		// Token: 0x04000280 RID: 640
		private PartitionId partitionId;

		// Token: 0x04000281 RID: 641
		private bool skipCheckVirtualIndex;

		// Token: 0x04000282 RID: 642
		private TenantConsistencyMode tenantConsistencyMode;

		// Token: 0x04000283 RID: 643
		private bool retiredTenantModificationAllowed;

		// Token: 0x04000284 RID: 644
		private bool includeCNFObject = true;

		// Token: 0x04000285 RID: 645
		[NonSerialized]
		private IBudget budget;

		// Token: 0x02000081 RID: 129
		internal abstract class SessionSettingsFactory
		{
			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000689 RID: 1673 RVA: 0x000226B0 File Offset: 0x000208B0
			public static ADSessionSettings.SessionSettingsFactory Default
			{
				get
				{
					if (ADSessionSettings.SessionSettingsFactory.defaultInstance == null)
					{
						ADSessionSettings.SessionSettingsFactory.defaultInstance = (ADSessionSettings.SessionSettingsFactory)DirectorySessionFactory.InstantiateFfoOrExoClass("Microsoft.Exchange.Hygiene.Data.Directory.FfoSessionSettingsFactory", typeof(ADSessionSettingsFactory));
					}
					return ADSessionSettings.SessionSettingsFactory.defaultInstance;
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x0600068A RID: 1674 RVA: 0x000226DC File Offset: 0x000208DC
			// (set) Token: 0x0600068B RID: 1675 RVA: 0x000226E4 File Offset: 0x000208E4
			internal static ADSessionSettings.SessionSettingsFactory.PostActionForSettings ThreadPostActionForSettings
			{
				get
				{
					return ADSessionSettings.SessionSettingsFactory.threadPostActionForSettings;
				}
				set
				{
					ExTraceGlobals.SessionSettingsTracer.TraceInformation(0, 0L, string.Format("ThreadPostActionForSettings is set, prevous is {0}, changed to {1}.", (ADSessionSettings.SessionSettingsFactory.threadPostActionForSettings == null) ? "null" : ADSessionSettings.SessionSettingsFactory.threadPostActionForSettings.ToString(), (value == null) ? "null" : value.ToString()));
					ADSessionSettings.SessionSettingsFactory.threadPostActionForSettings = value;
				}
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x00022736 File Offset: 0x00020936
			internal static bool IsTenantScopedOrganization(OrganizationId organizationId)
			{
				if (organizationId == null)
				{
					throw new ArgumentNullException("organizationId");
				}
				return organizationId.OrganizationalUnit != null;
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x00022758 File Offset: 0x00020958
			internal virtual ADSessionSettings FromAccountPartitionRootOrgScopeSet(PartitionId partitionId)
			{
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ADSessionSettings.SessionSettingsFactory.GlobalScopeSet, null, OrganizationId.ForestWideOrgId, null, ConfigScopes.RootOrg, partitionId);
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0002276D File Offset: 0x0002096D
			internal virtual ADSessionSettings FromAccountPartitionWideScopeSet(PartitionId partitionId)
			{
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ADSessionSettings.SessionSettingsFactory.GlobalScopeSet, null, OrganizationId.ForestWideOrgId, null, ConfigScopes.Global, partitionId);
			}

			// Token: 0x0600068F RID: 1679
			internal abstract ADSessionSettings FromAllTenantsPartitionId(PartitionId partitionId);

			// Token: 0x06000690 RID: 1680
			internal abstract ADSessionSettings FromTenantPartitionHint(TenantPartitionHint partitionHint);

			// Token: 0x06000691 RID: 1681 RVA: 0x00022782 File Offset: 0x00020982
			internal virtual ADObjectId GetRootOrgContainerId(PartitionId partitionId)
			{
				if (partitionId == null)
				{
					throw new ArgumentNullException("partitionId");
				}
				if (ADSession.IsBoundToAdam || partitionId.IsLocalForestPartition())
				{
					return ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
				}
				return ADSystemConfigurationSession.GetRootOrgContainerId(partitionId.ForestFQDN, null, null);
			}

			// Token: 0x06000692 RID: 1682
			internal abstract ADSessionSettings FromAllTenantsObjectId(ADObjectId id);

			// Token: 0x06000693 RID: 1683
			internal abstract ADSessionSettings FromAllTenantsOrRootOrgAutoDetect(ADObjectId id);

			// Token: 0x06000694 RID: 1684 RVA: 0x000227BA File Offset: 0x000209BA
			internal virtual ADSessionSettings FromRootOrgBootStrapSession(ADObjectId configNC)
			{
				if (configNC == null)
				{
					throw new ArgumentNullException("configNC");
				}
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ADSessionSettings.SessionSettingsFactory.GlobalScopeSet, configNC, OrganizationId.ForestWideOrgId, null, ConfigScopes.RootOrg, TopologyProvider.IsAdamTopology() ? PartitionId.LocalForest : configNC.GetPartitionId());
			}

			// Token: 0x06000695 RID: 1685 RVA: 0x000227F0 File Offset: 0x000209F0
			internal ADSessionSettings FromCustomScopeSet(ScopeSet scopeSet, ADObjectId rootOrgId, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId, bool allowRehoming = true)
			{
				if (rootOrgId == null)
				{
					throw new ArgumentNullException("rootOrgId");
				}
				ConfigScopes configScopes = ConfigScopes.TenantLocal;
				if (allowRehoming)
				{
					currentOrganizationId = this.RehomeScopingOrganizationIdIfNeeded(currentOrganizationId);
					executingUserOrganizationId = this.RehomeScopingOrganizationIdIfNeeded(executingUserOrganizationId);
				}
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(scopeSet, rootOrgId, currentOrganizationId, executingUserOrganizationId, configScopes, currentOrganizationId.PartitionId);
			}

			// Token: 0x06000696 RID: 1686
			internal abstract ADSessionSettings FromAllTenantsOrRootOrgAutoDetect(OrganizationId orgId);

			// Token: 0x06000697 RID: 1687 RVA: 0x00022838 File Offset: 0x00020A38
			internal virtual ADSessionSettings FromOrganizationIdWithAddressListScopeServiceOnly(OrganizationId scopingOrganizationId, ADObjectId scopingAddressListId)
			{
				QueryFilter recipientReadFilter;
				if (scopingAddressListId == null)
				{
					recipientReadFilter = ADScope.NoObjectFilter;
				}
				else
				{
					recipientReadFilter = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, scopingAddressListId),
						new ExistsFilter(ADRecipientSchema.DisplayName)
					});
				}
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ScopeSet.GetOrgWideDefaultScopeSet(scopingOrganizationId, recipientReadFilter), null, scopingOrganizationId, null, ConfigScopes.TenantLocal, scopingOrganizationId.PartitionId);
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x00022894 File Offset: 0x00020A94
			internal ADSessionSettings FromOrganizationIdWithAddressListScope(ADObjectId rootOrgId, OrganizationId scopingOrganizationId, ADObjectId scopingAddressListId, OrganizationId executingUserOrganizationId)
			{
				ArgumentValidator.ThrowIfNull("scopingAddressListId", scopingAddressListId);
				QueryFilter recipientReadFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, scopingAddressListId),
					new ExistsFilter(ADRecipientSchema.DisplayName)
				});
				return this.FromCustomScopeSet(ScopeSet.GetOrgWideDefaultScopeSet(scopingOrganizationId, recipientReadFilter), rootOrgId, scopingOrganizationId, executingUserOrganizationId, true);
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x000228E8 File Offset: 0x00020AE8
			internal virtual ADSessionSettings FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId scopingOrganizationId, bool allowRehoming)
			{
				if (allowRehoming)
				{
					scopingOrganizationId = this.RehomeScopingOrganizationIdIfNeeded(scopingOrganizationId);
				}
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ScopeSet.GetOrgWideDefaultScopeSet(scopingOrganizationId, null), null, scopingOrganizationId, null, ConfigScopes.TenantLocal, scopingOrganizationId.PartitionId);
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0002290C File Offset: 0x00020B0C
			internal virtual ADSessionSettings FromOrganizationIdWithoutRbacScopes(ADObjectId rootOrgId, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId, bool scopeToExecutingUserOrgId, bool allowRehoming)
			{
				if (rootOrgId == null)
				{
					throw new ArgumentNullException("rootOrgId");
				}
				if (null == currentOrganizationId)
				{
					throw new ArgumentNullException("currentOrganizationId");
				}
				if (scopeToExecutingUserOrgId && executingUserOrganizationId == null)
				{
					throw new ArgumentException("scopeToExecutingUserOrgId + null executingUserOrganizationId");
				}
				if (allowRehoming)
				{
					currentOrganizationId = this.RehomeScopingOrganizationIdIfNeeded(currentOrganizationId);
					executingUserOrganizationId = this.RehomeScopingOrganizationIdIfNeeded(executingUserOrganizationId);
				}
				OrganizationId organizationId = currentOrganizationId;
				if (scopeToExecutingUserOrgId)
				{
					organizationId = executingUserOrganizationId;
				}
				ScopeSet orgWideDefaultScopeSet = ScopeSet.GetOrgWideDefaultScopeSet(organizationId);
				ConfigScopes configScopes = ConfigScopes.TenantLocal;
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(orgWideDefaultScopeSet, rootOrgId, currentOrganizationId, executingUserOrganizationId, configScopes, (currentOrganizationId.PartitionId != null) ? currentOrganizationId.PartitionId : (Globals.IsMicrosoftHostedOnly ? rootOrgId.GetPartitionId() : null));
			}

			// Token: 0x0600069B RID: 1691
			internal abstract ADSessionSettings FromExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId);

			// Token: 0x0600069C RID: 1692
			internal abstract ADSessionSettings FromTenantForestAndCN(string exoAccountForest, string exoTenantContainer);

			// Token: 0x0600069D RID: 1693 RVA: 0x000229AC File Offset: 0x00020BAC
			internal virtual ADSessionSettings RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(string domain)
			{
				if (!Globals.IsDatacenter)
				{
					return ADSessionSettings.FromRootOrgScopeSet();
				}
				ADSessionSettings result;
				try
				{
					result = ADSessionSettings.FromTenantAcceptedDomain(domain);
				}
				catch (CannotResolveTenantNameException)
				{
					result = ADSessionSettings.FromRootOrgScopeSet();
				}
				return result;
			}

			// Token: 0x0600069E RID: 1694
			internal abstract ADSessionSettings FromTenantAcceptedDomain(string domain);

			// Token: 0x0600069F RID: 1695
			internal abstract ADSessionSettings FromTenantMSAUser(string msaUserNetID);

			// Token: 0x060006A0 RID: 1696
			internal abstract ADSessionSettings FromTenantCUName(string name);

			// Token: 0x060006A1 RID: 1697 RVA: 0x000229EC File Offset: 0x00020BEC
			internal virtual ADSessionSettings FromRootOrgScopeSet()
			{
				return ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(ADSessionSettings.SessionSettingsFactory.GlobalScopeSet, null, OrganizationId.ForestWideOrgId, null, ConfigScopes.RootOrg, PartitionId.LocalForest);
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x00022A08 File Offset: 0x00020C08
			internal virtual ADSessionSettings RescopeToSubtree(ADSessionSettings sessionSettings)
			{
				if (sessionSettings == null)
				{
					throw new ArgumentNullException("sessionSettings");
				}
				if (sessionSettings.ConfigScopes != ConfigScopes.TenantLocal)
				{
					throw new ArgumentException(DirectoryStrings.ErrorInvalidConfigScope(sessionSettings.ConfigScopes.ToString()));
				}
				ADSessionSettings adsessionSettings = ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(sessionSettings.ScopeSet, sessionSettings.RootOrgId, sessionSettings.CurrentOrganizationId, sessionSettings.ExecutingUserOrganizationId, ConfigScopes.TenantSubTree, sessionSettings.PartitionId);
				adsessionSettings.IncludeSoftDeletedObjects = sessionSettings.IncludeSoftDeletedObjects;
				adsessionSettings.IncludeSoftDeletedObjectLinks = sessionSettings.IncludeSoftDeletedObjectLinks;
				adsessionSettings.IncludeInactiveMailbox = sessionSettings.IncludeInactiveMailbox;
				return adsessionSettings;
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x00022A98 File Offset: 0x00020C98
			internal virtual ADSessionSettings RescopeToAllTenants(ADSessionSettings sessionSettings)
			{
				if (sessionSettings == null)
				{
					throw new ArgumentNullException("sessionSettings");
				}
				ADSessionSettings adsessionSettings = ADSessionSettings.SessionSettingsFactory.CreateADSessionSettings(sessionSettings.ScopeSet, sessionSettings.RootOrgId, sessionSettings.CurrentOrganizationId, sessionSettings.ExecutingUserOrganizationId, ConfigScopes.AllTenants, sessionSettings.PartitionId);
				adsessionSettings.IncludeSoftDeletedObjects = sessionSettings.IncludeSoftDeletedObjects;
				adsessionSettings.IncludeSoftDeletedObjectLinks = sessionSettings.IncludeSoftDeletedObjectLinks;
				adsessionSettings.IncludeInactiveMailbox = sessionSettings.IncludeInactiveMailbox;
				return adsessionSettings;
			}

			// Token: 0x060006A4 RID: 1700
			internal abstract bool InDomain();

			// Token: 0x060006A5 RID: 1701 RVA: 0x00022B00 File Offset: 0x00020D00
			protected static ADSessionSettings CreateADSessionSettings(ScopeSet scopeSet, ADObjectId rootOrgId, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId, ConfigScopes configScopes, PartitionId partitionId)
			{
				ADSessionSettings adsessionSettings = new ADSessionSettings(scopeSet, rootOrgId, currentOrganizationId, executingUserOrganizationId, configScopes, partitionId);
				if (ADSessionSettings.SessionSettingsFactory.ThreadPostActionForSettings == null)
				{
					return adsessionSettings;
				}
				return ADSessionSettings.SessionSettingsFactory.ThreadPostActionForSettings(adsessionSettings);
			}

			// Token: 0x060006A6 RID: 1702
			protected abstract OrganizationId RehomeScopingOrganizationIdIfNeeded(OrganizationId currentOrganizationId);

			// Token: 0x060006A7 RID: 1703 RVA: 0x00022B30 File Offset: 0x00020D30
			[Conditional("DEBUG")]
			private static void CheckCallStackForBootstrapSession()
			{
				string stackTrace = Environment.StackTrace;
				if (!stackTrace.Contains("Microsoft.Exchange.Data.Directory.SystemConfiguration.ADSystemConfigurationSession"))
				{
					stackTrace.Contains("Microsoft.Exchange.Directory.TopologyService");
				}
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x00022B5C File Offset: 0x00020D5C
			[Conditional("DEBUG")]
			private static void CheckProcessNameForTopologyServiceSession()
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					if (!currentProcess.MainModule.ModuleName.Equals("Microsoft.Exchange.Directory.TopologyService.exe", StringComparison.OrdinalIgnoreCase))
					{
						currentProcess.MainModule.ModuleName.Equals("PerseusStudio.exe", StringComparison.OrdinalIgnoreCase);
					}
				}
			}

			// Token: 0x0400028D RID: 653
			protected static readonly ScopeSet GlobalScopeSet = ScopeSet.GetOrgWideDefaultScopeSet(OrganizationId.ForestWideOrgId);

			// Token: 0x0400028E RID: 654
			private static ADSessionSettings.SessionSettingsFactory defaultInstance;

			// Token: 0x0400028F RID: 655
			[ThreadStatic]
			private static ADSessionSettings.SessionSettingsFactory.PostActionForSettings threadPostActionForSettings;

			// Token: 0x02000082 RID: 130
			// (Invoke) Token: 0x060006AC RID: 1708
			internal delegate ADSessionSettings PostActionForSettings(ADSessionSettings settings);
		}
	}
}
