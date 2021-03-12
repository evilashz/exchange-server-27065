using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000223 RID: 547
	[Cmdlet("Remove", "Organization", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "OrgScopedParameterSet")]
	public sealed class RemoveOrganization : ManageOrganizationTaskBase
	{
		// Token: 0x06001290 RID: 4752 RVA: 0x00051838 File Offset: 0x0004FA38
		public RemoveOrganization()
		{
			base.Fields["InstallationMode"] = InstallationModes.Uninstall;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00051861 File Offset: 0x0004FA61
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new RemoveOrganizationTaskModuleFactory();
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00051868 File Offset: 0x0004FA68
		protected override bool ShouldExecuteComponentTasks()
		{
			return true;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0005186B File Offset: 0x0004FA6B
		protected override LocalizedString Description
		{
			get
			{
				return Strings.RemoveOrganizationDescription;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00051872 File Offset: 0x0004FA72
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOrganization(this.Identity.ToString());
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00051884 File Offset: 0x0004FA84
		protected override ExchangeRunspaceConfigurationSettings.ExchangeApplication ClientApplication
		{
			get
			{
				return ExchangeRunspaceConfigurationSettings.ExchangeApplication.DiscretionaryScripts;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00051888 File Offset: 0x0004FA88
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x0005188F File Offset: 0x0004FA8F
		private new Fqdn DomainController
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00051896 File Offset: 0x0004FA96
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x000518BC File Offset: 0x0004FABC
		[Parameter(Mandatory = false)]
		public SwitchParameter ForReconciliation
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForReconciliation"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForReconciliation"] = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x000518D4 File Offset: 0x0004FAD4
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x000518FA File Offset: 0x0004FAFA
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00051912 File Offset: 0x0004FB12
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x00051938 File Offset: 0x0004FB38
		[Parameter]
		public SwitchParameter Async
		{
			get
			{
				return (SwitchParameter)(base.Fields["Async"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Async"] = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00051950 File Offset: 0x0004FB50
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x00051958 File Offset: 0x0004FB58
		[Parameter]
		public SwitchParameter AuthoritativeOnly
		{
			get
			{
				return this.authoritativeOnly;
			}
			set
			{
				this.authoritativeOnly = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00051961 File Offset: 0x0004FB61
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x00051978 File Offset: 0x0004FB78
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0005198B File Offset: 0x0004FB8B
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x000519AC File Offset: 0x0004FBAC
		private bool PartnerMode
		{
			get
			{
				return (bool)(base.Fields["PartnerMode"] ?? false);
			}
			set
			{
				base.Fields["PartnerMode"] = value;
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000519C4 File Offset: 0x0004FBC4
		protected override void PopulateContextVariables()
		{
			base.PopulateContextVariables();
			if (this.exchangeConfigUnit != null)
			{
				base.Fields["OrganizationHierarchicalPath"] = OrganizationIdParameter.GetHierarchicalIdentityFromDN(this.tenantOU.DistinguishedName);
				base.Fields["RemoveObjectsChunkSize"] = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ProvisioningThrottling", "RemoveObjectsChunkSize", 100);
				base.Fields["RemoveObjectsChunkSleepTime"] = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ProvisioningThrottling", "RemoveObjectsChunkSleepTime", 10);
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00051A62 File Offset: 0x0004FC62
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.ExchangeRunspaceConfig != null)
			{
				this.PartnerMode = base.ExchangeRunspaceConfig.PartnerMode;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00051A90 File Offset: 0x0004FC90
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			OrganizationId organizationId = OrganizationTaskHelper.ResolveOrganization(this, this.Identity, base.RootOrgContainerId, Strings.ErrorOrganizationIdentityRequired);
			if (this.Identity == null)
			{
				this.Identity = new OrganizationIdParameter(organizationId.OrganizationalUnit.Name);
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(organizationId.PartitionId);
			this.adSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession((base.ServerSettings == null) ? null : base.ServerSettings.PreferredGlobalCatalog(organizationId.PartitionId.ForestFQDN), false, ConsistencyMode.PartiallyConsistent, sessionSettings, 262, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\RemoveOrganization.cs");
			this.tenantOU = OrganizationTaskHelper.GetOUFromOrganizationId(this.Identity, this.adSession, new Task.TaskErrorLoggingDelegate(base.WriteError), false);
			if (this.tenantOU == null)
			{
				base.WriteError(new OrganizationDoesNotExistException(Strings.ErrorOrganizationNotFound(this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
			}
			this.adSession.UseConfigNC = true;
			this.exchangeConfigUnit = this.adSession.Read<ExchangeConfigurationUnit>(this.tenantOU.ConfigurationUnit);
			if (!OrganizationTaskHelper.CanProceedWithOrganizationTask(this.Identity, this.adSession, RemoveOrganization.ignorableFlagsOnStatusTimeout, new Task.TaskErrorLoggingDelegate(base.WriteError)))
			{
				base.WriteError(new OrganizationPendingOperationException(Strings.ErrorCannotRemoveNonActiveOrganization(this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
			}
			ServicePlan servicePlanSettings = ServicePlanConfiguration.GetInstance().GetServicePlanSettings(this.exchangeConfigUnit.ProgramId, this.exchangeConfigUnit.OfferId);
			base.InternalLocalStaticConfigEnabled = !servicePlanSettings.Organization.AdvancedHydrateableObjectsSharedEnabled;
			base.InternalLocalHydrateableConfigEnabled = !servicePlanSettings.Organization.CommonHydrateableObjectsSharedEnabled;
			base.InternalCreateSharedConfiguration = (this.exchangeConfigUnit.SharedConfigurationInfo != null);
			ADSessionSettings sessionSettings2 = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, this.tenantOU.OrganizationId, base.ExecutingUserOrganizationId, false);
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession((base.ServerSettings == null) ? null : base.ServerSettings.PreferredGlobalCatalog(organizationId.PartitionId.ForestFQDN), true, ConsistencyMode.PartiallyConsistent, sessionSettings2, 314, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\RemoveOrganization.cs");
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession((base.ServerSettings == null) ? null : base.ServerSettings.PreferredGlobalCatalog(organizationId.PartitionId.ForestFQDN), true, ConsistencyMode.PartiallyConsistent, sessionSettings2, 320, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\RemoveOrganization.cs");
			TransportConfigContainer transportConfigContainer = tenantOrTopologyConfigurationSession.FindSingletonConfigurationObject<TransportConfigContainer>();
			if (transportConfigContainer != null)
			{
				this.organizationFederatedMailboxAlias = transportConfigContainer.OrganizationFederatedMailbox.Local;
			}
			if (!this.Force && this.GetUserMailboxCount(2) > 0)
			{
				base.WriteError(new OrganizationValidationException(Strings.RemoveOrganizationFailWithExistingMailboxes), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (ExchangeConfigurationUnit.RelocationInProgress(this.exchangeConfigUnit))
			{
				base.WriteError(new OrganizationValidationException(Strings.RemoveOrganizationFailRelocationInProgress), (ErrorCategory)1000, this.Identity);
			}
			if (this.exchangeConfigUnit.EnableAsSharedConfiguration)
			{
				base.WriteError(new OrganizationValidationException(Strings.RemoveOrganizationFailWithoutSharedConfigurationParameter), (ErrorCategory)1000, this.Identity);
			}
			if (OrganizationTaskHelper.IsSharedConfigLinkedToOtherTenants(organizationId, this.adSession))
			{
				base.WriteError(new OrganizationValidationException(Strings.RemoveOrganizationFailWithSharedConfigurationBacklinks), (ErrorCategory)1000, this.Identity);
			}
			if (base.IsMSITTenant(organizationId))
			{
				this.authoritativeOnly = true;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00051DD4 File Offset: 0x0004FFD4
		protected override void SetRunspaceVariables()
		{
			base.SetRunspaceVariables();
			if (this.authoritativeOnly)
			{
				this.monadConnection.RunspaceProxy.SetVariable(StartOrganizationUpgradeTask.AuthoritativeOnlyVarName, true);
			}
			this.monadConnection.RunspaceProxy.SetVariable("ReconciliationMode", this.ForReconciliation);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00051E74 File Offset: 0x00050074
		protected override void FilterComponents()
		{
			base.FilterComponents();
			if (this.ForReconciliation)
			{
				foreach (SetupComponentInfo setupComponentInfo in base.ComponentInfoList)
				{
					setupComponentInfo.Tasks.RemoveAll(delegate(TaskInfo taskInfo)
					{
						OrgTaskInfo orgTaskInfo = taskInfo as OrgTaskInfo;
						return orgTaskInfo != null && orgTaskInfo.Uninstall != null && orgTaskInfo.Uninstall.Tenant != null && !orgTaskInfo.Uninstall.Tenant.UseForReconciliation;
					});
				}
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00051EFC File Offset: 0x000500FC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			int defaultValue = this.IsTestTopologyDomain() ? 100 : 0;
			int value = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ProvisioningThrottling", "MailboxCountAsyncRemovalSize", defaultValue);
			if (this.Async && this.GetRecipientCount(value + 1, false, true) > value)
			{
				OrganizationTaskHelper.SetOrganizationStatus(this.adSession, this.exchangeConfigUnit, OrganizationStatus.ReadyForRemoval);
			}
			else
			{
				base.Fields["TenantOrganizationFullPath"] = this.tenantOU.DistinguishedName;
				base.Fields["TenantCUFullPath"] = this.tenantOU.ConfigurationUnit.Parent.DistinguishedName;
				base.Fields[RemoveOrganization.ExternalDirectoryOrganizationIdVarName] = this.exchangeConfigUnit.ExternalDirectoryOrganizationId;
				if (this.exchangeConfigUnit.OrganizationStatus != OrganizationStatus.PendingRemoval)
				{
					OrganizationTaskHelper.SetOrganizationStatus(this.adSession, this.exchangeConfigUnit, OrganizationStatus.PendingRemoval);
				}
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00051FEF File Offset: 0x000501EF
		private int GetUserMailboxCount(int maxCount)
		{
			return this.GetRecipientCount(maxCount, true, false);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00051FFC File Offset: 0x000501FC
		private bool IsTestTopologyDomain()
		{
			string forestName = NativeHelpers.GetForestName();
			return !string.IsNullOrEmpty(forestName) && (forestName.EndsWith(".extest.microsoft.com", StringComparison.InvariantCultureIgnoreCase) || forestName.EndsWith(".extest.net", StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00052038 File Offset: 0x00050238
		internal int GetRecipientCount(int maxCount, bool userMailboxOnly, bool includeSoftDeletedObjects)
		{
			int result = 0;
			IRecipientSession recipientSession = this.recipientSession;
			QueryFilter queryFilter = new NotFilter(new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.ArbitrationMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.MailboxPlan),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.DiscoveryMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.AuditLogMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.Alias, this.organizationFederatedMailboxAlias)
			}));
			if (userMailboxOnly)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
					queryFilter
				});
			}
			if (includeSoftDeletedObjects)
			{
				ADSessionSettings sessionSettings = this.recipientSession.SessionSettings;
				sessionSettings.IncludeSoftDeletedObjects = true;
				recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(this.recipientSession.DomainController, this.recipientSession.SearchRoot, this.recipientSession.Lcid, this.recipientSession.ReadOnly, this.recipientSession.ConsistencyMode, this.recipientSession.NetworkCredential, sessionSettings, ConfigScopes.TenantSubTree, 555, "GetRecipientCount", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\RemoveOrganization.cs");
				recipientSession.EnforceDefaultScope = this.recipientSession.EnforceDefaultScope;
				recipientSession.UseGlobalCatalog = this.recipientSession.UseGlobalCatalog;
				recipientSession.LinkResolutionServer = this.recipientSession.LinkResolutionServer;
			}
			ADRecipient[] array = recipientSession.Find(null, QueryScope.SubTree, queryFilter, null, maxCount);
			if (array != null)
			{
				result = array.Length;
			}
			return result;
		}

		// Token: 0x040007E6 RID: 2022
		private const string OrgScopedParameterSet = "OrgScopedParameterSet";

		// Token: 0x040007E7 RID: 2023
		private const string ForReconciliationVarName = "ReconciliationMode";

		// Token: 0x040007E8 RID: 2024
		private const string RemoveObjectsChunkSize = "RemoveObjectsChunkSize";

		// Token: 0x040007E9 RID: 2025
		private const string RemoveObjectsChunkSleepTime = "RemoveObjectsChunkSleepTime";

		// Token: 0x040007EA RID: 2026
		private const string RecipientCountAsyncRemovalSize = "MailboxCountAsyncRemovalSize";

		// Token: 0x040007EB RID: 2027
		internal static readonly string ExternalDirectoryOrganizationIdVarName = "TenantExternalDirectoryOrganizationId";

		// Token: 0x040007EC RID: 2028
		private ITenantConfigurationSession adSession;

		// Token: 0x040007ED RID: 2029
		private ADOrganizationalUnit tenantOU;

		// Token: 0x040007EE RID: 2030
		private ExchangeConfigurationUnit exchangeConfigUnit;

		// Token: 0x040007EF RID: 2031
		private IRecipientSession recipientSession;

		// Token: 0x040007F0 RID: 2032
		private SwitchParameter authoritativeOnly;

		// Token: 0x040007F1 RID: 2033
		private string organizationFederatedMailboxAlias = string.Empty;

		// Token: 0x040007F2 RID: 2034
		private static readonly OrganizationStatus[] ignorableFlagsOnStatusTimeout = new OrganizationStatus[]
		{
			OrganizationStatus.PendingRemoval,
			OrganizationStatus.PendingAcceptedDomainAddition,
			OrganizationStatus.PendingAcceptedDomainRemoval,
			OrganizationStatus.ReadyForRemoval,
			OrganizationStatus.SoftDeleted,
			OrganizationStatus.Suspended,
			OrganizationStatus.LockedOut
		};
	}
}
