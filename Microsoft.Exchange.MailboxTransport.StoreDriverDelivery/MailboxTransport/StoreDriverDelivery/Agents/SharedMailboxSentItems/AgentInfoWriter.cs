using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000B0 RID: 176
	internal class AgentInfoWriter : IAgentInfoWriter
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x0001F490 File Offset: 0x0001D690
		public AgentInfoWriter(StoreDriverDeliveryEventArgs eventArgs, string agentName)
		{
			ArgumentValidator.ThrowIfNull("eventArgs", eventArgs);
			ArgumentValidator.ThrowIfNull("agentName", agentName);
			this.eventArgs = eventArgs;
			this.agentName = agentName;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001F4BC File Offset: 0x0001D6BC
		public void AddAgentInfo(string eventName, string message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("message", message)
			};
			this.eventArgs.AddAgentInfo(this.agentName, eventName, data);
		}

		// Token: 0x04000346 RID: 838
		private readonly StoreDriverDeliveryEventArgs eventArgs;

		// Token: 0x04000347 RID: 839
		private readonly string agentName;
	}
}
