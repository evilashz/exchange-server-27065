using System;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x02000003 RID: 3
	internal sealed class EventSource : IEventSource
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D0 File Offset: 0x000002D0
		public EventSource(SubmittedMessageEventSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020ED File Offset: 0x000002ED
		public void Defer(TimeSpan delayTime, SmtpResponse response)
		{
			this.source.Defer(delayTime, response);
		}

		// Token: 0x04000001 RID: 1
		private SubmittedMessageEventSource source;
	}
}
