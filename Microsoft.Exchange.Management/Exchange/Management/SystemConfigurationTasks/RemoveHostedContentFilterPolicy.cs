using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A42 RID: 2626
	[Cmdlet("Remove", "HostedContentFilterPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveHostedContentFilterPolicy : RemoveSystemConfigurationObjectTask<HostedContentFilterPolicyIdParameter, HostedContentFilterPolicy>
	{
		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x0018A8AD File Offset: 0x00188AAD
		// (set) Token: 0x06005E12 RID: 24082 RVA: 0x0018A8B5 File Offset: 0x00188AB5
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

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x0018A8BE File Offset: 0x00188ABE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveHostedContentFilterPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x06005E14 RID: 24084 RVA: 0x0018A8D0 File Offset: 0x00188AD0
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x06005E15 RID: 24085 RVA: 0x0018A8D4 File Offset: 0x00188AD4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			if (base.DataObject.IsDefault && !this.Force)
			{
				base.WriteError(new OperationNotAllowedException(Strings.ErrorDefaultHostedContentFilterPolicyCannotBeDeleted), ErrorCategory.InvalidOperation, base.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x0018A950 File Offset: 0x00188B50
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			FfoDualWriter.DeleteFromFfo<HostedContentFilterPolicy>(this, base.DataObject);
			TaskLogger.LogExit();
		}
	}
}
