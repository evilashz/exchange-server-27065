using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000087 RID: 135
	[DataContract(Name = "subcode")]
	internal enum ErrorSubcode
	{
		// Token: 0x04000250 RID: 592
		None,
		// Token: 0x04000251 RID: 593
		[EnumMember]
		ServiceFailure,
		// Token: 0x04000252 RID: 594
		[EnumMember]
		BadRequest,
		// Token: 0x04000253 RID: 595
		[EnumMember]
		Forbidden,
		// Token: 0x04000254 RID: 596
		[EnumMember]
		ResourceNotFound,
		// Token: 0x04000255 RID: 597
		[EnumMember]
		MethodNotAllowed,
		// Token: 0x04000256 RID: 598
		[EnumMember]
		Conflict,
		// Token: 0x04000257 RID: 599
		[EnumMember]
		InvalidOperation,
		// Token: 0x04000258 RID: 600
		[EnumMember]
		TooManyRequests,
		// Token: 0x04000259 RID: 601
		[EnumMember]
		RequestTooLarge,
		// Token: 0x0400025A RID: 602
		[EnumMember]
		ResourceTerminating,
		// Token: 0x0400025B RID: 603
		[EnumMember]
		ResourceExists,
		// Token: 0x0400025C RID: 604
		[EnumMember]
		InvalidResourceKey,
		// Token: 0x0400025D RID: 605
		[EnumMember]
		InvalidResourceState,
		// Token: 0x0400025E RID: 606
		[EnumMember]
		InvalidRequestBody,
		// Token: 0x0400025F RID: 607
		[EnumMember]
		ApplicationNotFound = 1001,
		// Token: 0x04000260 RID: 608
		[EnumMember]
		OnlineMeetingNotFound = 4001,
		// Token: 0x04000261 RID: 609
		[EnumMember]
		OnlineMeetingExists,
		// Token: 0x04000262 RID: 610
		[EnumMember]
		ConversationNotFound = 5001,
		// Token: 0x04000263 RID: 611
		[EnumMember]
		InvitationNotFound,
		// Token: 0x04000264 RID: 612
		[EnumMember]
		CallNotFound,
		// Token: 0x04000265 RID: 613
		[EnumMember]
		SessionNotFound,
		// Token: 0x04000266 RID: 614
		[EnumMember]
		ConversationOperationFailed,
		// Token: 0x04000267 RID: 615
		[EnumMember]
		InvalidInvitationType,
		// Token: 0x04000268 RID: 616
		[EnumMember]
		SessionContextNotChangable,
		// Token: 0x04000269 RID: 617
		[EnumMember]
		PendingSessionRenegotiation,
		// Token: 0x0400026A RID: 618
		[EnumMember]
		CallNotAnswered,
		// Token: 0x0400026B RID: 619
		[EnumMember]
		CallCancelled,
		// Token: 0x0400026C RID: 620
		[EnumMember]
		CallDeclined,
		// Token: 0x0400026D RID: 621
		[EnumMember]
		CallFailed,
		// Token: 0x0400026E RID: 622
		[EnumMember]
		CallTransfered,
		// Token: 0x0400026F RID: 623
		[EnumMember]
		CallReplaced,
		// Token: 0x04000270 RID: 624
		[EnumMember]
		InvalidSDP,
		// Token: 0x04000271 RID: 625
		[EnumMember]
		MediaTypeNotSupported,
		// Token: 0x04000272 RID: 626
		[EnumMember]
		OfferAnswerFailure,
		// Token: 0x04000273 RID: 627
		[EnumMember]
		AudioUnavailable,
		// Token: 0x04000274 RID: 628
		[EnumMember]
		UserNotEnabledForOutsideVoice,
		// Token: 0x04000275 RID: 629
		[EnumMember]
		CallTransferFailed
	}
}
