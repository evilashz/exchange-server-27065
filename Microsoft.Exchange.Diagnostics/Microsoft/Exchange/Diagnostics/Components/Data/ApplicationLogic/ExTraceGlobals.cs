using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic
{
	// Token: 0x0200032F RID: 815
	public static class ExTraceGlobals
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0004D8C8 File Offset: 0x0004BAC8
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0004D8E6 File Offset: 0x0004BAE6
		public static Trace FreeBusyTracer
		{
			get
			{
				if (ExTraceGlobals.freeBusyTracer == null)
				{
					ExTraceGlobals.freeBusyTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.freeBusyTracer;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0004D904 File Offset: 0x0004BB04
		public static Trace ExtensionTracer
		{
			get
			{
				if (ExTraceGlobals.extensionTracer == null)
				{
					ExTraceGlobals.extensionTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.extensionTracer;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004D922 File Offset: 0x0004BB22
		public static Trace PeopleConnectConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.peopleConnectConfigurationTracer == null)
				{
					ExTraceGlobals.peopleConnectConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.peopleConnectConfigurationTracer;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0004D940 File Offset: 0x0004BB40
		public static Trace MailboxFileStoreTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxFileStoreTracer == null)
				{
					ExTraceGlobals.mailboxFileStoreTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.mailboxFileStoreTracer;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0004D95E File Offset: 0x0004BB5E
		public static Trace CafeTracer
		{
			get
			{
				if (ExTraceGlobals.cafeTracer == null)
				{
					ExTraceGlobals.cafeTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.cafeTracer;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0004D97C File Offset: 0x0004BB7C
		public static Trace DiagnosticHandlersTracer
		{
			get
			{
				if (ExTraceGlobals.diagnosticHandlersTracer == null)
				{
					ExTraceGlobals.diagnosticHandlersTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.diagnosticHandlersTracer;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0004D99A File Offset: 0x0004BB9A
		public static Trace E4ETracer
		{
			get
			{
				if (ExTraceGlobals.e4ETracer == null)
				{
					ExTraceGlobals.e4ETracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.e4ETracer;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0004D9B8 File Offset: 0x0004BBB8
		public static Trace SyncCalendarTracer
		{
			get
			{
				if (ExTraceGlobals.syncCalendarTracer == null)
				{
					ExTraceGlobals.syncCalendarTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.syncCalendarTracer;
			}
		}

		// Token: 0x04001665 RID: 5733
		private static Guid componentGuid = new Guid("A9F57445-AB0E-47ff-90F3-9593E8D23B6F");

		// Token: 0x04001666 RID: 5734
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001667 RID: 5735
		private static Trace freeBusyTracer = null;

		// Token: 0x04001668 RID: 5736
		private static Trace extensionTracer = null;

		// Token: 0x04001669 RID: 5737
		private static Trace peopleConnectConfigurationTracer = null;

		// Token: 0x0400166A RID: 5738
		private static Trace mailboxFileStoreTracer = null;

		// Token: 0x0400166B RID: 5739
		private static Trace cafeTracer = null;

		// Token: 0x0400166C RID: 5740
		private static Trace diagnosticHandlersTracer = null;

		// Token: 0x0400166D RID: 5741
		private static Trace e4ETracer = null;

		// Token: 0x0400166E RID: 5742
		private static Trace syncCalendarTracer = null;
	}
}
