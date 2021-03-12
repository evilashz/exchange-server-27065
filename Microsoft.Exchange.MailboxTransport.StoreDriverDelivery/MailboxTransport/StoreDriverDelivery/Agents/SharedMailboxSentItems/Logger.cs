using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.SharedMailboxSentItems
{
	// Token: 0x020000B5 RID: 181
	internal sealed class Logger : ILogger
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x0001F500 File Offset: 0x0001D700
		public Logger(Trace trace)
		{
			ArgumentValidator.ThrowIfNull("trace", trace);
			this.trace = trace;
			this.traceId = this.GetHashCode();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001F528 File Offset: 0x0001D728
		public void LogEvent(ExEventLog.EventTuple eventTuple, Exception exception)
		{
			StoreDriverDeliveryDiagnostics.LogEvent(eventTuple, exception.Message, new object[]
			{
				exception
			});
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001F54D File Offset: 0x0001D74D
		public void TraceDebug(params string[] messagesStrings)
		{
			if (messagesStrings.Length > 0 && this.trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.trace.TraceDebug((long)this.traceId, string.Concat(messagesStrings));
			}
		}

		// Token: 0x04000348 RID: 840
		private readonly Trace trace;

		// Token: 0x04000349 RID: 841
		private readonly int traceId;
	}
}
