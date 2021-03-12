using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200015E RID: 350
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum MeetingRequestTypeType
	{
		// Token: 0x04000ADC RID: 2780
		None,
		// Token: 0x04000ADD RID: 2781
		FullUpdate,
		// Token: 0x04000ADE RID: 2782
		InformationalUpdate,
		// Token: 0x04000ADF RID: 2783
		NewMeetingRequest,
		// Token: 0x04000AE0 RID: 2784
		Outdated,
		// Token: 0x04000AE1 RID: 2785
		SilentUpdate,
		// Token: 0x04000AE2 RID: 2786
		PrincipalWantsCopy
	}
}
