using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.Transport.Storage
{
	// Token: 0x02000093 RID: 147
	public abstract class StorageAgentFactory : AgentFactory
	{
		// Token: 0x06000369 RID: 873
		internal abstract StorageAgent CreateAgent(SmtpServer server);

		// Token: 0x0600036A RID: 874 RVA: 0x0000873A File Offset: 0x0000693A
		internal override Agent CreateAgent(string typeName, object state)
		{
			if (typeName != typeof(StorageAgent).FullName)
			{
				throw new ConfigurationErrorsException(string.Format("The supplied agent factory doesn't match the agent type found in the supplied assembly. typeName=={0}", typeName));
			}
			return this.CreateAgent(null);
		}
	}
}
