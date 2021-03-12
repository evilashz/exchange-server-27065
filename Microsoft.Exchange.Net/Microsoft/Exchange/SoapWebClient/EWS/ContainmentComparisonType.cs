using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002FD RID: 765
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ContainmentComparisonType
	{
		// Token: 0x040012EA RID: 4842
		Exact,
		// Token: 0x040012EB RID: 4843
		IgnoreCase,
		// Token: 0x040012EC RID: 4844
		IgnoreNonSpacingCharacters,
		// Token: 0x040012ED RID: 4845
		Loose,
		// Token: 0x040012EE RID: 4846
		IgnoreCaseAndNonSpacingCharacters,
		// Token: 0x040012EF RID: 4847
		LooseAndIgnoreCase,
		// Token: 0x040012F0 RID: 4848
		LooseAndIgnoreNonSpace,
		// Token: 0x040012F1 RID: 4849
		LooseAndIgnoreCaseAndIgnoreNonSpace
	}
}
