using System;
using System.Text;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000043 RID: 67
	internal class TransportRulesTracer : ITracer
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000EADC File Offset: 0x0000CCDC
		internal bool IsTestMessage { get; private set; }

		// Token: 0x06000284 RID: 644 RVA: 0x0000EAE5 File Offset: 0x0000CCE5
		public override string ToString()
		{
			if (this.traceStringBuilder != null)
			{
				return this.traceStringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000EB00 File Offset: 0x0000CD00
		internal TransportRulesTracer(MailItem mailItem, bool isTestMessage = false)
		{
			this.mailItem = mailItem;
			this.IsTestMessage = isTestMessage;
			if (this.IsTestMessage)
			{
				this.traceStringBuilder = new StringBuilder();
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EB29 File Offset: 0x0000CD29
		public void TraceDebug(string message)
		{
			ExTraceGlobals.TransportRulesEngineTracer.TraceDebug(0L, message);
			if (this.mailItem != null)
			{
				SystemProbeHelper.EtrTracer.TracePass(this.mailItem, 0L, message);
			}
			if (this.traceStringBuilder != null)
			{
				this.traceStringBuilder.AppendLine(message);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EB68 File Offset: 0x0000CD68
		public void TraceDebug(string formatString, params object[] args)
		{
			this.TraceDebug(string.Format(formatString, args));
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000EB77 File Offset: 0x0000CD77
		public void TraceWarning(string message)
		{
			ExTraceGlobals.TransportRulesEngineTracer.TraceWarning(0L, message);
			if (this.traceStringBuilder != null)
			{
				this.traceStringBuilder.AppendLine(message);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000EB9B File Offset: 0x0000CD9B
		public void TraceWarning(string formatString, params object[] args)
		{
			this.TraceWarning(string.Format(formatString, args));
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000EBAA File Offset: 0x0000CDAA
		public void TraceError(string message)
		{
			ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, message);
			if (this.traceStringBuilder != null)
			{
				this.traceStringBuilder.AppendLine(message);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000EBCE File Offset: 0x0000CDCE
		public void TraceError(string formatString, params object[] args)
		{
			this.TraceError(string.Format(formatString, args));
		}

		// Token: 0x040001C3 RID: 451
		private MailItem mailItem;

		// Token: 0x040001C4 RID: 452
		private StringBuilder traceStringBuilder;
	}
}
