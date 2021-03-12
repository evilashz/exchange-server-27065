using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x0200001F RID: 31
	[Cmdlet("Set", "TransportAgent", SupportsShouldProcess = true)]
	public class SetTransportAgent : AgentBaseTask
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004ED6 File Offset: 0x000030D6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return AgentStrings.ConfirmationMessageSetTransportAgent(this.Identity.ToString());
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004EF0 File Offset: 0x000030F0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			this.cleanIdentity = base.ValidateAndNormalizeAgentIdentity(this.Identity.ToString());
			if (!base.AgentExists(this.cleanIdentity))
			{
				base.WriteError(new ArgumentException(AgentStrings.AgentNotFound(this.Identity.ToString()), "Identity"), ErrorCategory.InvalidArgument, null);
			}
			IList<AgentInfo> publicAgentList = base.MExConfiguration.GetPublicAgentList();
			if (this.Priority != null)
			{
				if (this.Priority < 1 || this.Priority > publicAgentList.Count)
				{
					base.WriteError(new ArgumentOutOfRangeException(AgentStrings.PriorityOutOfRange(publicAgentList.Count.ToString())), ErrorCategory.InvalidArgument, null);
				}
				foreach (AgentInfo agentInfo in publicAgentList)
				{
					if (string.Compare(agentInfo.AgentName, this.cleanIdentity, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						base.MExConfiguration.AgentList.Remove(agentInfo);
						int index = this.Priority.Value - 1 + base.MExConfiguration.GetPreExecutionInternalAgents().Count;
						base.MExConfiguration.AgentList.Insert(index, agentInfo);
						base.Save();
						this.WriteWarning(AgentStrings.RestartServiceForChanges(base.GetTransportServiceName()));
						TaskLogger.LogExit();
						return;
					}
				}
				base.WriteError(new ArgumentException(AgentStrings.AgentNotFound(this.Identity.ToString()), "Identity"), ErrorCategory.InvalidArgument, null);
				return;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000050C0 File Offset: 0x000032C0
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000050D7 File Offset: 0x000032D7
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000050EA File Offset: 0x000032EA
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00005101 File Offset: 0x00003301
		[Parameter(Mandatory = false)]
		public int? Priority
		{
			get
			{
				return (int?)base.Fields["Priority"];
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x0400003D RID: 61
		private string cleanIdentity;
	}
}
