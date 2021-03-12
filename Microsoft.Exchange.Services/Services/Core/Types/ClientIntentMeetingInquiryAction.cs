using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000725 RID: 1829
	[XmlType(TypeName = "ClientIntentMeetingInquiryActionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ClientIntentMeetingInquiryAction
	{
		// Token: 0x04001ED3 RID: 7891
		SendCancellation,
		// Token: 0x04001ED4 RID: 7892
		ReviveMeeting,
		// Token: 0x04001ED5 RID: 7893
		SendUpdateForMaster,
		// Token: 0x04001ED6 RID: 7894
		MeetingAlreadyExists,
		// Token: 0x04001ED7 RID: 7895
		ExistingOccurrence,
		// Token: 0x04001ED8 RID: 7896
		HasDelegates,
		// Token: 0x04001ED9 RID: 7897
		DeletedVersionNotFound,
		// Token: 0x04001EDA RID: 7898
		PairedCancellationFound,
		// Token: 0x04001EDB RID: 7899
		FailedToRevive
	}
}
