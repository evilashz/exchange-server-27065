using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02000014 RID: 20
	[Cmdlet("Disable", "TransportAgent", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class DisableTransportAgent : AgentBaseTask
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000040D6 File Offset: 0x000022D6
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			base.SetAgentEnabled(this.Identity.ToString(), false);
			base.Save();
			this.WriteWarning(AgentStrings.RestartServiceForChanges(base.GetTransportServiceName()));
			TaskLogger.LogExit();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004111 File Offset: 0x00002311
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return AgentStrings.ConfirmationMessageDisableTransportAgent(this.Identity.ToString());
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004123 File Offset: 0x00002323
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000413A File Offset: 0x0000233A
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
