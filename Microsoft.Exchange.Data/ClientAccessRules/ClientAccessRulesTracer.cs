using System;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000115 RID: 277
	internal class ClientAccessRulesTracer : ITracer
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0001E2EB File Offset: 0x0001C4EB
		internal ClientAccessRulesTracer(long traceId)
		{
			this.traceId = traceId;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001E2FA File Offset: 0x0001C4FA
		public void TraceDebug(string message)
		{
			ExTraceGlobals.ClientAccessRulesTracer.TraceDebug(this.traceId, message);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001E30D File Offset: 0x0001C50D
		public void TraceDebug(string formatString, params object[] args)
		{
			this.TraceDebug(string.Format(formatString, args));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001E31C File Offset: 0x0001C51C
		public void TraceWarning(string message)
		{
			ExTraceGlobals.ClientAccessRulesTracer.TraceWarning(this.traceId, message);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001E32F File Offset: 0x0001C52F
		public void TraceWarning(string formatString, params object[] args)
		{
			this.TraceWarning(string.Format(formatString, args));
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001E33E File Offset: 0x0001C53E
		public void TraceError(string message)
		{
			ExTraceGlobals.ClientAccessRulesTracer.TraceError(this.traceId, message);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001E351 File Offset: 0x0001C551
		public void TraceError(string formatString, params object[] args)
		{
			this.TraceError(string.Format(formatString, args));
		}

		// Token: 0x0400060E RID: 1550
		private readonly long traceId;
	}
}
