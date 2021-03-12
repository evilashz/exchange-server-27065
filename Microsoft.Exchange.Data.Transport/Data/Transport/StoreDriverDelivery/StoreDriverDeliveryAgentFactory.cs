using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.Transport.StoreDriverDelivery
{
	// Token: 0x020000A1 RID: 161
	internal abstract class StoreDriverDeliveryAgentFactory : AgentFactory
	{
		// Token: 0x0600039F RID: 927
		public abstract StoreDriverDeliveryAgent CreateAgent(SmtpServer server);

		// Token: 0x060003A0 RID: 928 RVA: 0x00008A84 File Offset: 0x00006C84
		internal override Agent CreateAgent(string typeName, object state)
		{
			if (typeName != typeof(StoreDriverDeliveryAgent).FullName || (state != null && !(state is SmtpServer)))
			{
				throw new ConfigurationErrorsException(string.Format("The supplied agent factory doesn't match the agent type found in the supplied assembly. typeName=={0} state=={1}", typeName, (state == null) ? "null" : state.GetType().ToString()));
			}
			return this.CreateAgent((SmtpServer)state);
		}
	}
}
