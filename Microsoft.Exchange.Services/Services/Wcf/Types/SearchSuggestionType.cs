using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B3C RID: 2876
	[XmlType(TypeName = "SearchSuggestionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SearchSuggestionType
	{
		// Token: 0x0600517E RID: 20862 RVA: 0x0010A86F File Offset: 0x00108A6F
		public SearchSuggestionType()
		{
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x0010A877 File Offset: 0x00108A77
		internal SearchSuggestionType(string suggestedQuery, double weight, SuggestionSourceType suggestionSource)
		{
			this.SuggestedQuery = suggestedQuery;
			this.Weight = weight;
			this.SuggestionSource = suggestionSource;
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x0010A894 File Offset: 0x00108A94
		// (set) Token: 0x06005181 RID: 20865 RVA: 0x0010A89C File Offset: 0x00108A9C
		[DataMember]
		public string SuggestedQuery { get; set; }

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06005182 RID: 20866 RVA: 0x0010A8A5 File Offset: 0x00108AA5
		// (set) Token: 0x06005183 RID: 20867 RVA: 0x0010A8AD File Offset: 0x00108AAD
		[DataMember]
		public double Weight { get; set; }

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06005184 RID: 20868 RVA: 0x0010A8B6 File Offset: 0x00108AB6
		// (set) Token: 0x06005185 RID: 20869 RVA: 0x0010A8BE File Offset: 0x00108ABE
		[DataMember]
		public SuggestionSourceType SuggestionSource { get; set; }
	}
}
