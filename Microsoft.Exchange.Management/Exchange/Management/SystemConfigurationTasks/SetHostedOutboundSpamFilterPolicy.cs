using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A56 RID: 2646
	[Cmdlet("Set", "HostedOutboundSpamFilterPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetHostedOutboundSpamFilterPolicy : SetSystemConfigurationObjectTask<HostedOutboundSpamFilterPolicyIdParameter, HostedOutboundSpamFilterPolicy>
	{
		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x0018CE4F File Offset: 0x0018B04F
		// (set) Token: 0x06005ED4 RID: 24276 RVA: 0x0018CE57 File Offset: 0x0018B057
		[Parameter]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06005ED5 RID: 24277 RVA: 0x0018CE60 File Offset: 0x0018B060
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetHostedOutboundSpamFilterPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06005ED6 RID: 24278 RVA: 0x0018CE72 File Offset: 0x0018B072
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x0018CE84 File Offset: 0x0018B084
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			if (!this.IgnoreDehydratedFlag)
			{
				SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x0018CEDC File Offset: 0x0018B0DC
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ADObject adobject = dataObject as ADObject;
			if (adobject != null)
			{
				this.dualWriter = new FfoDualWriter(adobject.Name);
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x06005ED9 RID: 24281 RVA: 0x0018CF0C File Offset: 0x0018B10C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			this.dualWriter.Save<HostedOutboundSpamFilterPolicy>(this, this.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x040034F4 RID: 13556
		private FfoDualWriter dualWriter;
	}
}
