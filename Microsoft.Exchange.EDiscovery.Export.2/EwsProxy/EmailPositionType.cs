using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200017D RID: 381
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum EmailPositionType
	{
		// Token: 0x04000B50 RID: 2896
		LatestReply,
		// Token: 0x04000B51 RID: 2897
		Other,
		// Token: 0x04000B52 RID: 2898
		Subject,
		// Token: 0x04000B53 RID: 2899
		Signature
	}
}
