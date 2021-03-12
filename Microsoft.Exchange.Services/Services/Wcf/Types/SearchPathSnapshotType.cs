using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B3B RID: 2875
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class SearchPathSnapshotType
	{
		// Token: 0x06005177 RID: 20855 RVA: 0x0010A81D File Offset: 0x00108A1D
		public SearchPathSnapshotType()
		{
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0010A825 File Offset: 0x00108A25
		internal SearchPathSnapshotType(QueryOptionsType queryOptionType, SearchPerfMarkerContainer perfMarkerContainer)
		{
			this.QueryOptions = queryOptionType;
			this.PerfMarkerContainer = perfMarkerContainer;
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x0010A83B File Offset: 0x00108A3B
		// (set) Token: 0x0600517A RID: 20858 RVA: 0x0010A843 File Offset: 0x00108A43
		[DataMember]
		public QueryOptionsType QueryOptions { get; set; }

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x0010A84C File Offset: 0x00108A4C
		[DataMember]
		public InstantSearchPerfMarkerType[] PerfMarkers
		{
			get
			{
				return this.PerfMarkerContainer.MarkerCollection.ToArray();
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x0010A85E File Offset: 0x00108A5E
		// (set) Token: 0x0600517D RID: 20861 RVA: 0x0010A866 File Offset: 0x00108A66
		internal SearchPerfMarkerContainer PerfMarkerContainer { get; set; }
	}
}
