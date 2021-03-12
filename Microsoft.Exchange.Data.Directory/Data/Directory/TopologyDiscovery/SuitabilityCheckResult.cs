using System;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006C7 RID: 1735
	internal class SuitabilityCheckResult
	{
		// Token: 0x06005021 RID: 20513 RVA: 0x001271F4 File Offset: 0x001253F4
		public SuitabilityCheckResult()
		{
			this.IsEnabled = false;
			this.IsDNSEntryAvailable = false;
			this.IsReachableByTCPConnection = ADServerRole.None;
			this.IsSynchronized = ADServerRole.None;
			this.IsPDC = false;
			this.IsSACLRightAvailable = false;
			this.IsCriticalDataAvailable = false;
			this.IsNetlogonAllowed = ADServerRole.None;
			this.IsOSVersionSuitable = false;
			this.WritableNC = string.Empty;
			this.SchemaNC = string.Empty;
			this.RootNC = string.Empty;
		}

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x06005022 RID: 20514 RVA: 0x00127267 File Offset: 0x00125467
		// (set) Token: 0x06005023 RID: 20515 RVA: 0x0012726F File Offset: 0x0012546F
		public string ConfigNC { get; set; }

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x00127278 File Offset: 0x00125478
		// (set) Token: 0x06005025 RID: 20517 RVA: 0x00127280 File Offset: 0x00125480
		public string RootNC { get; set; }

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06005026 RID: 20518 RVA: 0x00127289 File Offset: 0x00125489
		// (set) Token: 0x06005027 RID: 20519 RVA: 0x00127291 File Offset: 0x00125491
		public string SchemaNC { get; set; }

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x0012729A File Offset: 0x0012549A
		// (set) Token: 0x06005029 RID: 20521 RVA: 0x001272A2 File Offset: 0x001254A2
		public string WritableNC { get; set; }

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x001272AB File Offset: 0x001254AB
		// (set) Token: 0x0600502B RID: 20523 RVA: 0x001272B3 File Offset: 0x001254B3
		public bool IsDNSEntryAvailable { get; set; }

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x001272BC File Offset: 0x001254BC
		// (set) Token: 0x0600502D RID: 20525 RVA: 0x001272C4 File Offset: 0x001254C4
		public ADServerRole IsReachableByTCPConnection { get; set; }

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x001272CD File Offset: 0x001254CD
		// (set) Token: 0x0600502F RID: 20527 RVA: 0x001272D5 File Offset: 0x001254D5
		public bool IsEnabled { get; set; }

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x001272DE File Offset: 0x001254DE
		// (set) Token: 0x06005031 RID: 20529 RVA: 0x001272E6 File Offset: 0x001254E6
		public bool IsSACLRightAvailable { get; set; }

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x001272EF File Offset: 0x001254EF
		// (set) Token: 0x06005033 RID: 20531 RVA: 0x001272F7 File Offset: 0x001254F7
		public bool IsCriticalDataAvailable { get; set; }

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x00127300 File Offset: 0x00125500
		// (set) Token: 0x06005035 RID: 20533 RVA: 0x00127308 File Offset: 0x00125508
		public ADServerRole IsSynchronized { get; set; }

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x06005036 RID: 20534 RVA: 0x00127311 File Offset: 0x00125511
		// (set) Token: 0x06005037 RID: 20535 RVA: 0x00127319 File Offset: 0x00125519
		public bool IsOSVersionSuitable { get; set; }

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x06005038 RID: 20536 RVA: 0x00127322 File Offset: 0x00125522
		// (set) Token: 0x06005039 RID: 20537 RVA: 0x0012732A File Offset: 0x0012552A
		public bool IsInMM { get; set; }

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x0600503A RID: 20538 RVA: 0x00127333 File Offset: 0x00125533
		// (set) Token: 0x0600503B RID: 20539 RVA: 0x0012733B File Offset: 0x0012553B
		public ADServerRole IsNetlogonAllowed { get; set; }

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x0600503C RID: 20540 RVA: 0x00127344 File Offset: 0x00125544
		// (set) Token: 0x0600503D RID: 20541 RVA: 0x0012734C File Offset: 0x0012554C
		public bool IsPDC { get; set; }

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x0600503E RID: 20542 RVA: 0x00127355 File Offset: 0x00125555
		// (set) Token: 0x0600503F RID: 20543 RVA: 0x0012735D File Offset: 0x0012555D
		public bool IsReadOnlyDC { get; set; }

		// Token: 0x06005040 RID: 20544 RVA: 0x00127368 File Offset: 0x00125568
		public override string ToString()
		{
			if (Globals.IsDatacenter)
			{
				return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", new object[]
				{
					Convert.ToInt16(this.IsEnabled),
					(int)this.IsReachableByTCPConnection,
					(int)this.IsSynchronized,
					Convert.ToInt16((this.IsSynchronized & ADServerRole.GlobalCatalog) != ADServerRole.None),
					Convert.ToInt16(this.IsPDC),
					Convert.ToInt16(this.IsSACLRightAvailable),
					Convert.ToInt16(this.IsCriticalDataAvailable),
					(int)this.IsNetlogonAllowed,
					Convert.ToInt16(this.IsOSVersionSuitable),
					Convert.ToInt16(this.IsInMM)
				});
			}
			return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", new object[]
			{
				Convert.ToInt16(this.IsEnabled),
				(int)this.IsReachableByTCPConnection,
				(int)this.IsSynchronized,
				Convert.ToInt16((this.IsSynchronized & ADServerRole.GlobalCatalog) != ADServerRole.None),
				Convert.ToInt16(this.IsPDC),
				Convert.ToInt16(this.IsSACLRightAvailable),
				Convert.ToInt16(this.IsCriticalDataAvailable),
				(int)this.IsNetlogonAllowed,
				Convert.ToInt16(this.IsOSVersionSuitable)
			});
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x00127504 File Offset: 0x00125704
		internal bool IsSuitable(ADServerRole role)
		{
			return this.IsEnabled && (this.IsReachableByTCPConnection & role) == role && (this.IsSynchronized & role) == role && this.IsSACLRightAvailable && this.IsCriticalDataAvailable && (this.IsNetlogonAllowed & role) == role && this.IsOSVersionSuitable && (!Globals.IsDatacenter || !this.IsInMM);
		}
	}
}
