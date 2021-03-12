using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B2D RID: 2861
	[XmlType(TypeName = "InstantSearchPayloadType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class InstantSearchPayloadType
	{
		// Token: 0x060050EF RID: 20719 RVA: 0x0010A11A File Offset: 0x0010831A
		public InstantSearchPayloadType()
		{
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x0010A122 File Offset: 0x00108322
		internal InstantSearchPayloadType(string searchSessionId, long searchRequestId, InstantSearchResultType resultDataType, SearchPerfMarkerContainer perfMarkerContainer)
		{
			this.SearchSessionId = searchSessionId;
			this.SearchRequestId = searchRequestId;
			this.ResultType = resultDataType;
			this.SearchPerfMarkerContainer = perfMarkerContainer;
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x0010A147 File Offset: 0x00108347
		// (set) Token: 0x060050F2 RID: 20722 RVA: 0x0010A14F File Offset: 0x0010834F
		[DataMember(IsRequired = true)]
		public string SearchSessionId { get; set; }

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x060050F3 RID: 20723 RVA: 0x0010A158 File Offset: 0x00108358
		// (set) Token: 0x060050F4 RID: 20724 RVA: 0x0010A160 File Offset: 0x00108360
		[DataMember(IsRequired = true)]
		public long SearchRequestId { get; set; }

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x060050F5 RID: 20725 RVA: 0x0010A169 File Offset: 0x00108369
		// (set) Token: 0x060050F6 RID: 20726 RVA: 0x0010A171 File Offset: 0x00108371
		[DataMember(IsRequired = true)]
		public InstantSearchResultType ResultType { get; set; }

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x060050F7 RID: 20727 RVA: 0x0010A17A File Offset: 0x0010837A
		// (set) Token: 0x060050F8 RID: 20728 RVA: 0x0010A182 File Offset: 0x00108382
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public bool QueryProcessingComplete { get; set; }

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x060050F9 RID: 20729 RVA: 0x0010A18B File Offset: 0x0010838B
		// (set) Token: 0x060050FA RID: 20730 RVA: 0x0010A193 File Offset: 0x00108393
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public QueryStatisticsType QueryStatistics { get; set; }

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x0010A19C File Offset: 0x0010839C
		// (set) Token: 0x060050FC RID: 20732 RVA: 0x0010A1A4 File Offset: 0x001083A4
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ItemType[] Items { get; set; }

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x060050FD RID: 20733 RVA: 0x0010A1AD File Offset: 0x001083AD
		// (set) Token: 0x060050FE RID: 20734 RVA: 0x0010A1B5 File Offset: 0x001083B5
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public EwsCalendarItemType[] CalendarItems { get; set; }

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x0010A1BE File Offset: 0x001083BE
		// (set) Token: 0x06005100 RID: 20736 RVA: 0x0010A1C6 File Offset: 0x001083C6
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public SearchSuggestionType[] SearchSuggestions { get; set; }

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06005101 RID: 20737 RVA: 0x0010A1CF File Offset: 0x001083CF
		// (set) Token: 0x06005102 RID: 20738 RVA: 0x0010A1D7 File Offset: 0x001083D7
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ConversationType[] Conversations { get; set; }

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x0010A1E0 File Offset: 0x001083E0
		// (set) Token: 0x06005104 RID: 20740 RVA: 0x0010A1E8 File Offset: 0x001083E8
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public Persona[] PersonaItems { get; set; }

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06005105 RID: 20741 RVA: 0x0010A1F1 File Offset: 0x001083F1
		// (set) Token: 0x06005106 RID: 20742 RVA: 0x0010A1F9 File Offset: 0x001083F9
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public RefinerDataType[] RefinerData { get; set; }

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x0010A202 File Offset: 0x00108402
		// (set) Token: 0x06005108 RID: 20744 RVA: 0x0010A20A File Offset: 0x0010840A
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string[] Errors { get; set; }

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06005109 RID: 20745 RVA: 0x0010A213 File Offset: 0x00108413
		// (set) Token: 0x0600510A RID: 20746 RVA: 0x0010A21B File Offset: 0x0010841B
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string[] SearchTerms
		{
			get
			{
				return this.searchTerms;
			}
			set
			{
				this.searchTerms = value;
				this.ResultType |= InstantSearchResultType.SearchTerms;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x0600510B RID: 20747 RVA: 0x0010A233 File Offset: 0x00108433
		// (set) Token: 0x0600510C RID: 20748 RVA: 0x0010A23B File Offset: 0x0010843B
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public InstantSearchPerfMarkerType[] PerfMarkers { get; set; }

		// Token: 0x0600510D RID: 20749 RVA: 0x0010A244 File Offset: 0x00108444
		[OnSerializing]
		private void StampSerializationTime(StreamingContext context)
		{
			this.SearchPerfMarkerContainer.SetPerfMarker(InstantSearchPerfKey.NotificationSerializationTime);
			this.PerfMarkers = this.SearchPerfMarkerContainer.GetMarkerSnapshot();
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x0600510E RID: 20750 RVA: 0x0010A263 File Offset: 0x00108463
		// (set) Token: 0x0600510F RID: 20751 RVA: 0x0010A26B File Offset: 0x0010846B
		[XmlIgnore]
		[IgnoreDataMember]
		internal SearchPerfMarkerContainer SearchPerfMarkerContainer { get; private set; }

		// Token: 0x04002D46 RID: 11590
		private string[] searchTerms;
	}
}
