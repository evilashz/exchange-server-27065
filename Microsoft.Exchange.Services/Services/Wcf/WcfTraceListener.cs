using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DAA RID: 3498
	public class WcfTraceListener : TraceListener
	{
		// Token: 0x060058C5 RID: 22725 RVA: 0x001145B4 File Offset: 0x001127B4
		public override void Write(string message)
		{
			ExTraceGlobals.WCFTracer.TraceDebug(0L, message);
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x001145C3 File Offset: 0x001127C3
		public override void WriteLine(string message)
		{
			ExTraceGlobals.WCFTracer.TraceDebug(0L, message);
		}
	}
}
