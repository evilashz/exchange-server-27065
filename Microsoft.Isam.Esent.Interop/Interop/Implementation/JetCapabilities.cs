using System;

namespace Microsoft.Isam.Esent.Interop.Implementation
{
	// Token: 0x020002DA RID: 730
	internal sealed class JetCapabilities
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0001AE78 File Offset: 0x00019078
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0001AE80 File Offset: 0x00019080
		public bool SupportsServer2003Features { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0001AE89 File Offset: 0x00019089
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0001AE91 File Offset: 0x00019091
		public bool SupportsVistaFeatures { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0001AE9A File Offset: 0x0001909A
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x0001AEA2 File Offset: 0x000190A2
		public bool SupportsWindows7Features { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0001AEAB File Offset: 0x000190AB
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x0001AEB3 File Offset: 0x000190B3
		public bool SupportsWindows8Features { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0001AEBC File Offset: 0x000190BC
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0001AEC4 File Offset: 0x000190C4
		public bool SupportsWindows81Features { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0001AECD File Offset: 0x000190CD
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0001AED5 File Offset: 0x000190D5
		public bool SupportsUnicodePaths { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0001AEDE File Offset: 0x000190DE
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0001AEE6 File Offset: 0x000190E6
		public bool SupportsLargeKeys { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0001AEEF File Offset: 0x000190EF
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0001AEF7 File Offset: 0x000190F7
		public int ColumnsKeyMost { get; set; }
	}
}
