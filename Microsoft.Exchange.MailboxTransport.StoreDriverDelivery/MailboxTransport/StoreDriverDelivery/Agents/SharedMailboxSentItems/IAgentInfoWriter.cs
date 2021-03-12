using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000AF RID: 175
	internal interface IAgentInfoWriter
	{
		// Token: 0x060005B5 RID: 1461
		void AddAgentInfo(string eventName, string message);
	}
}
