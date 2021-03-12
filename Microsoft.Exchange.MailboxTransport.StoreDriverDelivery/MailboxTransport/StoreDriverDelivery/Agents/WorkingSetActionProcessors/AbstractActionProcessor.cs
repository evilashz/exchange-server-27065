using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkingSet;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.WorkingSetActionProcessors
{
	// Token: 0x020000CF RID: 207
	internal abstract class AbstractActionProcessor : IActionProcessor
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x000238EF File Offset: 0x00021AEF
		protected AbstractActionProcessor(ActionProcessorFactory actionProcessorFactory)
		{
			this.actionProcessorFactory = actionProcessorFactory;
		}

		// Token: 0x06000656 RID: 1622
		public abstract void Process(StoreDriverDeliveryEventArgsImpl argsImpl, Action action, int traceId);

		// Token: 0x04000377 RID: 887
		protected static readonly Trace Tracer = ExTraceGlobals.WorkingSetAgentTracer;

		// Token: 0x04000378 RID: 888
		protected ActionProcessorFactory actionProcessorFactory;
	}
}
