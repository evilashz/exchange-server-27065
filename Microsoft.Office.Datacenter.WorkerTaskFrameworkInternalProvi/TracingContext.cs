using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200000F RID: 15
	public class TracingContext
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00007018 File Offset: 0x00005218
		public TracingContext(WorkItem workItem)
		{
			this.workItem = workItem;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007027 File Offset: 0x00005227
		public TracingContext() : this(null)
		{
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00007030 File Offset: 0x00005230
		public static TracingContext Default
		{
			get
			{
				return TracingContext.defaultTracingContext;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007037 File Offset: 0x00005237
		public WorkItem WorkItem
		{
			get
			{
				return this.workItem;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000703F File Offset: 0x0000523F
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00007047 File Offset: 0x00005247
		public int Id { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007050 File Offset: 0x00005250
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00007058 File Offset: 0x00005258
		public int LId { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007061 File Offset: 0x00005261
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00007069 File Offset: 0x00005269
		internal bool IsDisabled { get; set; }

		// Token: 0x040000AD RID: 173
		private static readonly TracingContext defaultTracingContext = new TracingContext(null);

		// Token: 0x040000AE RID: 174
		private WorkItem workItem;
	}
}
