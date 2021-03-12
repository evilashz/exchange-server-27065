using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000205 RID: 517
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SensitivityChoicesType
	{
		// Token: 0x04000D6F RID: 3439
		Normal,
		// Token: 0x04000D70 RID: 3440
		Personal,
		// Token: 0x04000D71 RID: 3441
		Private,
		// Token: 0x04000D72 RID: 3442
		Confidential
	}
}
