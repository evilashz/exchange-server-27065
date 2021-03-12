using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Office.Story.V1.ImageAnalysis
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal class Bin<T>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000221C File Offset: 0x0000041C
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002224 File Offset: 0x00000424
		public List<T> Items { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000222D File Offset: 0x0000042D
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002235 File Offset: 0x00000435
		public double RangeStart { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000223E File Offset: 0x0000043E
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002246 File Offset: 0x00000446
		public double RangeEnd { get; set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002250 File Offset: 0x00000450
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[{0:G}, {1:G}]: {2}", new object[]
			{
				this.RangeStart,
				this.RangeEnd,
				this.Items.Count
			});
		}
	}
}
