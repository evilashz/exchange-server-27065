using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x02000002 RID: 2
	internal interface IEventSource
	{
		// Token: 0x06000001 RID: 1
		void Defer(TimeSpan delayTime, SmtpResponse response);
	}
}
