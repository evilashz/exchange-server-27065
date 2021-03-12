using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200019C RID: 412
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ClientIntentMeetingInquiryActionType
	{
		// Token: 0x040009B8 RID: 2488
		SendCancellation,
		// Token: 0x040009B9 RID: 2489
		ReviveMeeting,
		// Token: 0x040009BA RID: 2490
		SendUpdateForMaster,
		// Token: 0x040009BB RID: 2491
		MeetingAlreadyExists,
		// Token: 0x040009BC RID: 2492
		ExistingOccurrence,
		// Token: 0x040009BD RID: 2493
		HasDelegates,
		// Token: 0x040009BE RID: 2494
		DeletedVersionNotFound,
		// Token: 0x040009BF RID: 2495
		PairedCancellationFound,
		// Token: 0x040009C0 RID: 2496
		FailedToRevive
	}
}
