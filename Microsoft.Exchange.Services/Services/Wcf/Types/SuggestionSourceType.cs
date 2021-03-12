using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B3D RID: 2877
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Flags]
	[XmlType(TypeName = "SuggestionSourceType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SuggestionSourceType
	{
		// Token: 0x04002D90 RID: 11664
		None = 0,
		// Token: 0x04002D91 RID: 11665
		RecentSearches = 1,
		// Token: 0x04002D92 RID: 11666
		Spelling = 2,
		// Token: 0x04002D93 RID: 11667
		Synonyms = 4,
		// Token: 0x04002D94 RID: 11668
		Nicknames = 8,
		// Token: 0x04002D95 RID: 11669
		TopN = 16,
		// Token: 0x04002D96 RID: 11670
		Fuzzy = 26,
		// Token: 0x04002D97 RID: 11671
		All = 31
	}
}
