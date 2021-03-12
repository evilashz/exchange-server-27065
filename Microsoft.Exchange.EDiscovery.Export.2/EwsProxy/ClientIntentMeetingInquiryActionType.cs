using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000BB RID: 187
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ClientIntentMeetingInquiryActionType
	{
		// Token: 0x04000566 RID: 1382
		SendCancellation,
		// Token: 0x04000567 RID: 1383
		ReviveMeeting,
		// Token: 0x04000568 RID: 1384
		SendUpdateForMaster,
		// Token: 0x04000569 RID: 1385
		MeetingAlreadyExists,
		// Token: 0x0400056A RID: 1386
		ExistingOccurrence,
		// Token: 0x0400056B RID: 1387
		HasDelegates,
		// Token: 0x0400056C RID: 1388
		DeletedVersionNotFound,
		// Token: 0x0400056D RID: 1389
		PairedCancellationFound,
		// Token: 0x0400056E RID: 1390
		FailedToRevive
	}
}
