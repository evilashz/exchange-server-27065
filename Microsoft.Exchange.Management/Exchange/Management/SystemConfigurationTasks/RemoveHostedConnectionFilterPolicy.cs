using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A3E RID: 2622
	[Cmdlet("Remove", "HostedConnectionFilterPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveHostedConnectionFilterPolicy : RemoveSystemConfigurationObjectTask<HostedConnectionFilterPolicyIdParameter, HostedConnectionFilterPolicy>
	{
		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x06005D9F RID: 23967 RVA: 0x00189E7E File Offset: 0x0018807E
		// (set) Token: 0x06005DA0 RID: 23968 RVA: 0x00189E86 File Offset: 0x00188086
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return base.InternalForce;
			}
			set
			{
				base.InternalForce = value;
			}
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x06005DA1 RID: 23969 RVA: 0x00189E8F File Offset: 0x0018808F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveHostedConnectionFilterPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x06005DA2 RID: 23970 RVA: 0x00189EA1 File Offset: 0x001880A1
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x00189EA4 File Offset: 0x001880A4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			if (base.DataObject.IsDefault && !this.Force)
			{
				base.WriteError(new OperationNotAllowedException(Strings.ErrorDefaultHostedConnectionFilterPolicyCannotBeDeleted), ErrorCategory.InvalidOperation, base.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x00189F20 File Offset: 0x00188120
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			FfoDualWriter.DeleteFromFfo<HostedConnectionFilterPolicy>(this, base.DataObject);
			TaskLogger.LogExit();
		}
	}
}
