using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200025A RID: 602
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum FlagStatusType
	{
		// Token: 0x04000F95 RID: 3989
		NotFlagged,
		// Token: 0x04000F96 RID: 3990
		Flagged,
		// Token: 0x04000F97 RID: 3991
		Complete
	}
}
