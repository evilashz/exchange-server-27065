using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x0200083B RID: 2107
	[DataContract]
	public class AggregatedQueueInfo
	{
		// Token: 0x06002CD5 RID: 11477 RVA: 0x0006525C File Offset: 0x0006345C
		public AggregatedQueueInfo()
		{
			this.NormalDetails = new List<AggregatedQueueNormalDetails>();
			this.VerboseDetails = new List<AggregatedQueueVerboseDetails>();
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x0006527A File Offset: 0x0006347A
		// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x00065282 File Offset: 0x00063482
		[DataMember(IsRequired = true)]
		public string GroupByValue { get; set; }

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x0006528B File Offset: 0x0006348B
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x00065293 File Offset: 0x00063493
		[DataMember(IsRequired = true)]
		public int MessageCount { get; set; }

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x0006529C File Offset: 0x0006349C
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x000652A4 File Offset: 0x000634A4
		[DataMember(IsRequired = true)]
		public int DeferredMessageCount { get; set; }

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000652AD File Offset: 0x000634AD
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000652B5 File Offset: 0x000634B5
		[DataMember(IsRequired = true)]
		public int LockedMessageCount { get; set; }

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000652BE File Offset: 0x000634BE
		// (set) Token: 0x06002CDF RID: 11487 RVA: 0x000652C6 File Offset: 0x000634C6
		[DataMember(IsRequired = true)]
		public int StaleMessageCount { get; set; }

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000652CF File Offset: 0x000634CF
		// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x000652D7 File Offset: 0x000634D7
		[DataMember(IsRequired = true)]
		public double IncomingRate { get; set; }

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000652E0 File Offset: 0x000634E0
		// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x000652E8 File Offset: 0x000634E8
		[DataMember(IsRequired = true)]
		public double OutgoingRate { get; set; }

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000652F1 File Offset: 0x000634F1
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x000652F9 File Offset: 0x000634F9
		[DataMember(IsRequired = true)]
		public List<AggregatedQueueNormalDetails> NormalDetails { get; set; }

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x00065302 File Offset: 0x00063502
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x0006530A File Offset: 0x0006350A
		[DataMember(IsRequired = true)]
		public List<AggregatedQueueVerboseDetails> VerboseDetails { get; set; }

		// Token: 0x06002CE8 RID: 11496 RVA: 0x00065314 File Offset: 0x00063514
		public AggregatedQueueInfo Clone()
		{
			return new AggregatedQueueInfo
			{
				GroupByValue = this.GroupByValue,
				MessageCount = this.MessageCount,
				DeferredMessageCount = this.DeferredMessageCount,
				LockedMessageCount = this.LockedMessageCount,
				StaleMessageCount = this.StaleMessageCount,
				IncomingRate = this.IncomingRate,
				OutgoingRate = this.OutgoingRate,
				NormalDetails = new List<AggregatedQueueNormalDetails>(this.NormalDetails),
				VerboseDetails = new List<AggregatedQueueVerboseDetails>(this.VerboseDetails)
			};
		}
	}
}
