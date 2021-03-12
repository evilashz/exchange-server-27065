using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B2C RID: 2860
	[Flags]
	[XmlType(TypeName = "InstantSearchResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public enum InstantSearchResultType
	{
		// Token: 0x04002D3B RID: 11579
		None = 0,
		// Token: 0x04002D3C RID: 11580
		Suggestions = 1,
		// Token: 0x04002D3D RID: 11581
		ItemResults = 2,
		// Token: 0x04002D3E RID: 11582
		ConversationResults = 4,
		// Token: 0x04002D3F RID: 11583
		Refiners = 8,
		// Token: 0x04002D40 RID: 11584
		SearchTerms = 16,
		// Token: 0x04002D41 RID: 11585
		Errors = 32,
		// Token: 0x04002D42 RID: 11586
		QueryStatistics = 64,
		// Token: 0x04002D43 RID: 11587
		CalendarItemResults = 128,
		// Token: 0x04002D44 RID: 11588
		PersonaResults = 256,
		// Token: 0x04002D45 RID: 11589
		SuggestionsPrimer = 512
	}
}
