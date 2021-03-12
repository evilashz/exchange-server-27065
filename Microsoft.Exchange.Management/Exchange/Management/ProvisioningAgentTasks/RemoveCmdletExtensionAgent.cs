using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningAgentTasks
{
	// Token: 0x02000CD9 RID: 3289
	[Cmdlet("Remove", "CmdletExtensionAgent", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveCmdletExtensionAgent : RemoveSystemConfigurationObjectTask<CmdletExtensionAgentIdParameter, CmdletExtensionAgent>
	{
		// Token: 0x17002767 RID: 10087
		// (get) Token: 0x06007ED8 RID: 32472 RVA: 0x0020627C File Offset: 0x0020447C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveCmdletExtensionAgent(this.Identity.ToString());
			}
		}

		// Token: 0x06007ED9 RID: 32473 RVA: 0x00206290 File Offset: 0x00204490
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			this.agentsGlobalConfig = new CmdletExtensionAgentsGlobalConfig((ITopologyConfigurationSession)base.DataSession);
			foreach (LocalizedString text in this.agentsGlobalConfig.ConfigurationIssues)
			{
				this.WriteWarning(text);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x002062F1 File Offset: 0x002044F1
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			ProvisioningLayer.RefreshProvisioningBroker(this);
			TaskLogger.LogExit();
		}

		// Token: 0x04003E41 RID: 15937
		private CmdletExtensionAgentsGlobalConfig agentsGlobalConfig;
	}
}
