using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001D1 RID: 465
	internal struct WLMResourceStatsData
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0002B585 File Offset: 0x00029785
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x0002B58D File Offset: 0x0002978D
		public string OwnerResourceName { get; set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0002B596 File Offset: 0x00029796
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x0002B59E File Offset: 0x0002979E
		public Guid OwnerResourceGuid { get; set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0002B5A7 File Offset: 0x000297A7
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x0002B5AF File Offset: 0x000297AF
		public string OwnerResourceType { get; set; }

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0002B5B8 File Offset: 0x000297B8
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x0002B5C0 File Offset: 0x000297C0
		public string WlmResourceKey { get; set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0002B5C9 File Offset: 0x000297C9
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x0002B5D1 File Offset: 0x000297D1
		public string LoadState { get; set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0002B5DA File Offset: 0x000297DA
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x0002B5E2 File Offset: 0x000297E2
		public double LoadRatio { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0002B5EB File Offset: 0x000297EB
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x0002B5F3 File Offset: 0x000297F3
		public string Metric { get; set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0002B5FC File Offset: 0x000297FC
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x0002B604 File Offset: 0x00029804
		public double DynamicCapacity { get; set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0002B60D File Offset: 0x0002980D
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x0002B615 File Offset: 0x00029815
		public string IsDisabled { get; set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0002B61E File Offset: 0x0002981E
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x0002B626 File Offset: 0x00029826
		public string DynamicThrottingDisabled { get; set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0002B62F File Offset: 0x0002982F
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x0002B637 File Offset: 0x00029837
		public TimeSpan TimeInterval { get; set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0002B640 File Offset: 0x00029840
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x0002B648 File Offset: 0x00029848
		public uint UnderloadedCount { get; set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0002B651 File Offset: 0x00029851
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x0002B659 File Offset: 0x00029859
		public uint FullCount { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0002B662 File Offset: 0x00029862
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x0002B66A File Offset: 0x0002986A
		public uint OverloadedCount { get; set; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0002B673 File Offset: 0x00029873
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x0002B67B File Offset: 0x0002987B
		public uint CriticalCount { get; set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0002B684 File Offset: 0x00029884
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x0002B68C File Offset: 0x0002988C
		public uint UnknownCount { get; set; }
	}
}
