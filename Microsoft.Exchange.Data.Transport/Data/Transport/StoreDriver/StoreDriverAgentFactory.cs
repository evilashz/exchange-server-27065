using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.Transport.StoreDriver
{
	// Token: 0x020000A9 RID: 169
	internal abstract class StoreDriverAgentFactory : AgentFactory
	{
		// Token: 0x060003C5 RID: 965
		public abstract StoreDriverAgent CreateAgent(SmtpServer server);

		// Token: 0x060003C6 RID: 966 RVA: 0x00008CD8 File Offset: 0x00006ED8
		internal override Agent CreateAgent(string typeName, object state)
		{
			if (typeName != typeof(StoreDriverAgent).FullName || (state != null && !(state is SmtpServer)))
			{
				throw new ConfigurationErrorsException(string.Format("The supplied agent factory doesn't match the agent type found in the supplied assembly. typeName=={0} state=={1}", typeName, (state == null) ? "null" : state.GetType().ToString()));
			}
			return this.CreateAgent((SmtpServer)state);
		}
	}
}
