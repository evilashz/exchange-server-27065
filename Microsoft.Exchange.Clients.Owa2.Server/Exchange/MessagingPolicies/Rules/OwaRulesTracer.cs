using System;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200022B RID: 555
	internal class OwaRulesTracer : ITracer
	{
		// Token: 0x06001517 RID: 5399 RVA: 0x0004B11B File Offset: 0x0004931B
		internal OwaRulesTracer()
		{
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0004B123 File Offset: 0x00049323
		public void TraceDebug(string message)
		{
			ExTraceGlobals.OwaRulesEngineTracer.TraceDebug(0L, message);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0004B132 File Offset: 0x00049332
		public void TraceDebug(string formatString, params object[] args)
		{
			this.TraceDebug(string.Format(formatString, args));
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0004B141 File Offset: 0x00049341
		public void TraceWarning(string message)
		{
			ExTraceGlobals.OwaRulesEngineTracer.TraceWarning(0L, message);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0004B150 File Offset: 0x00049350
		public void TraceWarning(string formatString, params object[] args)
		{
			this.TraceWarning(string.Format(formatString, args));
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0004B15F File Offset: 0x0004935F
		public void TraceError(string message)
		{
			ExTraceGlobals.OwaRulesEngineTracer.TraceError(0L, message);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0004B16E File Offset: 0x0004936E
		public void TraceError(string formatString, params object[] args)
		{
			this.TraceError(string.Format(formatString, args));
		}
	}
}
