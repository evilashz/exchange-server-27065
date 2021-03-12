using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A55 RID: 2645
	[Cmdlet("Remove", "HostedOutboundSpamFilterPolicy")]
	public sealed class RemoveHostedOutboundSpamFilterPolicy : RemoveSystemConfigurationObjectTask<HostedOutboundSpamFilterPolicyIdParameter, HostedOutboundSpamFilterPolicy>
	{
		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x0018CDA0 File Offset: 0x0018AFA0
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x06005ED0 RID: 24272 RVA: 0x0018CDA3 File Offset: 0x0018AFA3
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			TaskLogger.LogExit();
		}

		// Token: 0x06005ED1 RID: 24273 RVA: 0x0018CDE4 File Offset: 0x0018AFE4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			FfoDualWriter.DeleteFromFfo<HostedOutboundSpamFilterPolicy>(this, base.DataObject);
			TaskLogger.LogExit();
		}
	}
}
