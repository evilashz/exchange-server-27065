using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002AD RID: 685
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemChoiceType
	{
		// Token: 0x040011DC RID: 4572
		AllInternal,
		// Token: 0x040011DD RID: 4573
		And,
		// Token: 0x040011DE RID: 4574
		RecipientIs,
		// Token: 0x040011DF RID: 4575
		SenderDepartments,
		// Token: 0x040011E0 RID: 4576
		True
	}
}
