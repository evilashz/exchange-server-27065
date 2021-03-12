using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001C5 RID: 453
	[Cmdlet("Enable", "OrganizationCustomization", SupportsShouldProcess = true, DefaultParameterSetName = "IdentityParameterSet")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class EnableOrganizationCustomizationTask : ManageServicePlanMigrationBase
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00045CD2 File Offset: 0x00043ED2
		protected override LocalizedString Description
		{
			get
			{
				return Strings.EnableConfigCustomizationsDescription;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00045CD9 File Offset: 0x00043ED9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableConfigCustomizations(this.orgIdParam.ToString());
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00045CEB File Offset: 0x00043EEB
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00045CF3 File Offset: 0x00043EF3
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override OrganizationIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00045CFC File Offset: 0x00043EFC
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			OrganizationId organizationId = null;
			if (OrganizationId.ForestWideOrgId.Equals(base.ExecutingUserOrganizationId))
			{
				if (this.Identity == null)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorNeedOrgIdentity), (ErrorCategory)1000, null);
				}
				else
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 109, "InternalStateReset", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\EnableConfigCustomizations.cs");
					tenantOrTopologyConfigurationSession.UseConfigNC = false;
					ADOrganizationalUnit oufromOrganizationId = OrganizationTaskHelper.GetOUFromOrganizationId(this.Identity, tenantOrTopologyConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
					organizationId = oufromOrganizationId.OrganizationId;
				}
			}
			else
			{
				organizationId = base.ExecutingUserOrganizationId;
			}
			this.orgIdParam = new OrganizationIdParameter(organizationId.OrganizationalUnit);
			base.LoadTenantCU();
			TaskLogger.LogExit();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00045DE0 File Offset: 0x00043FE0
		protected override void OnHalt(Exception e)
		{
			TaskLogger.LogEvent(TaskEventLogConstants.Tuple_HydrationTaskFailed, base.CurrentTaskContext.InvocationInfo, this.tenantCU.DistinguishedName, new object[]
			{
				e.ToString()
			});
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00045E20 File Offset: 0x00044020
		protected override void ResolveTargetOffer()
		{
			string targetOfferId;
			if (!this.config.TryGetHydratedOfferId(this.tenantCU.ProgramId, this.tenantCU.OfferId, out targetOfferId))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorChangeConfigurationNotSupported), (ErrorCategory)1000, null);
				return;
			}
			this.targetProgramId = this.tenantCU.ProgramId;
			this.targetOfferId = targetOfferId;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00045E86 File Offset: 0x00044086
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			if (base.ExchangeRunspaceConfig != null)
			{
				base.ExchangeRunspaceConfig.LoadRoleCmdletInfo();
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00045EA1 File Offset: 0x000440A1
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new EnableOrganizationCustomizationTaskModuleFactory();
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00045EA8 File Offset: 0x000440A8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!this.IsHydrationAllowed())
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorHydrationDisabled(this.tenantCU.ConfigurationUnit.Parent.Name)), (ErrorCategory)1000, null);
			}
			if (this.tenantCU.IsPilotingOrganization)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTenantIsPiloting), (ErrorCategory)1000, null);
			}
			if (this.tenantCU.AdminDisplayVersion != null && (int)this.tenantCU.AdminDisplayVersion.ExchangeBuild.Major < OrganizationTaskHelper.ManagementDllVersion.FileMajorPart)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTenantNeedsUpgradeFirst), (ErrorCategory)1000, null);
			}
			if (this.tenantCU.IsUpgradingOrganization)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTenantIsUpgrading), (ErrorCategory)1000, null);
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00045F94 File Offset: 0x00044194
		private bool IsHydrationAllowed()
		{
			bool config = SlimTenantConfigImpl.GetConfig<bool>("IsHydrationAllowed");
			ExTraceGlobals.SlimTenantTracer.TraceDebug<bool>(0L, "Global Config: EnableOrganizationCustomizationTask::IsHydrationAllowed: {0}.", config);
			return config;
		}
	}
}
