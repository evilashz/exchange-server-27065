using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.Transport.Routing
{
	// Token: 0x02000084 RID: 132
	public abstract class RoutingAgentFactory : AgentFactory
	{
		// Token: 0x060002FC RID: 764
		public abstract RoutingAgent CreateAgent(SmtpServer server);

		// Token: 0x060002FD RID: 765 RVA: 0x000079DC File Offset: 0x00005BDC
		internal override Agent CreateAgent(string typeName, object state)
		{
			if (typeName != typeof(RoutingAgent).FullName || (state != null && !(state is SmtpServer)))
			{
				throw new ConfigurationErrorsException(string.Format("The supplied agent factory doesn't match the agent type found in the supplied assembly. typeName=={0} state=={1}", typeName, (state == null) ? "null" : state.GetType().ToString()));
			}
			return this.CreateAgent((SmtpServer)state);
		}
	}
}
