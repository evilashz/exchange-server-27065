using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001F3 RID: 499
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DeliverMeetingRequestsType
	{
		// Token: 0x04000DF4 RID: 3572
		DelegatesOnly,
		// Token: 0x04000DF5 RID: 3573
		DelegatesAndMe,
		// Token: 0x04000DF6 RID: 3574
		DelegatesAndSendInformationToMe,
		// Token: 0x04000DF7 RID: 3575
		NoForward
	}
}
