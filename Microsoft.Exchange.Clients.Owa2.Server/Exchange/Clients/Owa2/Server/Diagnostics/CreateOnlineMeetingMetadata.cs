using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200043C RID: 1084
	internal enum CreateOnlineMeetingMetadata
	{
		// Token: 0x04001469 RID: 5225
		[DisplayName("OM", "MS")]
		ManagerSipUri,
		// Token: 0x0400146A RID: 5226
		[DisplayName("OM", "ORG")]
		Organization,
		// Token: 0x0400146B RID: 5227
		[DisplayName("OM", "URL")]
		UcwaUrl,
		// Token: 0x0400146C RID: 5228
		[DisplayName("OM", "UGUID")]
		UserGuid,
		// Token: 0x0400146D RID: 5229
		[DisplayName("OM", "CID")]
		ConferenceId,
		// Token: 0x0400146E RID: 5230
		[DisplayName("OM", "IID")]
		ItemId,
		// Token: 0x0400146F RID: 5231
		[DisplayName("OM", "ITC")]
		IsTaskCompleted,
		// Token: 0x04001470 RID: 5232
		[DisplayName("OM", "IUS")]
		IsUcwaSupported,
		// Token: 0x04001471 RID: 5233
		[DisplayName("OM", "OCID")]
		OAuthCorrelationId,
		// Token: 0x04001472 RID: 5234
		[DisplayName("OM", "EX")]
		Exceptions,
		// Token: 0x04001473 RID: 5235
		[DisplayName("OM", "WEX")]
		WorkerExceptions,
		// Token: 0x04001474 RID: 5236
		[DisplayName("OM", "LCT")]
		LeaderCount,
		// Token: 0x04001475 RID: 5237
		[DisplayName("OM", "ACT")]
		AttendeeCount,
		// Token: 0x04001476 RID: 5238
		[DisplayName("OM", "EXPT")]
		ExpiryTime,
		// Token: 0x04001477 RID: 5239
		[DisplayName("OM", "EXA")]
		DefaultEntryExitAnnouncement,
		// Token: 0x04001478 RID: 5240
		[DisplayName("OM", "ALA")]
		AutomaticLeaderAssignment,
		// Token: 0x04001479 RID: 5241
		[DisplayName("OM", "ALVL")]
		AccessLevel,
		// Token: 0x0400147A RID: 5242
		[DisplayName("OM", "PWT")]
		ParticipantsWarningThreshold,
		// Token: 0x0400147B RID: 5243
		[DisplayName("OM", "EXAP")]
		PolicyEntryExitAnnouncement,
		// Token: 0x0400147C RID: 5244
		[DisplayName("OM", "PUA")]
		PhoneUserAdmission,
		// Token: 0x0400147D RID: 5245
		[DisplayName("OM", "EMR")]
		ExternalUserMeetingRecording,
		// Token: 0x0400147E RID: 5246
		[DisplayName("OM", "MR")]
		MeetingRecording,
		// Token: 0x0400147F RID: 5247
		[DisplayName("OM", "VA")]
		VoipAudio,
		// Token: 0x04001480 RID: 5248
		[DisplayName("OM", "MSZ")]
		MeetingSize,
		// Token: 0x04001481 RID: 5249
		[DisplayName("OM", "CO")]
		CacheOperation,
		// Token: 0x04001482 RID: 5250
		[DisplayName("OM", "RSH")]
		ResponseHeaders,
		// Token: 0x04001483 RID: 5251
		[DisplayName("OM", "RSB")]
		ResponseBody
	}
}
