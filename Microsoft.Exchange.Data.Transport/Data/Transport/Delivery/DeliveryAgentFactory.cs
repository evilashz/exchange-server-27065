using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.Transport.Delivery
{
	// Token: 0x02000055 RID: 85
	public abstract class DeliveryAgentFactory<Manager> : AgentFactory where Manager : DeliveryAgentManager, new()
	{
		// Token: 0x060001F9 RID: 505
		public abstract DeliveryAgent CreateAgent(SmtpServer server);

		// Token: 0x060001FA RID: 506 RVA: 0x00006778 File Offset: 0x00004978
		internal override Agent CreateAgent(string typeName, object state)
		{
			if (typeName != typeof(DeliveryAgent).FullName || (state != null && !(state is SmtpServer)))
			{
				throw new ConfigurationErrorsException(string.Format("The supplied agent factory doesn't match the agent type found in the supplied assembly. typeName=={0} state=={1}", typeName, (state == null) ? "null" : state.GetType().ToString()));
			}
			return this.CreateAgent((SmtpServer)state);
		}
	}
}
