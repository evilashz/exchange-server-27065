using System;
using System.Linq;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.Query
{
	// Token: 0x020006B2 RID: 1714
	public class QueryDecorator<TReportObject> where TReportObject : class
	{
		// Token: 0x06003C9F RID: 15519 RVA: 0x00101F8D File Offset: 0x0010018D
		public QueryDecorator(ITaskContext taskContext)
		{
			this.TaskContext = taskContext;
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x00101F9C File Offset: 0x0010019C
		// (set) Token: 0x06003CA1 RID: 15521 RVA: 0x00101FA4 File Offset: 0x001001A4
		public ITaskContext TaskContext { get; private set; }

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x00101FAD File Offset: 0x001001AD
		// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x00101FB5 File Offset: 0x001001B5
		public bool IsPipeline { get; set; }

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x00101FBE File Offset: 0x001001BE
		// (set) Token: 0x06003CA5 RID: 15525 RVA: 0x00101FC6 File Offset: 0x001001C6
		public bool IsEnforced { get; set; }

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x00101FCF File Offset: 0x001001CF
		public virtual QueryOrder QueryOrder
		{
			get
			{
				return QueryOrder.Where;
			}
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x00101FD2 File Offset: 0x001001D2
		public virtual IQueryable<TReportObject> GetQuery(IQueryable<TReportObject> query)
		{
			return query;
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x00101FD5 File Offset: 0x001001D5
		public virtual void Validate()
		{
		}
	}
}
