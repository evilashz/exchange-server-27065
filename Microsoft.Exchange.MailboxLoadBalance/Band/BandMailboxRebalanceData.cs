using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x02000015 RID: 21
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BandMailboxRebalanceData
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000536A File Offset: 0x0000356A
		public BandMailboxRebalanceData(LoadContainer sourceDatabase, LoadContainer targetDatabase, LoadMetricStorage rebalanceInformation)
		{
			this.SourceDatabase = sourceDatabase.GetShallowCopy();
			this.TargetDatabase = targetDatabase.GetShallowCopy();
			this.RebalanceInformation = rebalanceInformation;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005391 File Offset: 0x00003591
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00005399 File Offset: 0x00003599
		[DataMember(Order = 1)]
		public string ConstraintSetIdentity { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000053A2 File Offset: 0x000035A2
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000053AA File Offset: 0x000035AA
		[DataMember(Order = 0)]
		public string RebalanceBatchName { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000053B3 File Offset: 0x000035B3
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000053BB File Offset: 0x000035BB
		[DataMember(Order = 0)]
		public LoadMetricStorage RebalanceInformation { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000053C4 File Offset: 0x000035C4
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000053CC File Offset: 0x000035CC
		[DataMember(Order = 0)]
		public LoadContainer SourceDatabase { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000053D5 File Offset: 0x000035D5
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000053DD File Offset: 0x000035DD
		[DataMember(Order = 0)]
		public LoadContainer TargetDatabase { get; private set; }

		// Token: 0x060000CE RID: 206 RVA: 0x000053E6 File Offset: 0x000035E6
		public void ConvertToFromSerializationFormat()
		{
			this.RebalanceInformation.ConvertFromSerializationFormat();
			this.SourceDatabase.ConvertFromSerializationFormat();
			this.TargetDatabase.ConvertFromSerializationFormat();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000540C File Offset: 0x0000360C
		public BandMailboxRebalanceData ToSerializationFormat(bool convertBandToBandData)
		{
			LoadMetricStorage loadMetricStorage = new LoadMetricStorage(this.RebalanceInformation);
			loadMetricStorage.ConvertToSerializationMetrics(convertBandToBandData);
			return new BandMailboxRebalanceData((LoadContainer)this.SourceDatabase.ToSerializationFormat(convertBandToBandData), (LoadContainer)this.TargetDatabase.ToSerializationFormat(convertBandToBandData), loadMetricStorage)
			{
				RebalanceBatchName = this.RebalanceBatchName,
				ConstraintSetIdentity = this.ConstraintSetIdentity
			};
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005470 File Offset: 0x00003670
		public override string ToString()
		{
			return string.Format("Rebalance[CSET: {0}; From: {1}; To: {2}; {3}", new object[]
			{
				this.ConstraintSetIdentity,
				this.SourceDatabase.Guid,
				this.TargetDatabase.Guid,
				this.RebalanceInformation
			});
		}
	}
}
