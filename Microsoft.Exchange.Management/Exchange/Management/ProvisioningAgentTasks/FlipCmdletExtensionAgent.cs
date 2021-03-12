using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningAgentTasks
{
	// Token: 0x02000CD4 RID: 3284
	public abstract class FlipCmdletExtensionAgent : SystemConfigurationObjectActionTask<CmdletExtensionAgentIdParameter, CmdletExtensionAgent>
	{
		// Token: 0x06007EB1 RID: 32433 RVA: 0x00205B78 File Offset: 0x00203D78
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			this.agentsGlobalConfig = new CmdletExtensionAgentsGlobalConfig((ITopologyConfigurationSession)base.DataSession);
			if (this.agentsGlobalConfig.ConfigurationIssues.Length > 0)
			{
				base.WriteError(new InvalidOperationException(this.agentsGlobalConfig.ConfigurationIssues[0]), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007EB2 RID: 32434 RVA: 0x00205BE3 File Offset: 0x00203DE3
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.IsSystem && !this.FlipTo)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorAgentCannotBeDisabled), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x17002757 RID: 10071
		// (get) Token: 0x06007EB3 RID: 32435
		internal abstract bool FlipTo { get; }

		// Token: 0x06007EB4 RID: 32436 RVA: 0x00205C21 File Offset: 0x00203E21
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.DataObject.Enabled = this.FlipTo;
			base.InternalProcessRecord();
			ProvisioningLayer.RefreshProvisioningBroker(this);
			TaskLogger.LogExit();
		}

		// Token: 0x04003E3E RID: 15934
		private CmdletExtensionAgentsGlobalConfig agentsGlobalConfig;
	}
}
