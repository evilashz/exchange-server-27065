using System;

namespace Microsoft.Exchange.Diagnostics.Components.SpamEngine
{
	// Token: 0x020003C5 RID: 965
	public static class ExTraceGlobals
	{
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x000596CD File Offset: 0x000578CD
		public static Trace RulesEngineTracer
		{
			get
			{
				if (ExTraceGlobals.rulesEngineTracer == null)
				{
					ExTraceGlobals.rulesEngineTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.rulesEngineTracer;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x000596EB File Offset: 0x000578EB
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00059709 File Offset: 0x00057909
		public static Trace BackScatterTracer
		{
			get
			{
				if (ExTraceGlobals.backScatterTracer == null)
				{
					ExTraceGlobals.backScatterTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.backScatterTracer;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00059727 File Offset: 0x00057927
		public static Trace SenderAuthenticationTracer
		{
			get
			{
				if (ExTraceGlobals.senderAuthenticationTracer == null)
				{
					ExTraceGlobals.senderAuthenticationTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.senderAuthenticationTracer;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00059745 File Offset: 0x00057945
		public static Trace UriScanTracer
		{
			get
			{
				if (ExTraceGlobals.uriScanTracer == null)
				{
					ExTraceGlobals.uriScanTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.uriScanTracer;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00059763 File Offset: 0x00057963
		public static Trace DnsChecksTracer
		{
			get
			{
				if (ExTraceGlobals.dnsChecksTracer == null)
				{
					ExTraceGlobals.dnsChecksTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.dnsChecksTracer;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x00059781 File Offset: 0x00057981
		public static Trace DkimTracer
		{
			get
			{
				if (ExTraceGlobals.dkimTracer == null)
				{
					ExTraceGlobals.dkimTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.dkimTracer;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0005979F File Offset: 0x0005799F
		public static Trace DmarcTracer
		{
			get
			{
				if (ExTraceGlobals.dmarcTracer == null)
				{
					ExTraceGlobals.dmarcTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.dmarcTracer;
			}
		}

		// Token: 0x04001BDA RID: 7130
		private static Guid componentGuid = new Guid("D47F7E4B-8F27-43fa-9BE6-DDF69C7AE225");

		// Token: 0x04001BDB RID: 7131
		private static Trace rulesEngineTracer = null;

		// Token: 0x04001BDC RID: 7132
		private static Trace commonTracer = null;

		// Token: 0x04001BDD RID: 7133
		private static Trace backScatterTracer = null;

		// Token: 0x04001BDE RID: 7134
		private static Trace senderAuthenticationTracer = null;

		// Token: 0x04001BDF RID: 7135
		private static Trace uriScanTracer = null;

		// Token: 0x04001BE0 RID: 7136
		private static Trace dnsChecksTracer = null;

		// Token: 0x04001BE1 RID: 7137
		private static Trace dkimTracer = null;

		// Token: 0x04001BE2 RID: 7138
		private static Trace dmarcTracer = null;
	}
}
