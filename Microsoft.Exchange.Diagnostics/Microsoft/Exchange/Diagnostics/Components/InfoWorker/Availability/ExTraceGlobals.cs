using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability
{
	// Token: 0x02000343 RID: 835
	public static class ExTraceGlobals
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00051464 File Offset: 0x0004F664
		public static Trace InitializeTracer
		{
			get
			{
				if (ExTraceGlobals.initializeTracer == null)
				{
					ExTraceGlobals.initializeTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.initializeTracer;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00051482 File Offset: 0x0004F682
		public static Trace SecurityTracer
		{
			get
			{
				if (ExTraceGlobals.securityTracer == null)
				{
					ExTraceGlobals.securityTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.securityTracer;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x000514A0 File Offset: 0x0004F6A0
		public static Trace CalendarViewTracer
		{
			get
			{
				if (ExTraceGlobals.calendarViewTracer == null)
				{
					ExTraceGlobals.calendarViewTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.calendarViewTracer;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x000514BE File Offset: 0x0004F6BE
		public static Trace ConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.configurationTracer == null)
				{
					ExTraceGlobals.configurationTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.configurationTracer;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x000514DC File Offset: 0x0004F6DC
		public static Trace PublicFolderRequestTracer
		{
			get
			{
				if (ExTraceGlobals.publicFolderRequestTracer == null)
				{
					ExTraceGlobals.publicFolderRequestTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.publicFolderRequestTracer;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x000514FA File Offset: 0x0004F6FA
		public static Trace IntraSiteCalendarRequestTracer
		{
			get
			{
				if (ExTraceGlobals.intraSiteCalendarRequestTracer == null)
				{
					ExTraceGlobals.intraSiteCalendarRequestTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.intraSiteCalendarRequestTracer;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x00051519 File Offset: 0x0004F719
		public static Trace MeetingSuggestionsTracer
		{
			get
			{
				if (ExTraceGlobals.meetingSuggestionsTracer == null)
				{
					ExTraceGlobals.meetingSuggestionsTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.meetingSuggestionsTracer;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x00051538 File Offset: 0x0004F738
		public static Trace AutoDiscoverTracer
		{
			get
			{
				if (ExTraceGlobals.autoDiscoverTracer == null)
				{
					ExTraceGlobals.autoDiscoverTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.autoDiscoverTracer;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x00051557 File Offset: 0x0004F757
		public static Trace MailboxConnectionCacheTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxConnectionCacheTracer == null)
				{
					ExTraceGlobals.mailboxConnectionCacheTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.mailboxConnectionCacheTracer;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x00051576 File Offset: 0x0004F776
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00051595 File Offset: 0x0004F795
		public static Trace DnsReaderTracer
		{
			get
			{
				if (ExTraceGlobals.dnsReaderTracer == null)
				{
					ExTraceGlobals.dnsReaderTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.dnsReaderTracer;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x000515B4 File Offset: 0x0004F7B4
		public static Trace MessageTracer
		{
			get
			{
				if (ExTraceGlobals.messageTracer == null)
				{
					ExTraceGlobals.messageTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.messageTracer;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x000515D3 File Offset: 0x0004F7D3
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x0400180B RID: 6155
		private static Guid componentGuid = new Guid("A7F9AB97-3B1B-4e10-B58F-E58136B9DA0A");

		// Token: 0x0400180C RID: 6156
		private static Trace initializeTracer = null;

		// Token: 0x0400180D RID: 6157
		private static Trace securityTracer = null;

		// Token: 0x0400180E RID: 6158
		private static Trace calendarViewTracer = null;

		// Token: 0x0400180F RID: 6159
		private static Trace configurationTracer = null;

		// Token: 0x04001810 RID: 6160
		private static Trace publicFolderRequestTracer = null;

		// Token: 0x04001811 RID: 6161
		private static Trace intraSiteCalendarRequestTracer = null;

		// Token: 0x04001812 RID: 6162
		private static Trace meetingSuggestionsTracer = null;

		// Token: 0x04001813 RID: 6163
		private static Trace autoDiscoverTracer = null;

		// Token: 0x04001814 RID: 6164
		private static Trace mailboxConnectionCacheTracer = null;

		// Token: 0x04001815 RID: 6165
		private static Trace pFDTracer = null;

		// Token: 0x04001816 RID: 6166
		private static Trace dnsReaderTracer = null;

		// Token: 0x04001817 RID: 6167
		private static Trace messageTracer = null;

		// Token: 0x04001818 RID: 6168
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
