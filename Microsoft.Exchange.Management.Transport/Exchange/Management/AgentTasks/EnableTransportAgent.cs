using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02000015 RID: 21
	[Cmdlet("Enable", "TransportAgent", SupportsShouldProcess = true)]
	public class EnableTransportAgent : AgentBaseTask
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000414D File Offset: 0x0000234D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return AgentStrings.ConfirmationMessageEnableTransportAgent(this.Identity.ToString());
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004167 File Offset: 0x00002367
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			base.SetAgentEnabled(this.Identity.ToString(), true);
			base.Save();
			this.WriteWarning(AgentStrings.RestartServiceForChanges(base.GetTransportServiceName()));
			TaskLogger.LogExit();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000041A2 File Offset: 0x000023A2
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000041B9 File Offset: 0x000023B9
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
