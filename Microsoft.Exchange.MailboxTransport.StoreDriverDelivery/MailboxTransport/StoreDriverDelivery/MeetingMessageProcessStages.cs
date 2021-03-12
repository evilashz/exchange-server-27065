using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200009C RID: 156
	internal enum MeetingMessageProcessStages
	{
		// Token: 0x040002DC RID: 732
		Unprocessed,
		// Token: 0x040002DD RID: 733
		DelegateMessageFound,
		// Token: 0x040002DE RID: 734
		ErrorObtainingCalendarItem,
		// Token: 0x040002DF RID: 735
		ExternalMsgProcessingDisabled,
		// Token: 0x040002E0 RID: 736
		ProcessMeetingRequest,
		// Token: 0x040002E1 RID: 737
		ProcessMeetingResponse,
		// Token: 0x040002E2 RID: 738
		ProcessMeetingCancellation,
		// Token: 0x040002E3 RID: 739
		ProcessMFN,
		// Token: 0x040002E4 RID: 740
		MessageReceipientIsOrganizer,
		// Token: 0x040002E5 RID: 741
		RUMAbortDelivery,
		// Token: 0x040002E6 RID: 742
		CalendarAssistantAddNewItemsFalse,
		// Token: 0x040002E7 RID: 743
		CalendarAssistantActiveFalse,
		// Token: 0x040002E8 RID: 744
		ResourceMailboxFound,
		// Token: 0x040002E9 RID: 745
		HijackedMeetingFound,
		// Token: 0x040002EA RID: 746
		ParticipantMatchFailure,
		// Token: 0x040002EB RID: 747
		HijackedAppointmentFound,
		// Token: 0x040002EC RID: 748
		JunkNewMeetingRequestFound
	}
}
