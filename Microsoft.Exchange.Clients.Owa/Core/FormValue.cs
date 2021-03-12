using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000125 RID: 293
	public class FormValue
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x00045092 File Offset: 0x00043292
		public FormValue(object value, ulong segmentationFlags, bool isCustomForm)
		{
			this.value = value;
			this.segmentationFlags = segmentationFlags;
			this.isCustomForm = isCustomForm;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x000450AF File Offset: 0x000432AF
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000450B7 File Offset: 0x000432B7
		public ulong SegmentationFlags
		{
			get
			{
				return this.segmentationFlags;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000450BF File Offset: 0x000432BF
		public bool IsCustomForm
		{
			get
			{
				return this.isCustomForm;
			}
		}

		// Token: 0x0400072D RID: 1837
		private readonly object value;

		// Token: 0x0400072E RID: 1838
		private readonly ulong segmentationFlags;

		// Token: 0x0400072F RID: 1839
		private readonly bool isCustomForm;
	}
}
