using System;

namespace Microsoft.Filtering.Results
{
	// Token: 0x02000018 RID: 24
	public class FilteringElapsedTimes
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003204 File Offset: 0x00001404
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000320C File Offset: 0x0000140C
		public TimeSpan Total { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003215 File Offset: 0x00001415
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000321D File Offset: 0x0000141D
		public TimeSpan Scanning { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003226 File Offset: 0x00001426
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000322E File Offset: 0x0000142E
		public TimeSpan Parsing { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003237 File Offset: 0x00001437
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000323F File Offset: 0x0000143F
		public TimeSpan TextExtraction { get; set; }
	}
}
