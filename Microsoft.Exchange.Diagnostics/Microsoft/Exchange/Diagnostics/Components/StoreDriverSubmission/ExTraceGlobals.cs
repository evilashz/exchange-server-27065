using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission
{
	// Token: 0x0200032B RID: 811
	public static class ExTraceGlobals
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0004C971 File Offset: 0x0004AB71
		public static Trace StoreDriverSubmissionTracer
		{
			get
			{
				if (ExTraceGlobals.storeDriverSubmissionTracer == null)
				{
					ExTraceGlobals.storeDriverSubmissionTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.storeDriverSubmissionTracer;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0004C98F File Offset: 0x0004AB8F
		public static Trace MapiStoreDriverSubmissionTracer
		{
			get
			{
				if (ExTraceGlobals.mapiStoreDriverSubmissionTracer == null)
				{
					ExTraceGlobals.mapiStoreDriverSubmissionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mapiStoreDriverSubmissionTracer;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0004C9AD File Offset: 0x0004ABAD
		public static Trace MailboxTransportSubmissionServiceTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxTransportSubmissionServiceTracer == null)
				{
					ExTraceGlobals.mailboxTransportSubmissionServiceTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.mailboxTransportSubmissionServiceTracer;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0004C9CB File Offset: 0x0004ABCB
		public static Trace MeetingForwardNotificationTracer
		{
			get
			{
				if (ExTraceGlobals.meetingForwardNotificationTracer == null)
				{
					ExTraceGlobals.meetingForwardNotificationTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.meetingForwardNotificationTracer;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0004C9E9 File Offset: 0x0004ABE9
		public static Trace ModeratedTransportTracer
		{
			get
			{
				if (ExTraceGlobals.moderatedTransportTracer == null)
				{
					ExTraceGlobals.moderatedTransportTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.moderatedTransportTracer;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0004CA08 File Offset: 0x0004AC08
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0004CA27 File Offset: 0x0004AC27
		public static Trace SubmissionConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.submissionConnectionTracer == null)
				{
					ExTraceGlobals.submissionConnectionTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.submissionConnectionTracer;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0004CA46 File Offset: 0x0004AC46
		public static Trace SubmissionConnectionPoolTracer
		{
			get
			{
				if (ExTraceGlobals.submissionConnectionPoolTracer == null)
				{
					ExTraceGlobals.submissionConnectionPoolTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.submissionConnectionPoolTracer;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0004CA65 File Offset: 0x0004AC65
		public static Trace ParkedItemSubmitterAgentTracer
		{
			get
			{
				if (ExTraceGlobals.parkedItemSubmitterAgentTracer == null)
				{
					ExTraceGlobals.parkedItemSubmitterAgentTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.parkedItemSubmitterAgentTracer;
			}
		}

		// Token: 0x040015F9 RID: 5625
		private static Guid componentGuid = new Guid("2b76aa96-0fe5-4c87-8101-1d236c9fa3ab");

		// Token: 0x040015FA RID: 5626
		private static Trace storeDriverSubmissionTracer = null;

		// Token: 0x040015FB RID: 5627
		private static Trace mapiStoreDriverSubmissionTracer = null;

		// Token: 0x040015FC RID: 5628
		private static Trace mailboxTransportSubmissionServiceTracer = null;

		// Token: 0x040015FD RID: 5629
		private static Trace meetingForwardNotificationTracer = null;

		// Token: 0x040015FE RID: 5630
		private static Trace moderatedTransportTracer = null;

		// Token: 0x040015FF RID: 5631
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001600 RID: 5632
		private static Trace submissionConnectionTracer = null;

		// Token: 0x04001601 RID: 5633
		private static Trace submissionConnectionPoolTracer = null;

		// Token: 0x04001602 RID: 5634
		private static Trace parkedItemSubmitterAgentTracer = null;
	}
}
