using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02000016 RID: 22
	[Cmdlet("Get", "TransportAgent")]
	[OutputType(new Type[]
	{
		typeof(TransportAgent)
	})]
	public class GetTransportAgent : AgentBaseTask
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000041D4 File Offset: 0x000023D4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			GetTransportAgent.MatchOptions matchOptions = GetTransportAgent.MatchOptions.FullString;
			string text = (this.Identity == null) ? null : this.Identity.ToString();
			bool flag = !string.IsNullOrEmpty(text) && (text.StartsWith("*") || text.EndsWith("*"));
			if (flag)
			{
				if (text.StartsWith("*") && text.EndsWith("*") && text.Length > 2)
				{
					text = text.Substring(1, text.Length - 2);
					matchOptions = GetTransportAgent.MatchOptions.SubString;
				}
				else if (text.EndsWith("*") && text.Length > 1)
				{
					text = text.Substring(0, text.Length - 1);
					matchOptions = GetTransportAgent.MatchOptions.StartsWith;
				}
				else if (text.StartsWith("*") && text.Length > 1)
				{
					text = text.Substring(1);
					matchOptions = GetTransportAgent.MatchOptions.EndsWith;
				}
				else if (string.Compare("*", text, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					text = string.Empty;
				}
			}
			if (!string.IsNullOrEmpty(text) && -1 != text.IndexOf("*", StringComparison.InvariantCultureIgnoreCase))
			{
				TaskLogger.LogExit();
				return;
			}
			IList<AgentInfo> publicAgentList = base.MExConfiguration.GetPublicAgentList();
			for (int i = 0; i < publicAgentList.Count; i++)
			{
				if (string.IsNullOrEmpty(text) || (matchOptions == GetTransportAgent.MatchOptions.FullString && string.Compare(publicAgentList[i].AgentName, text, StringComparison.InvariantCultureIgnoreCase) == 0) || (matchOptions == GetTransportAgent.MatchOptions.SubString && -1 != publicAgentList[i].AgentName.IndexOf(text, StringComparison.InvariantCultureIgnoreCase)) || (matchOptions == GetTransportAgent.MatchOptions.EndsWith && publicAgentList[i].AgentName.EndsWith(text, StringComparison.InvariantCultureIgnoreCase)) || (matchOptions == GetTransportAgent.MatchOptions.StartsWith && publicAgentList[i].AgentName.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)))
				{
					TransportAgent sendToPipeline = new TransportAgent(publicAgentList[i].AgentName, publicAgentList[i].Enabled, i + 1, publicAgentList[i].FactoryTypeName, publicAgentList[i].FactoryAssemblyPath);
					base.WriteObject(sendToPipeline);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000043C9 File Offset: 0x000025C9
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000043E0 File Offset: 0x000025E0
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ParameterSetName = "Identity", Position = 0)]
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

		// Token: 0x02000017 RID: 23
		private enum MatchOptions
		{
			// Token: 0x0400002D RID: 45
			FullString,
			// Token: 0x0400002E RID: 46
			SubString,
			// Token: 0x0400002F RID: 47
			StartsWith,
			// Token: 0x04000030 RID: 48
			EndsWith
		}
	}
}
