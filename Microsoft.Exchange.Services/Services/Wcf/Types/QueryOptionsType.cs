using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B2F RID: 2863
	[Flags]
	[XmlType(TypeName = "QueryOptionsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public enum QueryOptionsType
	{
		// Token: 0x04002D58 RID: 11608
		None = 0,
		// Token: 0x04002D59 RID: 11609
		Suggestions = 1,
		// Token: 0x04002D5A RID: 11610
		Results = 2,
		// Token: 0x04002D5B RID: 11611
		Refiners = 4,
		// Token: 0x04002D5C RID: 11612
		SearchTerms = 8,
		// Token: 0x04002D5D RID: 11613
		ExplicitSearch = 16,
		// Token: 0x04002D5E RID: 11614
		SuggestionsPrimer = 32,
		// Token: 0x04002D5F RID: 11615
		AllowFuzzing = 64
	}
}
