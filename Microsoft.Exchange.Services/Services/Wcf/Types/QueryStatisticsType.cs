using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Search.Query;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B33 RID: 2867
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "QueryStatisticsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class QueryStatisticsType
	{
		// Token: 0x0600514B RID: 20811 RVA: 0x0010A584 File Offset: 0x00108784
		internal QueryStatisticsType()
		{
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0010A58C File Offset: 0x0010878C
		internal QueryStatisticsType(QueryStatistics queryStatistics)
		{
			this.StoreByPassed = queryStatistics.StoreBypassed;
			this.Version = queryStatistics.Version;
			this.StartTime = queryStatistics.StartTime.ToString("MM/dd/yyyy hh:mm:ss.fff tt", CultureInfo.InvariantCulture);
			this.QueryTime = (double)((queryStatistics.EndTime.Ticks - queryStatistics.StartTime.Ticks) / 10000L);
			IReadOnlyCollection<QueryExecutionStep> steps = queryStatistics.Steps;
			if (steps != null)
			{
				this.QueryStepsCount = steps.Count;
				this.Steps = new QueryExecutionStepType[steps.Count];
				int num = 0;
				foreach (QueryExecutionStep queryExecutionStep in steps)
				{
					this.Steps[num++] = new QueryExecutionStepType(queryExecutionStep);
				}
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x0600514D RID: 20813 RVA: 0x0010A678 File Offset: 0x00108878
		// (set) Token: 0x0600514E RID: 20814 RVA: 0x0010A680 File Offset: 0x00108880
		[DataMember]
		public bool StoreByPassed { get; set; }

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600514F RID: 20815 RVA: 0x0010A689 File Offset: 0x00108889
		// (set) Token: 0x06005150 RID: 20816 RVA: 0x0010A691 File Offset: 0x00108891
		[DataMember]
		public int Version { get; set; }

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06005151 RID: 20817 RVA: 0x0010A69A File Offset: 0x0010889A
		// (set) Token: 0x06005152 RID: 20818 RVA: 0x0010A6A2 File Offset: 0x001088A2
		[DataMember]
		public string StartTime { get; set; }

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x0010A6AB File Offset: 0x001088AB
		// (set) Token: 0x06005154 RID: 20820 RVA: 0x0010A6B3 File Offset: 0x001088B3
		[DataMember]
		public double QueryTime { get; set; }

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06005155 RID: 20821 RVA: 0x0010A6BC File Offset: 0x001088BC
		// (set) Token: 0x06005156 RID: 20822 RVA: 0x0010A6C4 File Offset: 0x001088C4
		[DataMember]
		public int QueryStepsCount { get; set; }

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06005157 RID: 20823 RVA: 0x0010A6CD File Offset: 0x001088CD
		// (set) Token: 0x06005158 RID: 20824 RVA: 0x0010A6D5 File Offset: 0x001088D5
		[DataMember]
		public QueryExecutionStepType[] Steps { get; set; }
	}
}
