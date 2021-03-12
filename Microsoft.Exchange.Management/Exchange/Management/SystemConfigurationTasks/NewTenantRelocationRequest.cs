using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000692 RID: 1682
	[Cmdlet("New", "TenantRelocationRequest", SupportsShouldProcess = true)]
	public sealed class NewTenantRelocationRequest : SetSystemConfigurationObjectTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x000FD8EE File Offset: 0x000FBAEE
		// (set) Token: 0x06003B95 RID: 15253 RVA: 0x000FD905 File Offset: 0x000FBB05
		[Parameter(Mandatory = true)]
		public AccountPartitionIdParameter TargetAccountPartition
		{
			get
			{
				return (AccountPartitionIdParameter)base.Fields[TenantRelocationRequestSchema.TargetForest];
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.TargetForest] = value;
			}
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000FD918 File Offset: 0x000FBB18
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x000FD92F File Offset: 0x000FBB2F
		[Parameter(Mandatory = false)]
		public Schedule SafeLockdownSchedule
		{
			get
			{
				return (Schedule)base.Fields[TenantRelocationRequestSchema.SafeLockdownSchedule];
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.SafeLockdownSchedule] = value;
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x000FD942 File Offset: 0x000FBB42
		// (set) Token: 0x06003B99 RID: 15257 RVA: 0x000FD963 File Offset: 0x000FBB63
		[Parameter(Mandatory = false)]
		public bool AutoCompletionEnabled
		{
			get
			{
				return (bool)(base.Fields[TenantRelocationRequestSchema.AutoCompletionEnabled] ?? false);
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.AutoCompletionEnabled] = value;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x000FD97B File Offset: 0x000FBB7B
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x000FD99C File Offset: 0x000FBB9C
		[Parameter(Mandatory = false)]
		public bool LargeTenantModeEnabled
		{
			get
			{
				return (bool)(base.Fields[TenantRelocationRequestSchema.LargeTenantModeEnabled] ?? false);
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.LargeTenantModeEnabled] = value;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x000FD9B4 File Offset: 0x000FBBB4
		// (set) Token: 0x06003B9D RID: 15261 RVA: 0x000FD9CB File Offset: 0x000FBBCB
		[Parameter(Mandatory = false)]
		public RelocationStateRequestedByCmdlet RelocationStateRequested
		{
			get
			{
				return (RelocationStateRequestedByCmdlet)base.Fields[TenantRelocationRequestSchema.RelocationStateRequested];
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.RelocationStateRequested] = value;
			}
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x000FD9E4 File Offset: 0x000FBBE4
		protected override void ResolveCurrentOrgIdBasedOnIdentity(IIdentityParameter identity)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 107, "ResolveCurrentOrgIdBasedOnIdentity", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Relocation\\NewTenantRelocationRequest.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Identity, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Identity.ToString())));
			base.SetCurrentOrganizationWithScopeSet(adorganizationalUnit.OrganizationId);
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x000FDA79 File Offset: 0x000FBC79
		public override object GetDynamicParameters()
		{
			return null;
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000FDA7C File Offset: 0x000FBC7C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewTenantRelocationRequest(this.Identity.ToString());
			}
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000FDA90 File Offset: 0x000FBC90
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!string.IsNullOrEmpty(this.DataObject.TargetForest) || !string.IsNullOrEmpty(this.DataObject.RelocationSourceForestRaw))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTenantAlreadyBeingRelocated(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if ((this.DataObject.OrganizationStatus != OrganizationStatus.Active && this.DataObject.OrganizationStatus != OrganizationStatus.Suspended && this.DataObject.OrganizationStatus != OrganizationStatus.LockedOut) || this.DataObject.IsUpdatingServicePlan)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTenantNotInActiveOrgState(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (this.DataObject.EnableAsSharedConfiguration || this.DataObject.ImmutableConfiguration)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorSCTsCannotBeMigrated(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (this.DataObject.AdminDisplayVersion.ExchangeBuild < ExchangeObjectVersion.Exchange2012.ExchangeBuild)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorOldTenantsCannotBeMigrated(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			this.sourceAccountPartitionId = this.DataObject.OrganizationId.PartitionId;
			this.sourceForestFqdn = this.sourceAccountPartitionId.ForestFQDN;
			Organization rootOrgContainer = ADSystemConfigurationSession.GetRootOrgContainer(this.sourceForestFqdn, null, null);
			if (!rootOrgContainer.TenantRelocationsAllowed)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTenantRelocationNotAllowed(this.Identity.ToString(), this.sourceForestFqdn)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (TopologyProvider.LocalForestFqdn.Equals(this.TargetAccountPartition.RawIdentity, StringComparison.OrdinalIgnoreCase))
			{
				this.targetAccountPartitionFqdn = PartitionId.LocalForest.ForestFQDN;
			}
			else
			{
				AccountPartition accountPartition = (AccountPartition)base.GetDataObject<AccountPartition>(this.TargetAccountPartition, DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.SessionSettingsFactory.Default.FromRootOrgScopeSet(), 207, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Relocation\\NewTenantRelocationRequest.cs"), null, null, null);
				this.targetAccountPartitionFqdn = accountPartition.PartitionId.ForestFQDN;
			}
			if (this.targetAccountPartitionFqdn.IndexOf(PartitionId.LocalForest.ForestFQDN, StringComparison.InvariantCultureIgnoreCase) > 0)
			{
				this.targetAccountPartitionFqdn = PartitionId.LocalForest.ForestFQDN;
			}
			rootOrgContainer = ADSystemConfigurationSession.GetRootOrgContainer(this.targetAccountPartitionFqdn, null, null);
			if (!rootOrgContainer.TenantRelocationsAllowed)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTenantRelocationNotAllowed(this.Identity.ToString(), this.targetAccountPartitionFqdn)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (!base.Fields.IsModified(TenantRelocationRequestSchema.AutoCompletionEnabled))
			{
				this.AutoCompletionEnabled = false;
			}
			if (this.AutoCompletionEnabled && base.Fields.IsModified(TenantRelocationRequestSchema.RelocationStateRequested))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorRelocationStateRequestedIsNotAllowed(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (!this.AutoCompletionEnabled && !base.Fields.IsModified(TenantRelocationRequestSchema.RelocationStateRequested))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorRelocationStateRequestedIsMandatory(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			ADUser[] array = OrganizationMailbox.FindByOrganizationId(this.DataObject.OrganizationId, OrganizationCapability.Management);
			if (array.Length != 1)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorOneManagementOrgMailboxIsRequired(this.Identity.ToString(), array.Length.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			TenantOrganizationPresentationObject tenantOrganizationPresentationObject = new TenantOrganizationPresentationObject(this.DataObject);
			DateTime utcNow = DateTime.UtcNow;
			bool config = TenantRelocationConfigImpl.GetConfig<bool>("IgnoreRelocationConstraintExpiration");
			foreach (RelocationConstraint relocationConstraint in tenantOrganizationPresentationObject.RelocationConstraints)
			{
				if (config || relocationConstraint.ExpirationDate > utcNow)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorRelocationConstraintsPresent(this.Identity.ToString(), relocationConstraint.Name)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
			}
			this.externalDirectoryOrganizationId = Guid.Parse(this.DataObject.ExternalDirectoryOrganizationId);
			if (!NewTenantRelocationRequest.GLSRecordCheckDisabled() && GlsMServDirectorySession.GlsLookupMode != GlsLookupMode.MServOnly)
			{
				string text;
				string text2;
				string text3;
				Exception ex;
				bool flag = GetTenantRelocationRequest.TryGlsLookupByExternalDirectoryOrganizationId(this.externalDirectoryOrganizationId, out text, out text2, out text3, out ex);
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				if (!flag || string.IsNullOrEmpty(text2) || !text2.Equals(this.sourceForestFqdn, StringComparison.InvariantCultureIgnoreCase))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorUnexpectedAccountForestValueInGls(this.Identity.ToString(), text2, this.sourceForestFqdn)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000FDFA0 File Offset: 0x000FC1A0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.DataObject.AutoCompletionEnabled = this.AutoCompletionEnabled;
			this.DataObject.LargeTenantModeEnabled = this.LargeTenantModeEnabled;
			this.DataObject.SafeLockdownSchedule = (this.SafeLockdownSchedule ?? Schedule.Always);
			if (base.Fields.IsModified(TenantRelocationRequestSchema.RelocationStateRequested))
			{
				this.DataObject.RelocationStateRequested = (RelocationStateRequested)this.RelocationStateRequested;
			}
			this.DataObject.TargetForest = this.targetAccountPartitionFqdn;
			this.sourceForestRIDMaster = ForestTenantRelocationsCache.GetRidMasterName(this.sourceAccountPartitionId);
			DirectorySessionFactory @default = DirectorySessionFactory.Default;
			Fqdn domainController = base.DomainController;
			ITenantConfigurationSession tenantConfigurationSession = @default.CreateTenantConfigurationSession((domainController != null) ? domainController : this.sourceForestRIDMaster, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAllTenantsPartitionId(this.sourceAccountPartitionId), 334, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Relocation\\NewTenantRelocationRequest.cs");
			PartitionId partitionId = new PartitionId(this.targetAccountPartitionFqdn);
			this.targetForestRIDMaster = ForestTenantRelocationsCache.GetRidMasterName(partitionId);
			ITenantConfigurationSession tenantConfigurationSession2 = DirectorySessionFactory.Default.CreateTenantConfigurationSession(this.targetForestRIDMaster, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 343, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Relocation\\NewTenantRelocationRequest.cs");
			tenantConfigurationSession2.SessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			MsoTenantCookieContainer msoTenantCookieContainer = tenantConfigurationSession2.GetMsoTenantCookieContainer(this.externalDirectoryOrganizationId);
			if (msoTenantCookieContainer != null && !NewTenantRelocationRequest.IntraForestRelocationEnabled())
			{
				InvalidOperationException exception = new InvalidOperationException(Strings.ErrorTargetPartitionHasTenantWithSameId(this.DataObject.DistinguishedName, this.targetAccountPartitionFqdn, msoTenantCookieContainer.DistinguishedName, this.DataObject.ExternalDirectoryOrganizationId));
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			string text = this.GetInitialDomainName(tenantConfigurationSession, this.DataObject.OrganizationId);
			ExchangeConfigurationUnit exchangeConfigurationUnitByName = tenantConfigurationSession2.GetExchangeConfigurationUnitByName(text);
			if (exchangeConfigurationUnitByName != null)
			{
				if (text.Length > 50)
				{
					text = text.Substring(0, 50);
				}
				text = string.Format("{0}-RELO-{1}", text, Guid.NewGuid().ToString());
				if (text.Length > 64)
				{
					text = text.Substring(0, 64);
				}
			}
			Exception ex;
			OrganizationId targetSharedOrgId = SharedConfiguration.FindMostRecentSharedConfigurationInPartition(this.DataObject.OrganizationId, partitionId, out ex);
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			ADOrganizationalUnit adorganizationalUnit = null;
			bool useConfigNC = tenantConfigurationSession.UseConfigNC;
			try
			{
				tenantConfigurationSession.UseConfigNC = false;
				adorganizationalUnit = tenantConfigurationSession.Read<ADOrganizationalUnit>(this.DataObject.OrganizationId.OrganizationalUnit);
			}
			finally
			{
				tenantConfigurationSession.UseConfigNC = useConfigNC;
			}
			this.DataObject.RelocationStatusDetailsRaw = RelocationStatusDetails.InitializationStarted;
			base.InternalProcessRecord();
			ADObjectId enclosureContainerId = this.CreateEnclosureConfigContainer(text, tenantConfigurationSession2);
			ExchangeConfigurationUnit exchangeConfigurationUnit = this.CreateOrgConfigurationContainer(enclosureContainerId, targetSharedOrgId, this.DataObject.ExternalDirectoryOrganizationId, this.DataObject.ProgramId, this.DataObject.OfferId, this.DataObject.IsDehydrated, this.DataObject.IsStaticConfigurationShared, this.sourceForestFqdn, this.DataObject.Guid, this.DataObject.ExchangeObjectId, tenantConfigurationSession2);
			ADObjectId organizationalUnitLink = this.CreateOrganizationUnitContainer(text, adorganizationalUnit.Guid, adorganizationalUnit.ExchangeObjectId, tenantConfigurationSession2);
			exchangeConfigurationUnit.OrganizationalUnitLink = organizationalUnitLink;
			exchangeConfigurationUnit.RelocationStatusDetailsRaw = RelocationStatusDetails.InitializationFinished;
			tenantConfigurationSession2.Save(exchangeConfigurationUnit);
			this.DataObject.RelocationStatusDetailsRaw = RelocationStatusDetails.InitializationFinished;
			base.DataSession.Save(this.DataObject);
			TenantRelocationRequest tenantRelocationRequest = (TenantRelocationRequest)base.DataSession.Read<TenantRelocationRequest>(this.DataObject.Identity);
			TenantRelocationRequest targetForestObject = tenantConfigurationSession2.Read<TenantRelocationRequest>(exchangeConfigurationUnit.Id);
			TenantRelocationRequest.PopulatePresentationObject(tenantRelocationRequest, targetForestObject);
			GetTenantRelocationRequest.PopulateGlsProperty(tenantRelocationRequest, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			GetTenantRelocationRequest.PopulateRidMasterProperties(tenantRelocationRequest, this.sourceForestRIDMaster, this.targetForestRIDMaster, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			if (tenantRelocationRequest.OriginatingServer != this.sourceForestRIDMaster)
			{
				this.WriteWarning(Strings.WarningShouldWriteToRidMaster(tenantRelocationRequest.OriginatingServer, this.sourceForestRIDMaster));
			}
			base.WriteObject(tenantRelocationRequest);
			TaskLogger.LogExit();
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000FE370 File Offset: 0x000FC570
		private ADObjectId CreateOrganizationUnitContainer(string tenantContainerName, Guid sourceObjectGuid, Guid sourceObjectExchangeObjectId, ITenantConfigurationSession targetForestTenantSession)
		{
			ADOrganizationalUnit adorganizationalUnit = new ADOrganizationalUnit();
			adorganizationalUnit.CorrelationId = sourceObjectGuid;
			adorganizationalUnit.ExchangeObjectId = sourceObjectExchangeObjectId;
			adorganizationalUnit.RelocationInProgress = true;
			adorganizationalUnit.SetId(targetForestTenantSession.GetHostedOrganizationsRoot().GetChildId("OU", tenantContainerName));
			targetForestTenantSession.Save(adorganizationalUnit);
			return adorganizationalUnit.Id;
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000FE3C0 File Offset: 0x000FC5C0
		private ADObjectId CreateEnclosureConfigContainer(string tenantContainerName, ITenantConfigurationSession targetForestSession)
		{
			ADObjectId configurationUnitsRoot = targetForestSession.GetConfigurationUnitsRoot();
			ADObjectId childId = configurationUnitsRoot.GetChildId(tenantContainerName);
			Container container = new Container();
			container.SetId(childId);
			targetForestSession.Save(container);
			return container.Id;
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000FE3F8 File Offset: 0x000FC5F8
		private ExchangeConfigurationUnit CreateOrgConfigurationContainer(ADObjectId enclosureContainerId, OrganizationId targetSharedOrgId, string externalDirectoryOrganizationId, string programId, string offerId, bool isDehydrated, bool isStaticConfigurationShared, string sourceForestFqdn, Guid sourceObjectGuid, Guid sourceObjectExchangeObjectId, ITenantConfigurationSession targetForestTenantSession)
		{
			ExchangeConfigurationUnit exchangeConfigurationUnit = new ExchangeConfigurationUnit();
			exchangeConfigurationUnit.SetId(enclosureContainerId.GetChildId("Configuration"));
			exchangeConfigurationUnit.ExternalDirectoryOrganizationId = externalDirectoryOrganizationId;
			exchangeConfigurationUnit.RelocationStatusDetailsRaw = RelocationStatusDetails.InitializationStarted;
			exchangeConfigurationUnit.OrganizationStatus = OrganizationStatus.PendingArrival;
			exchangeConfigurationUnit.RelocationSourceForestRaw = sourceForestFqdn;
			exchangeConfigurationUnit.ProgramId = programId;
			exchangeConfigurationUnit.OfferId = offerId;
			exchangeConfigurationUnit.IsDehydrated = isDehydrated;
			exchangeConfigurationUnit.IsStaticConfigurationShared = isStaticConfigurationShared;
			exchangeConfigurationUnit.CorrelationId = sourceObjectGuid;
			exchangeConfigurationUnit.ExchangeObjectId = sourceObjectExchangeObjectId;
			if (targetSharedOrgId != null)
			{
				exchangeConfigurationUnit.SupportedSharedConfigurations = new MultiValuedProperty<ADObjectId>();
				exchangeConfigurationUnit.SupportedSharedConfigurations.Add(targetSharedOrgId.ConfigurationUnit);
			}
			exchangeConfigurationUnit.Name = "Configuration";
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(targetForestTenantSession.SessionSettings.PartitionId), 546, "CreateOrgConfigurationContainer", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Relocation\\NewTenantRelocationRequest.cs");
			string parentLegacyDN = string.Format("{0}{1}", topologyConfigurationSession.GetAdministrativeGroup().LegacyExchangeDN, this.GetRelativeDNTillConfigurationUnits(exchangeConfigurationUnit.Id));
			exchangeConfigurationUnit.LegacyExchangeDN = LegacyDN.GenerateLegacyDN(parentLegacyDN, exchangeConfigurationUnit);
			targetForestTenantSession.Save(exchangeConfigurationUnit);
			return exchangeConfigurationUnit;
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000FE500 File Offset: 0x000FC700
		private string GetRelativeDNTillConfigurationUnits(ADObjectId newTenant)
		{
			ADObjectId parent = newTenant.Parent;
			string text = string.Empty;
			while (parent != null && !string.Equals(parent.Name, ADObject.ConfigurationUnits, StringComparison.OrdinalIgnoreCase))
			{
				text = string.Format("{0}/cn={1}", text, parent.Name);
				parent = parent.Parent;
			}
			return text;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000FE54C File Offset: 0x000FC74C
		private string GetInitialDomainName(ITenantConfigurationSession session, OrganizationId orgId)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.InitialDomain, true);
			ADObjectId childId = orgId.ConfigurationUnit.GetChildId("Transport Settings").GetChildId("Accepted Domains");
			AcceptedDomain[] array = session.Find<AcceptedDomain>(childId, QueryScope.OneLevel, filter, null, 2);
			if (array == null || array.Length != 1)
			{
				InvalidOperationException exception = new InvalidOperationException(Strings.ErrorCannotDetermineInitialDomain(orgId.OrganizationalUnit.ToString()));
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			return array[0].DomainName.Domain;
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x000FE5D8 File Offset: 0x000FC7D8
		private static bool IntraForestRelocationEnabled()
		{
			int num = 0;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
			{
				if (registryKey != null)
				{
					num = (int)registryKey.GetValue("IntraForestRelocationEnabled", 0);
				}
			}
			return num != 0;
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000FE634 File Offset: 0x000FC834
		private static bool GLSRecordCheckDisabled()
		{
			return TenantRelocationSyncCoordinator.GetInt32ValueFromRegistryValueOrDefault("GLSRecordCheckDisabled", 0U) != 0U;
		}

		// Token: 0x040026D2 RID: 9938
		private PartitionId sourceAccountPartitionId;

		// Token: 0x040026D3 RID: 9939
		private string sourceForestFqdn;

		// Token: 0x040026D4 RID: 9940
		private string sourceForestRIDMaster;

		// Token: 0x040026D5 RID: 9941
		private string targetForestRIDMaster;

		// Token: 0x040026D6 RID: 9942
		private string targetAccountPartitionFqdn;

		// Token: 0x040026D7 RID: 9943
		private Guid externalDirectoryOrganizationId;
	}
}
