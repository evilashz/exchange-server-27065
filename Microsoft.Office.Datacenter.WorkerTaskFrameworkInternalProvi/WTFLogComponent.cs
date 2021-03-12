using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200002C RID: 44
	public class WTFLogComponent
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000AE86 File Offset: 0x00009086
		public WTFLogComponent(Guid category, int logTag, string name, bool traceLogEnabled)
		{
			this.category = category;
			this.logTag = logTag;
			this.name = name;
			this.traceLogEnabled = traceLogEnabled;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000AEAB File Offset: 0x000090AB
		public Guid Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000AEB3 File Offset: 0x000090B3
		public int LogTag
		{
			get
			{
				return this.logTag;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000AEBB File Offset: 0x000090BB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000AEC3 File Offset: 0x000090C3
		public bool IsTraceLoggingEnabled
		{
			get
			{
				return this.traceLogEnabled;
			}
		}

		// Token: 0x04000110 RID: 272
		private readonly Guid category;

		// Token: 0x04000111 RID: 273
		private readonly int logTag;

		// Token: 0x04000112 RID: 274
		private readonly string name;

		// Token: 0x04000113 RID: 275
		private readonly bool traceLogEnabled;
	}
}
