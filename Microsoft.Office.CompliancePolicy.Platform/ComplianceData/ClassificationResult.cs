using System;

namespace Microsoft.Office.CompliancePolicy.ComplianceData
{
	// Token: 0x02000052 RID: 82
	public sealed class ClassificationResult
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x000063BF File Offset: 0x000045BF
		public ClassificationResult(Guid classificationId, int count, int confidence)
		{
			if (classificationId == Guid.Empty)
			{
				throw new ArgumentException(string.Format("classificationId is empty", new object[0]));
			}
			this.ClassificationId = classificationId;
			this.Count = count;
			this.Confidence = confidence;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000063FF File Offset: 0x000045FF
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00006407 File Offset: 0x00004607
		public Guid ClassificationId { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006410 File Offset: 0x00004610
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00006418 File Offset: 0x00004618
		public int Count { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006421 File Offset: 0x00004621
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00006429 File Offset: 0x00004629
		public int Confidence { get; private set; }
	}
}
