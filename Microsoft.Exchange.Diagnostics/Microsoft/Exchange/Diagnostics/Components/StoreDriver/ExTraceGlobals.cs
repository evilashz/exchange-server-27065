using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.StoreDriver
{
	// Token: 0x02000327 RID: 807
	public static class ExTraceGlobals
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0004C317 File Offset: 0x0004A517
		public static Trace StoreDriverTracer
		{
			get
			{
				if (ExTraceGlobals.storeDriverTracer == null)
				{
					ExTraceGlobals.storeDriverTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.storeDriverTracer;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0004C335 File Offset: 0x0004A535
		public static Trace MapiSubmitTracer
		{
			get
			{
				if (ExTraceGlobals.mapiSubmitTracer == null)
				{
					ExTraceGlobals.mapiSubmitTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mapiSubmitTracer;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x0004C353 File Offset: 0x0004A553
		public static Trace MapiDeliverTracer
		{
			get
			{
				if (ExTraceGlobals.mapiDeliverTracer == null)
				{
					ExTraceGlobals.mapiDeliverTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.mapiDeliverTracer;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0004C371 File Offset: 0x0004A571
		public static Trace MailSubmissionServiceTracer
		{
			get
			{
				if (ExTraceGlobals.mailSubmissionServiceTracer == null)
				{
					ExTraceGlobals.mailSubmissionServiceTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.mailSubmissionServiceTracer;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0004C38F File Offset: 0x0004A58F
		public static Trace BridgeheadPickerTracer
		{
			get
			{
				if (ExTraceGlobals.bridgeheadPickerTracer == null)
				{
					ExTraceGlobals.bridgeheadPickerTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.bridgeheadPickerTracer;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0004C3AD File Offset: 0x0004A5AD
		public static Trace CalendarProcessingTracer
		{
			get
			{
				if (ExTraceGlobals.calendarProcessingTracer == null)
				{
					ExTraceGlobals.calendarProcessingTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.calendarProcessingTracer;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0004C3CB File Offset: 0x0004A5CB
		public static Trace ExceptionHandlingTracer
		{
			get
			{
				if (ExTraceGlobals.exceptionHandlingTracer == null)
				{
					ExTraceGlobals.exceptionHandlingTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.exceptionHandlingTracer;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0004C3E9 File Offset: 0x0004A5E9
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

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0004C407 File Offset: 0x0004A607
		public static Trace ApprovalAgentTracer
		{
			get
			{
				if (ExTraceGlobals.approvalAgentTracer == null)
				{
					ExTraceGlobals.approvalAgentTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.approvalAgentTracer;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0004C425 File Offset: 0x0004A625
		public static Trace MailboxRuleTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxRuleTracer == null)
				{
					ExTraceGlobals.mailboxRuleTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.mailboxRuleTracer;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0004C444 File Offset: 0x0004A644
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

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0004C463 File Offset: 0x0004A663
		public static Trace ConversationsTracer
		{
			get
			{
				if (ExTraceGlobals.conversationsTracer == null)
				{
					ExTraceGlobals.conversationsTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.conversationsTracer;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0004C482 File Offset: 0x0004A682
		public static Trace MailSubmissionRedundancyManagerTracer
		{
			get
			{
				if (ExTraceGlobals.mailSubmissionRedundancyManagerTracer == null)
				{
					ExTraceGlobals.mailSubmissionRedundancyManagerTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.mailSubmissionRedundancyManagerTracer;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0004C4A1 File Offset: 0x0004A6A1
		public static Trace UMPlayonPhoneAgentTracer
		{
			get
			{
				if (ExTraceGlobals.uMPlayonPhoneAgentTracer == null)
				{
					ExTraceGlobals.uMPlayonPhoneAgentTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.uMPlayonPhoneAgentTracer;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0004C4C0 File Offset: 0x0004A6C0
		public static Trace SmsDeliveryAgentTracer
		{
			get
			{
				if (ExTraceGlobals.smsDeliveryAgentTracer == null)
				{
					ExTraceGlobals.smsDeliveryAgentTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.smsDeliveryAgentTracer;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0004C4DF File Offset: 0x0004A6DF
		public static Trace UMPartnerMessageAgentTracer
		{
			get
			{
				if (ExTraceGlobals.uMPartnerMessageAgentTracer == null)
				{
					ExTraceGlobals.uMPartnerMessageAgentTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.uMPartnerMessageAgentTracer;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0004C4FE File Offset: 0x0004A6FE
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

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0004C51D File Offset: 0x0004A71D
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

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0004C53C File Offset: 0x0004A73C
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

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0004C55B File Offset: 0x0004A75B
		public static Trace UnJournalDeliveryAgentTracer
		{
			get
			{
				if (ExTraceGlobals.unJournalDeliveryAgentTracer == null)
				{
					ExTraceGlobals.unJournalDeliveryAgentTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.unJournalDeliveryAgentTracer;
			}
		}

		// Token: 0x040015CB RID: 5579
		private static Guid componentGuid = new Guid("a77be922-83fd-4eb1-9e88-6caadbc7340e");

		// Token: 0x040015CC RID: 5580
		private static Trace storeDriverTracer = null;

		// Token: 0x040015CD RID: 5581
		private static Trace mapiSubmitTracer = null;

		// Token: 0x040015CE RID: 5582
		private static Trace mapiDeliverTracer = null;

		// Token: 0x040015CF RID: 5583
		private static Trace mailSubmissionServiceTracer = null;

		// Token: 0x040015D0 RID: 5584
		private static Trace bridgeheadPickerTracer = null;

		// Token: 0x040015D1 RID: 5585
		private static Trace calendarProcessingTracer = null;

		// Token: 0x040015D2 RID: 5586
		private static Trace exceptionHandlingTracer = null;

		// Token: 0x040015D3 RID: 5587
		private static Trace meetingForwardNotificationTracer = null;

		// Token: 0x040015D4 RID: 5588
		private static Trace approvalAgentTracer = null;

		// Token: 0x040015D5 RID: 5589
		private static Trace mailboxRuleTracer = null;

		// Token: 0x040015D6 RID: 5590
		private static Trace moderatedTransportTracer = null;

		// Token: 0x040015D7 RID: 5591
		private static Trace conversationsTracer = null;

		// Token: 0x040015D8 RID: 5592
		private static Trace mailSubmissionRedundancyManagerTracer = null;

		// Token: 0x040015D9 RID: 5593
		private static Trace uMPlayonPhoneAgentTracer = null;

		// Token: 0x040015DA RID: 5594
		private static Trace smsDeliveryAgentTracer = null;

		// Token: 0x040015DB RID: 5595
		private static Trace uMPartnerMessageAgentTracer = null;

		// Token: 0x040015DC RID: 5596
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x040015DD RID: 5597
		private static Trace submissionConnectionTracer = null;

		// Token: 0x040015DE RID: 5598
		private static Trace submissionConnectionPoolTracer = null;

		// Token: 0x040015DF RID: 5599
		private static Trace unJournalDeliveryAgentTracer = null;
	}
}
