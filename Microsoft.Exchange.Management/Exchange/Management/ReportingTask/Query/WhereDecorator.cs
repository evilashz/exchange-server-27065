using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.Query
{
	// Token: 0x020006BA RID: 1722
	internal class WhereDecorator<TReportObject> : QueryDecorator<TReportObject> where TReportObject : class
	{
		// Token: 0x06003CD3 RID: 15571 RVA: 0x0010297D File Offset: 0x00100B7D
		public WhereDecorator(ITaskContext taskContext) : base(taskContext)
		{
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x00102986 File Offset: 0x00100B86
		// (set) Token: 0x06003CD5 RID: 15573 RVA: 0x0010298E File Offset: 0x00100B8E
		public Expression<Func<TReportObject, bool>> Predicate { get; set; }

		// Token: 0x06003CD6 RID: 15574 RVA: 0x00102997 File Offset: 0x00100B97
		public override IQueryable<TReportObject> GetQuery(IQueryable<TReportObject> query)
		{
			if (this.Predicate == null)
			{
				return query;
			}
			query = query.Where(this.Predicate);
			return query;
		}
	}
}
