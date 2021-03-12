using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000B1 RID: 177
	internal interface ILogger
	{
		// Token: 0x060005B8 RID: 1464
		void LogEvent(ExEventLog.EventTuple eventTuple, Exception exception);

		// Token: 0x060005B9 RID: 1465
		void TraceDebug(params string[] messagesStrings);
	}
}
