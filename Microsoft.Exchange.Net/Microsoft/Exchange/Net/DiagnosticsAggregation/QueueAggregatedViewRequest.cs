using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000838 RID: 2104
	[DataContract]
	internal class QueueAggregatedViewRequest
	{
		// Token: 0x06002CA5 RID: 11429 RVA: 0x00065096 File Offset: 0x00063296
		public QueueAggregatedViewRequest(QueueDigestGroupBy groupByKey, DetailsLevel detailsLevel, string queueFilter)
		{
			this.GroupByKey = groupByKey.ToString();
			this.DetailsLevel = detailsLevel.ToString();
			this.QueueFilter = queueFilter;
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x000650C7 File Offset: 0x000632C7
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x000650CF File Offset: 0x000632CF
		[DataMember(IsRequired = true)]
		public string QueueFilter { get; private set; }

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x000650D8 File Offset: 0x000632D8
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x000650E0 File Offset: 0x000632E0
		[DataMember(IsRequired = true)]
		public string GroupByKey { get; private set; }

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000650E9 File Offset: 0x000632E9
		// (set) Token: 0x06002CAB RID: 11435 RVA: 0x000650F1 File Offset: 0x000632F1
		[DataMember(IsRequired = true)]
		public string DetailsLevel { get; private set; }
	}
}
