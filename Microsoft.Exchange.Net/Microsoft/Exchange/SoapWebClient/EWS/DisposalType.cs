using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003C4 RID: 964
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DisposalType
	{
		// Token: 0x04001548 RID: 5448
		HardDelete,
		// Token: 0x04001549 RID: 5449
		SoftDelete,
		// Token: 0x0400154A RID: 5450
		MoveToDeletedItems
	}
}
