using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200034F RID: 847
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum OofState
	{
		// Token: 0x04001410 RID: 5136
		Disabled,
		// Token: 0x04001411 RID: 5137
		Enabled,
		// Token: 0x04001412 RID: 5138
		Scheduled
	}
}
