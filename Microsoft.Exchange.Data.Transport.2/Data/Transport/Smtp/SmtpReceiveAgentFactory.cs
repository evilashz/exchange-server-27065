using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000021 RID: 33
	public abstract class SmtpReceiveAgentFactory : AgentFactory
	{
		// Token: 0x060000AA RID: 170
		public abstract SmtpReceiveAgent CreateAgent(SmtpServer server);

		// Token: 0x060000AB RID: 171 RVA: 0x00002E14 File Offset: 0x00001014
		internal override Agent CreateAgent(string typeName, object state)
		{
			if (typeName != typeof(SmtpReceiveAgent).FullName || (state != null && !(state is SmtpServer)))
			{
				throw new ConfigurationErrorsException(string.Format("The supplied agent factory doesn't match the agent type found in the supplied assembly. typeName=={0}, state=={1}", typeName, state));
			}
			return this.CreateAgent((SmtpServer)state);
		}
	}
}
