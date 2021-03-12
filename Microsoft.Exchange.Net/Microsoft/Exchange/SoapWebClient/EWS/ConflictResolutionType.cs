using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000476 RID: 1142
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ConflictResolutionType
	{
		// Token: 0x0400177E RID: 6014
		NeverOverwrite,
		// Token: 0x0400177F RID: 6015
		AutoResolve,
		// Token: 0x04001780 RID: 6016
		AlwaysOverwrite
	}
}
