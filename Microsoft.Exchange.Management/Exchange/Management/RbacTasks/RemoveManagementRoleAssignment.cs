using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000663 RID: 1635
	[Cmdlet("Remove", "ManagementRoleAssignment", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveManagementRoleAssignment : RemoveSystemConfigurationObjectTask<RoleAssignmentIdParameter, ExchangeRoleAssignment>
	{
		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06003972 RID: 14706 RVA: 0x000F1638 File Offset: 0x000EF838
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveManagementRoleAssignment(this.Identity.ToString(), (base.DataObject.Role == null) ? "<null>" : base.DataObject.Role.ToString(), (base.DataObject.User == null) ? "<null>" : base.DataObject.User.ToString(), base.DataObject.RoleAssignmentDelegationType.ToString(), base.DataObject.RecipientWriteScope.ToString(), base.DataObject.ConfigWriteScope.ToString());
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x000F16DC File Offset: 0x000EF8DC
		// (set) Token: 0x06003974 RID: 14708 RVA: 0x000F1702 File Offset: 0x000EF902
		[Parameter(Mandatory = false)]
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

		// Token: 0x06003975 RID: 14709 RVA: 0x000F171C File Offset: 0x000EF91C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			RoleHelper.ValidateAssignmentMethod(this.Identity, this.Identity.User, base.DataObject.Role, base.DataObject.User, new RoleHelper.ErrorRoleAssignmentDelegate(Strings.ErrorRemoveGroupRoleAssignment), new RoleHelper.ErrorRoleAssignmentDelegate(Strings.ErrorRemoveMailboxPlanRoleAssignment), new RoleHelper.ErrorRoleAssignmentDelegate(Strings.ErrorRemovePolicyRoleAssignment), new Task.TaskErrorLoggingDelegate(base.WriteError));
			RoleAssignmentsGlobalConstraints roleAssignmentsGlobalConstraints = new RoleAssignmentsGlobalConstraints(this.ConfigurationSession, base.TenantGlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError));
			roleAssignmentsGlobalConstraints.ValidateIsSafeToRemoveAssignment(base.DataObject);
			RoleHelper.HierarchyRoleAssignmentChecking(base.DataObject, base.ExchangeRunspaceConfig, this.ConfigurationSession, base.ExecutingUserOrganizationId, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), true);
			TaskLogger.LogExit();
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000F180B File Offset: 0x000EFA0B
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.ResolveDataObject();
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000F182C File Offset: 0x000EFA2C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
