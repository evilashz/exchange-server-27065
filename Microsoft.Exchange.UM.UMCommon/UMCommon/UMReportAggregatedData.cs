using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200014B RID: 331
	[DataContract(Name = "UMReportAggregatedData", Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData")]
	internal class UMReportAggregatedData : IUMAggregatedData
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00027CCC File Offset: 0x00025ECC
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x00027CD4 File Offset: 0x00025ED4
		[DataMember(Name = "WaterMark")]
		public DateTime WaterMark { get; private set; }

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00027CE0 File Offset: 0x00025EE0
		public UMReportAggregatedData()
		{
			this.cleanUpNeeded = false;
			this.aggregatedData = new AggregatedDataDictionary();
			this.WaterMark = default(DateTime);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00027D14 File Offset: 0x00025F14
		public void AddCDR(CDRData cdrData)
		{
			ValidateArgument.NotNull(cdrData, "cdrData");
			UMReportTuple[] tuplesToAddInReport = UMReportTuple.GetTuplesToAddInReport(cdrData);
			foreach (UMReportTuple key in tuplesToAddInReport)
			{
				UMReportTupleData umreportTupleData;
				if (!this.aggregatedData.TryGetValue(key, out umreportTupleData))
				{
					umreportTupleData = new UMReportTupleData();
					this.aggregatedData.Add(key, umreportTupleData);
				}
				umreportTupleData.AddCDR(cdrData);
			}
			this.WaterMark = cdrData.CreationTime;
			this.cleanUpNeeded = true;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00027D8C File Offset: 0x00025F8C
		public UMReportRawCounters[] QueryAggregatedData(Guid dialPlanGuid, Guid gatewayGuid, GroupBy groupBy)
		{
			UMReportTuple key = new UMReportTuple(dialPlanGuid, gatewayGuid);
			UMReportTupleData umreportTupleData;
			if (this.aggregatedData.TryGetValue(key, out umreportTupleData))
			{
				return umreportTupleData.QueryReport(groupBy);
			}
			return null;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00027DBC File Offset: 0x00025FBC
		public void Cleanup(OrganizationId orgId)
		{
			if (this.aggregatedData.Count > 0)
			{
				AggregatedDataDictionary aggregatedDataDictionary = new AggregatedDataDictionary(this.aggregatedData.Count);
				foreach (UMReportTuple umreportTuple in this.aggregatedData.Keys)
				{
					if (!umreportTuple.ShouldRemoveFromReport(orgId))
					{
						UMReportTupleData umreportTupleData = this.aggregatedData[umreportTuple];
						umreportTupleData.CleanUp();
						aggregatedDataDictionary[umreportTuple] = umreportTupleData;
					}
				}
				this.aggregatedData = aggregatedDataDictionary;
			}
			this.cleanUpNeeded = false;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00027E60 File Offset: 0x00026060
		[OnSerializing]
		private void Cleanup(StreamingContext context)
		{
			if (this.cleanUpNeeded)
			{
				throw new InvalidOperationException("Aggregated data is being serialized without cleanup.");
			}
		}

		// Token: 0x040005C6 RID: 1478
		private bool cleanUpNeeded;

		// Token: 0x040005C7 RID: 1479
		[DataMember(Name = "AggregatedData")]
		private AggregatedDataDictionary aggregatedData;
	}
}
