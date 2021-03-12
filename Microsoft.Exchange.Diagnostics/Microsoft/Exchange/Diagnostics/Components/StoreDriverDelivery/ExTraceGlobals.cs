using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery
{
	// Token: 0x02000329 RID: 809
	public static class ExTraceGlobals
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0004C669 File Offset: 0x0004A869
		public static Trace StoreDriverDeliveryTracer
		{
			get
			{
				if (ExTraceGlobals.storeDriverDeliveryTracer == null)
				{
					ExTraceGlobals.storeDriverDeliveryTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.storeDriverDeliveryTracer;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0004C687 File Offset: 0x0004A887
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

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0004C6A5 File Offset: 0x0004A8A5
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

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0004C6C3 File Offset: 0x0004A8C3
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

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0004C6E1 File Offset: 0x0004A8E1
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

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0004C6FF File Offset: 0x0004A8FF
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

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0004C71D File Offset: 0x0004A91D
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0004C73B File Offset: 0x0004A93B
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

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0004C75A File Offset: 0x0004A95A
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0004C779 File Offset: 0x0004A979
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

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0004C798 File Offset: 0x0004A998
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

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0004C7B7 File Offset: 0x0004A9B7
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

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0004C7D6 File Offset: 0x0004A9D6
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0004C7F5 File Offset: 0x0004A9F5
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0004C814 File Offset: 0x0004AA14
		public static Trace GroupEscalationAgentTracer
		{
			get
			{
				if (ExTraceGlobals.groupEscalationAgentTracer == null)
				{
					ExTraceGlobals.groupEscalationAgentTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.groupEscalationAgentTracer;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0004C833 File Offset: 0x0004AA33
		public static Trace MeetingMessageProcessingAgentTracer
		{
			get
			{
				if (ExTraceGlobals.meetingMessageProcessingAgentTracer == null)
				{
					ExTraceGlobals.meetingMessageProcessingAgentTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.meetingMessageProcessingAgentTracer;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0004C852 File Offset: 0x0004AA52
		public static Trace MeetingSeriesMessageOrderingAgentTracer
		{
			get
			{
				if (ExTraceGlobals.meetingSeriesMessageOrderingAgentTracer == null)
				{
					ExTraceGlobals.meetingSeriesMessageOrderingAgentTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.meetingSeriesMessageOrderingAgentTracer;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0004C871 File Offset: 0x0004AA71
		public static Trace SharedMailboxSentItemsAgentTracer
		{
			get
			{
				if (ExTraceGlobals.sharedMailboxSentItemsAgentTracer == null)
				{
					ExTraceGlobals.sharedMailboxSentItemsAgentTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.sharedMailboxSentItemsAgentTracer;
			}
		}

		// Token: 0x040015E3 RID: 5603
		private static Guid componentGuid = new Guid("D81003EF-1A7B-4AF0-BA18-236DB5A83114");

		// Token: 0x040015E4 RID: 5604
		private static Trace storeDriverDeliveryTracer = null;

		// Token: 0x040015E5 RID: 5605
		private static Trace mapiDeliverTracer = null;

		// Token: 0x040015E6 RID: 5606
		private static Trace bridgeheadPickerTracer = null;

		// Token: 0x040015E7 RID: 5607
		private static Trace calendarProcessingTracer = null;

		// Token: 0x040015E8 RID: 5608
		private static Trace exceptionHandlingTracer = null;

		// Token: 0x040015E9 RID: 5609
		private static Trace meetingForwardNotificationTracer = null;

		// Token: 0x040015EA RID: 5610
		private static Trace approvalAgentTracer = null;

		// Token: 0x040015EB RID: 5611
		private static Trace mailboxRuleTracer = null;

		// Token: 0x040015EC RID: 5612
		private static Trace moderatedTransportTracer = null;

		// Token: 0x040015ED RID: 5613
		private static Trace conversationsTracer = null;

		// Token: 0x040015EE RID: 5614
		private static Trace uMPlayonPhoneAgentTracer = null;

		// Token: 0x040015EF RID: 5615
		private static Trace smsDeliveryAgentTracer = null;

		// Token: 0x040015F0 RID: 5616
		private static Trace uMPartnerMessageAgentTracer = null;

		// Token: 0x040015F1 RID: 5617
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x040015F2 RID: 5618
		private static Trace groupEscalationAgentTracer = null;

		// Token: 0x040015F3 RID: 5619
		private static Trace meetingMessageProcessingAgentTracer = null;

		// Token: 0x040015F4 RID: 5620
		private static Trace meetingSeriesMessageOrderingAgentTracer = null;

		// Token: 0x040015F5 RID: 5621
		private static Trace sharedMailboxSentItemsAgentTracer = null;
	}
}
