using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02000020 RID: 32
	[Cmdlet("Uninstall", "TransportAgent", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class UninstallTransportAgent : AgentBaseTask
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00005124 File Offset: 0x00003324
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			string strB = base.ValidateAndNormalizeAgentIdentity(this.Identity.ToString());
			foreach (AgentInfo agentInfo in base.MExConfiguration.GetPublicAgentList())
			{
				if (string.Compare(agentInfo.AgentName, strB, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					base.MExConfiguration.AgentList.Remove(agentInfo);
					base.Save();
					this.WriteWarning(AgentStrings.RestartServiceForChanges(base.GetTransportServiceName()));
					TaskLogger.LogExit();
					return;
				}
			}
			base.WriteError(new ArgumentException(AgentStrings.AgentNotFound(this.Identity.ToString()), "Identity"), ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000051F8 File Offset: 0x000033F8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return AgentStrings.ConfirmationMessageUninstallTransportAgent(this.Identity.ToString());
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000520A File Offset: 0x0000340A
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00005221 File Offset: 0x00003421
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ParameterSetName = "Identity", Position = 0)]
		public TransportAgentObjectId Identity
		{
			get
			{
				return (TransportAgentObjectId)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}
	}
}
