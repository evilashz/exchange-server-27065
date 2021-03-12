using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000484 RID: 1156
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum FolderQueryTraversalType
	{
		// Token: 0x040017A5 RID: 6053
		Shallow,
		// Token: 0x040017A6 RID: 6054
		Deep,
		// Token: 0x040017A7 RID: 6055
		SoftDeleted
	}
}
