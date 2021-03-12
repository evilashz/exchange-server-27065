using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x0200083A RID: 2106
	[DataContract]
	[Serializable]
	public class LocalQueueInfo
	{
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x0006511A File Offset: 0x0006331A
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x00065122 File Offset: 0x00063322
		[DataMember(IsRequired = true)]
		public string QueueIdentity { get; set; }

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x0006512B File Offset: 0x0006332B
		// (set) Token: 0x06002CB2 RID: 11442 RVA: 0x00065133 File Offset: 0x00063333
		[DataMember(IsRequired = true)]
		public string ServerIdentity { get; set; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x0006513C File Offset: 0x0006333C
		// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x00065144 File Offset: 0x00063344
		[DataMember(IsRequired = true)]
		public int MessageCount { get; set; }

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x0006514D File Offset: 0x0006334D
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x00065155 File Offset: 0x00063355
		[DataMember(IsRequired = true)]
		public int DeferredMessageCount { get; set; }

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x0006515E File Offset: 0x0006335E
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x00065166 File Offset: 0x00063366
		[DataMember(IsRequired = true)]
		public int LockedMessageCount { get; set; }

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x0006516F File Offset: 0x0006336F
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x00065177 File Offset: 0x00063377
		[DataMember(IsRequired = true)]
		public double IncomingRate { get; set; }

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x00065180 File Offset: 0x00063380
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x00065188 File Offset: 0x00063388
		[DataMember(IsRequired = true)]
		public double OutgoingRate { get; set; }

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x00065191 File Offset: 0x00063391
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x00065199 File Offset: 0x00063399
		[DataMember(IsRequired = true)]
		public double Velocity { get; set; }

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000651A2 File Offset: 0x000633A2
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x000651AA File Offset: 0x000633AA
		[DataMember(IsRequired = true)]
		public string NextHopDomain { get; set; }

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000651B3 File Offset: 0x000633B3
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x000651BB File Offset: 0x000633BB
		[DataMember(IsRequired = true)]
		public string NextHopCategory { get; set; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x000651C4 File Offset: 0x000633C4
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x000651CC File Offset: 0x000633CC
		[DataMember(IsRequired = true)]
		public string DeliveryType { get; set; }

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000651D5 File Offset: 0x000633D5
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x000651DD File Offset: 0x000633DD
		[DataMember(IsRequired = true)]
		public string Status { get; set; }

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000651E6 File Offset: 0x000633E6
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x000651EE File Offset: 0x000633EE
		[DataMember(IsRequired = true)]
		public string RiskLevel { get; set; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000651F7 File Offset: 0x000633F7
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x000651FF File Offset: 0x000633FF
		[DataMember(IsRequired = true)]
		public string OutboundIPPool { get; set; }

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x00065208 File Offset: 0x00063408
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x00065210 File Offset: 0x00063410
		[DataMember(IsRequired = true)]
		public string LastError { get; set; }

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x00065219 File Offset: 0x00063419
		// (set) Token: 0x06002CCE RID: 11470 RVA: 0x00065221 File Offset: 0x00063421
		[DataMember(IsRequired = true)]
		public string NextHopKey { get; set; }

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x0006522A File Offset: 0x0006342A
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x00065232 File Offset: 0x00063432
		[DataMember(IsRequired = true)]
		public Guid NextHopConnector { get; set; }

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x0006523B File Offset: 0x0006343B
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x00065243 File Offset: 0x00063443
		[DataMember(IsRequired = true)]
		public string TlsDomain { get; set; }

		// Token: 0x06002CD3 RID: 11475 RVA: 0x0006524C File Offset: 0x0006344C
		public override string ToString()
		{
			return this.QueueIdentity;
		}
	}
}
