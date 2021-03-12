using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200022C RID: 556
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum OnlineMeetingAccessLevelType
	{
		// Token: 0x04000E36 RID: 3638
		Locked,
		// Token: 0x04000E37 RID: 3639
		Invited,
		// Token: 0x04000E38 RID: 3640
		Internal,
		// Token: 0x04000E39 RID: 3641
		Everyone
	}
}
