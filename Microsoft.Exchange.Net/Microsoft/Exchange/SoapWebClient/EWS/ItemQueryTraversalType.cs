using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200046B RID: 1131
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemQueryTraversalType
	{
		// Token: 0x04001754 RID: 5972
		Shallow,
		// Token: 0x04001755 RID: 5973
		SoftDeleted,
		// Token: 0x04001756 RID: 5974
		Associated
	}
}
