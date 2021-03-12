using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x0200001B RID: 27
	[OutputType(new Type[]
	{
		typeof(TransportEvent)
	})]
	[Cmdlet("Get", "TransportPipeline")]
	public class GetTransportPipeline : DataAccessTask<Server>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004623 File Offset: 0x00002823
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000462B File Offset: 0x0000282B
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004634 File Offset: 0x00002834
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 79, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\Agents\\GetTransportPipeline.cs");
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004660 File Offset: 0x00002860
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			bool flag = false;
			for (int i = 0; i < GetTransportPipeline.AgentGroups.Length; i++)
			{
				AgentSubscription agentSubscription = AgentSubscription.Load(GetTransportPipeline.AgentGroups[i]);
				if (agentSubscription != null)
				{
					try
					{
						foreach (string eventTopic in GetTransportPipeline.EventTopics[i])
						{
							TransportEvent sendToPipeline = new TransportEvent(eventTopic, agentSubscription[eventTopic]);
							base.WriteObject(sendToPipeline);
							flag = true;
						}
					}
					finally
					{
						agentSubscription.Dispose();
					}
				}
			}
			if (!flag)
			{
				this.WriteWarning(AgentStrings.NoTransportPipelineData);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000037 RID: 55
		private static readonly string[] AgentGroups = new string[]
		{
			"Microsoft.Exchange.Data.Transport.Smtp.SmtpReceiveAgent",
			"Microsoft.Exchange.Data.Transport.Routing.RoutingAgent"
		};

		// Token: 0x04000038 RID: 56
		private static readonly string[][] EventTopics = new string[][]
		{
			SmtpEventBindings.All,
			RoutingEventBindings.All
		};
	}
}
