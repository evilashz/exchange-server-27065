using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200023F RID: 575
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum MeetingRequestTypeType
	{
		// Token: 0x04000F2E RID: 3886
		None,
		// Token: 0x04000F2F RID: 3887
		FullUpdate,
		// Token: 0x04000F30 RID: 3888
		InformationalUpdate,
		// Token: 0x04000F31 RID: 3889
		NewMeetingRequest,
		// Token: 0x04000F32 RID: 3890
		Outdated,
		// Token: 0x04000F33 RID: 3891
		SilentUpdate,
		// Token: 0x04000F34 RID: 3892
		PrincipalWantsCopy
	}
}
