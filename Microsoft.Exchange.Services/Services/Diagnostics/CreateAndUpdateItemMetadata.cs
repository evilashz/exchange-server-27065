using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200002D RID: 45
	public enum CreateAndUpdateItemMetadata
	{
		// Token: 0x040001E1 RID: 481
		[DisplayName("CUI", "MD")]
		MessageDisposition,
		// Token: 0x040001E2 RID: 482
		[DisplayName("CUI", "TNR")]
		TotalNbRecipients,
		// Token: 0x040001E3 RID: 483
		[DisplayName("CUI", "TBS")]
		TotalBodySize,
		// Token: 0x040001E4 RID: 484
		[DisplayName("CUI", "TNM")]
		TotalNbMessages,
		// Token: 0x040001E5 RID: 485
		[DisplayName("CUI", "GetRI")]
		GetMailboxItemResponseObjectInformation,
		// Token: 0x040001E6 RID: 486
		[DisplayName("CUI", "PI")]
		PrepareItem,
		// Token: 0x040001E7 RID: 487
		[DisplayName("CUI", "GCIB.T")]
		GetCalendarItemBaseTime,
		// Token: 0x040001E8 RID: 488
		[DisplayName("CUI", "GCIBRPC.L")]
		GetCalendarItemBaseRpcLatency,
		// Token: 0x040001E9 RID: 489
		[DisplayName("CUI", "GCIBRPC.C")]
		GetCalendarItemBaseRpcCount,
		// Token: 0x040001EA RID: 490
		[DisplayName("CUI", "SAV")]
		Save,
		// Token: 0x040001EB RID: 491
		[DisplayName("CUI", "TM")]
		TotalMeetings,
		// Token: 0x040001EC RID: 492
		[DisplayName("CUI", "RM.T")]
		RespondToMeetingRequestTime,
		// Token: 0x040001ED RID: 493
		[DisplayName("CUI", "RMRPC.C")]
		RespondToMeetingRequestRpcCount,
		// Token: 0x040001EE RID: 494
		[DisplayName("CUI", "RMRPC.L")]
		RespondToMeetingRequestRpcLatency,
		// Token: 0x040001EF RID: 495
		[DisplayName("CUI", "RCB.T")]
		RespondToCalendarItemBaseTime,
		// Token: 0x040001F0 RID: 496
		[DisplayName("CUI", "RCBRPC.C")]
		RespondToCalendarItemBaseRpcCount,
		// Token: 0x040001F1 RID: 497
		[DisplayName("CUI", "RCBRPC.L")]
		RespondToCalendarItemBaseRpcLatency,
		// Token: 0x040001F2 RID: 498
		[DisplayName("CUI", "UPD.T")]
		UpdateMeetingTime,
		// Token: 0x040001F3 RID: 499
		[DisplayName("CUI", "UPDRPC.C")]
		UpdateMeetingRpcCount,
		// Token: 0x040001F4 RID: 500
		[DisplayName("CUI", "UPDRPC.L")]
		UpdateMeetingRpcLatency,
		// Token: 0x040001F5 RID: 501
		[DisplayName("CUI", "LDI.T")]
		LoadAndDeleteItemTime,
		// Token: 0x040001F6 RID: 502
		[DisplayName("CUI", "LDIRPC.C")]
		LoadAndDeleteItemRpcCount,
		// Token: 0x040001F7 RID: 503
		[DisplayName("CUI", "LDIRPC.L")]
		LoadAndDeleteItemRpcLatency,
		// Token: 0x040001F8 RID: 504
		[DisplayName("CUI", "MI.T")]
		MoveItemTime,
		// Token: 0x040001F9 RID: 505
		[DisplayName("CUI", "MIRPC.C")]
		MoveItemRpcCount,
		// Token: 0x040001FA RID: 506
		[DisplayName("CUI", "MIRPC.L")]
		MoveItemRpcLatency,
		// Token: 0x040001FB RID: 507
		[DisplayName("CUI", "ACT")]
		ActionType,
		// Token: 0x040001FC RID: 508
		[DisplayName("CUI", "ST")]
		SessionType,
		// Token: 0x040001FD RID: 509
		[DisplayName("CUI", "PRIP")]
		Principal,
		// Token: 0x040001FE RID: 510
		[DisplayName("CUI", "CO")]
		ComposeOperation
	}
}
