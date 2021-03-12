using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningAgentTasks
{
	// Token: 0x02000CDA RID: 3290
	[Cmdlet("Set", "CmdletExtensionAgent", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetCmdletExtensionAgent : SetTopologySystemConfigurationObjectTask<CmdletExtensionAgentIdParameter, CmdletExtensionAgent>
	{
		// Token: 0x17002768 RID: 10088
		// (get) Token: 0x06007EDC RID: 32476 RVA: 0x00206311 File Offset: 0x00204511
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetCmdletExtensionAgent(this.Identity.ToString());
			}
		}

		// Token: 0x17002769 RID: 10089
		// (get) Token: 0x06007EDD RID: 32477 RVA: 0x00206323 File Offset: 0x00204523
		// (set) Token: 0x06007EDE RID: 32478 RVA: 0x0020634E File Offset: 0x0020454E
		[Parameter(Mandatory = false)]
		public byte Priority
		{
			get
			{
				if (base.Fields["Priority"] != null)
				{
					return (byte)base.Fields["Priority"];
				}
				return 0;
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x00206368 File Offset: 0x00204568
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

		// Token: 0x06007EE0 RID: 32480 RVA: 0x002063CC File Offset: 0x002045CC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.IsSystem && base.Fields["Priority"] != null && this.Priority != this.DataObject.Priority)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorAgentPriorityCannotBeChanged), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007EE1 RID: 32481 RVA: 0x00206434 File Offset: 0x00204634
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.Fields.Contains("Priority"))
			{
				byte priority = (byte)base.Fields["Priority"];
				if (!this.agentsGlobalConfig.IsPriorityAvailable(priority, this.DataObject) && !this.agentsGlobalConfig.FreeUpPriorityValue(priority))
				{
					base.WriteError(new ArgumentException(Strings.NotEnoughFreePrioritiesAvailable(priority.ToString())), ErrorCategory.InvalidArgument, null);
				}
				this.DataObject.Priority = priority;
			}
			if (this.agentsGlobalConfig.ObjectsToSave != null)
			{
				foreach (CmdletExtensionAgent instance in this.agentsGlobalConfig.ObjectsToSave)
				{
					base.DataSession.Save(instance);
				}
			}
			base.InternalProcessRecord();
			ProvisioningLayer.RefreshProvisioningBroker(this);
			TaskLogger.LogExit();
		}

		// Token: 0x04003E42 RID: 15938
		private CmdletExtensionAgentsGlobalConfig agentsGlobalConfig;
	}
}
