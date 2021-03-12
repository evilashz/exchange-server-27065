using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003EE RID: 1006
	[Serializable]
	public class HistoricalSearch
	{
		// Token: 0x06002354 RID: 9044 RVA: 0x0008F438 File Offset: 0x0008D638
		internal HistoricalSearch()
		{
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x0008F440 File Offset: 0x0008D640
		// (set) Token: 0x06002356 RID: 9046 RVA: 0x0008F448 File Offset: 0x0008D648
		public Guid JobId { get; internal set; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x0008F451 File Offset: 0x0008D651
		// (set) Token: 0x06002358 RID: 9048 RVA: 0x0008F459 File Offset: 0x0008D659
		public DateTime SubmitDate { get; internal set; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x0008F462 File Offset: 0x0008D662
		// (set) Token: 0x0600235A RID: 9050 RVA: 0x0008F46A File Offset: 0x0008D66A
		public string ReportTitle { get; set; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x0008F473 File Offset: 0x0008D673
		// (set) Token: 0x0600235C RID: 9052 RVA: 0x0008F47B File Offset: 0x0008D67B
		public JobStatus Status { get; internal set; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0008F484 File Offset: 0x0008D684
		// (set) Token: 0x0600235E RID: 9054 RVA: 0x0008F48C File Offset: 0x0008D68C
		public int Rows { get; internal set; }

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0008F495 File Offset: 0x0008D695
		// (set) Token: 0x06002360 RID: 9056 RVA: 0x0008F49D File Offset: 0x0008D69D
		public int FileRows { get; internal set; }

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x0008F4A6 File Offset: 0x0008D6A6
		// (set) Token: 0x06002362 RID: 9058 RVA: 0x0008F4AE File Offset: 0x0008D6AE
		public string ErrorCode { get; internal set; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x0008F4B7 File Offset: 0x0008D6B7
		// (set) Token: 0x06002364 RID: 9060 RVA: 0x0008F4BF File Offset: 0x0008D6BF
		public string ErrorDescription { get; internal set; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x0008F4C8 File Offset: 0x0008D6C8
		// (set) Token: 0x06002366 RID: 9062 RVA: 0x0008F4D0 File Offset: 0x0008D6D0
		public string FileUrl { get; internal set; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x0008F4D9 File Offset: 0x0008D6D9
		// (set) Token: 0x06002368 RID: 9064 RVA: 0x0008F4E1 File Offset: 0x0008D6E1
		public HistoricalSearchReportType ReportType { get; internal set; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x0008F4EA File Offset: 0x0008D6EA
		// (set) Token: 0x0600236A RID: 9066 RVA: 0x0008F4F2 File Offset: 0x0008D6F2
		public MultiValuedProperty<string> NotifyAddress { get; internal set; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x0008F4FB File Offset: 0x0008D6FB
		// (set) Token: 0x0600236C RID: 9068 RVA: 0x0008F503 File Offset: 0x0008D703
		public DateTime StartDate { get; internal set; }

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x0008F50C File Offset: 0x0008D70C
		// (set) Token: 0x0600236E RID: 9070 RVA: 0x0008F514 File Offset: 0x0008D714
		public DateTime EndDate { get; internal set; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x0008F51D File Offset: 0x0008D71D
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x0008F525 File Offset: 0x0008D725
		public MessageDeliveryStatus DeliveryStatus { get; internal set; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x0008F52E File Offset: 0x0008D72E
		// (set) Token: 0x06002372 RID: 9074 RVA: 0x0008F536 File Offset: 0x0008D736
		public MultiValuedProperty<string> SenderAddress { get; internal set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x0008F53F File Offset: 0x0008D73F
		// (set) Token: 0x06002374 RID: 9076 RVA: 0x0008F547 File Offset: 0x0008D747
		public MultiValuedProperty<string> RecipientAddress { get; internal set; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0008F550 File Offset: 0x0008D750
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x0008F558 File Offset: 0x0008D758
		public string OriginalClientIP { get; internal set; }

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x0008F561 File Offset: 0x0008D761
		// (set) Token: 0x06002378 RID: 9080 RVA: 0x0008F569 File Offset: 0x0008D769
		public MultiValuedProperty<string> MessageID { get; internal set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x0008F572 File Offset: 0x0008D772
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x0008F57A File Offset: 0x0008D77A
		public MultiValuedProperty<Guid> DLPPolicy { get; internal set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x0008F583 File Offset: 0x0008D783
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x0008F58B File Offset: 0x0008D78B
		public MultiValuedProperty<Guid> TransportRule { get; internal set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x0008F594 File Offset: 0x0008D794
		// (set) Token: 0x0600237E RID: 9086 RVA: 0x0008F59C File Offset: 0x0008D79C
		public CultureInfo Locale { get; internal set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x0008F5A5 File Offset: 0x0008D7A5
		// (set) Token: 0x06002380 RID: 9088 RVA: 0x0008F5AD File Offset: 0x0008D7AD
		public MessageDirection Direction { get; internal set; }
	}
}
