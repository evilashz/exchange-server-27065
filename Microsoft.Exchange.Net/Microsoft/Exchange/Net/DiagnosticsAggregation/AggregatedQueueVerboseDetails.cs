using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x0200083D RID: 2109
	[DataContract]
	[Serializable]
	public class AggregatedQueueVerboseDetails
	{
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000653D9 File Offset: 0x000635D9
		// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x000653E1 File Offset: 0x000635E1
		[DataMember(IsRequired = true)]
		public string QueueIdentity { get; set; }

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000653EA File Offset: 0x000635EA
		// (set) Token: 0x06002CF3 RID: 11507 RVA: 0x000653F2 File Offset: 0x000635F2
		[DataMember(IsRequired = true)]
		public string ServerIdentity { get; set; }

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000653FB File Offset: 0x000635FB
		// (set) Token: 0x06002CF5 RID: 11509 RVA: 0x00065403 File Offset: 0x00063603
		[DataMember(IsRequired = true)]
		public int MessageCount { get; set; }

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x0006540C File Offset: 0x0006360C
		// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x00065414 File Offset: 0x00063614
		[DataMember(IsRequired = true)]
		public int DeferredMessageCount { get; set; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x0006541D File Offset: 0x0006361D
		// (set) Token: 0x06002CF9 RID: 11513 RVA: 0x00065425 File Offset: 0x00063625
		[DataMember(IsRequired = true)]
		public int LockedMessageCount { get; set; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x0006542E File Offset: 0x0006362E
		// (set) Token: 0x06002CFB RID: 11515 RVA: 0x00065436 File Offset: 0x00063636
		[DataMember(IsRequired = true)]
		public double IncomingRate { get; set; }

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x0006543F File Offset: 0x0006363F
		// (set) Token: 0x06002CFD RID: 11517 RVA: 0x00065447 File Offset: 0x00063647
		[DataMember(IsRequired = true)]
		public double OutgoingRate { get; set; }

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x00065450 File Offset: 0x00063650
		// (set) Token: 0x06002CFF RID: 11519 RVA: 0x00065458 File Offset: 0x00063658
		[DataMember(IsRequired = true)]
		public double Velocity { get; set; }

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x00065461 File Offset: 0x00063661
		// (set) Token: 0x06002D01 RID: 11521 RVA: 0x00065469 File Offset: 0x00063669
		[DataMember(IsRequired = true)]
		public string NextHopDomain { get; set; }

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x00065472 File Offset: 0x00063672
		// (set) Token: 0x06002D03 RID: 11523 RVA: 0x0006547A File Offset: 0x0006367A
		[DataMember(IsRequired = true)]
		public string NextHopCategory { get; set; }

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x00065483 File Offset: 0x00063683
		// (set) Token: 0x06002D05 RID: 11525 RVA: 0x0006548B File Offset: 0x0006368B
		[DataMember(IsRequired = true)]
		public string DeliveryType { get; set; }

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x00065494 File Offset: 0x00063694
		// (set) Token: 0x06002D07 RID: 11527 RVA: 0x0006549C File Offset: 0x0006369C
		[DataMember(IsRequired = true)]
		public string Status { get; set; }

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002D08 RID: 11528 RVA: 0x000654A5 File Offset: 0x000636A5
		// (set) Token: 0x06002D09 RID: 11529 RVA: 0x000654AD File Offset: 0x000636AD
		[DataMember(IsRequired = true)]
		public string RiskLevel { get; set; }

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000654B6 File Offset: 0x000636B6
		// (set) Token: 0x06002D0B RID: 11531 RVA: 0x000654BE File Offset: 0x000636BE
		[DataMember(IsRequired = true)]
		public string OutboundIPPool { get; set; }

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000654C7 File Offset: 0x000636C7
		// (set) Token: 0x06002D0D RID: 11533 RVA: 0x000654CF File Offset: 0x000636CF
		[DataMember(IsRequired = true)]
		public string LastError { get; set; }

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000654D8 File Offset: 0x000636D8
		// (set) Token: 0x06002D0F RID: 11535 RVA: 0x000654E0 File Offset: 0x000636E0
		[DataMember(IsRequired = true)]
		public Guid NextHopConnector { get; set; }

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x000654E9 File Offset: 0x000636E9
		// (set) Token: 0x06002D11 RID: 11537 RVA: 0x000654F1 File Offset: 0x000636F1
		[DataMember(IsRequired = true)]
		public string TlsDomain { get; set; }
	}
}
