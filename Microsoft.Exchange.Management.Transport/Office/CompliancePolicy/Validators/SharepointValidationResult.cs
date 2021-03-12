using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Office.CompliancePolicy.Validators
{
	// Token: 0x0200013F RID: 319
	internal class SharepointValidationResult
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x000336F2 File Offset: 0x000318F2
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x000336FA File Offset: 0x000318FA
		public SharepointSource SharepointSource { get; internal set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00033703 File Offset: 0x00031903
		// (set) Token: 0x06000DFE RID: 3582 RVA: 0x0003370B File Offset: 0x0003190B
		public bool IsValid { get; internal set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00033714 File Offset: 0x00031914
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x0003371C File Offset: 0x0003191C
		public bool IsTopLevelSiteCollection { get; internal set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00033725 File Offset: 0x00031925
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x0003372D File Offset: 0x0003192D
		public LocalizedString ValidationText { get; internal set; }
	}
}
