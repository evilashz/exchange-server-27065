using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200012D RID: 301
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ResponseTypeType
	{
		// Token: 0x04000990 RID: 2448
		Unknown,
		// Token: 0x04000991 RID: 2449
		Organizer,
		// Token: 0x04000992 RID: 2450
		Tentative,
		// Token: 0x04000993 RID: 2451
		Accept,
		// Token: 0x04000994 RID: 2452
		Decline,
		// Token: 0x04000995 RID: 2453
		NoResponseReceived
	}
}
