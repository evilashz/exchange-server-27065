using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200016A RID: 362
	internal class LabeledTimeSpan
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x00026955 File Offset: 0x00024B55
		internal LabeledTimeSpan(string label, TimeSpan timeSpan)
		{
			this.label = label;
			this.timeSpan = timeSpan;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0002696B File Offset: 0x00024B6B
		internal string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00026973 File Offset: 0x00024B73
		internal TimeSpan TimeSpan
		{
			get
			{
				return this.timeSpan;
			}
		}

		// Token: 0x040006FB RID: 1787
		private readonly string label;

		// Token: 0x040006FC RID: 1788
		private readonly TimeSpan timeSpan;
	}
}
