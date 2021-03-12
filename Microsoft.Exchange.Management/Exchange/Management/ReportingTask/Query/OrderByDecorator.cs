using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.Query
{
	// Token: 0x020006B8 RID: 1720
	internal class OrderByDecorator<TReportObject, TOrderByProperty> : QueryDecorator<TReportObject> where TReportObject : class
	{
		// Token: 0x06003CC0 RID: 15552 RVA: 0x00102738 File Offset: 0x00100938
		public OrderByDecorator(ITaskContext taskContext) : base(taskContext)
		{
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x00102741 File Offset: 0x00100941
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x00102749 File Offset: 0x00100949
		public bool Descending { get; set; }

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x00102752 File Offset: 0x00100952
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x0010275A File Offset: 0x0010095A
		public Expression<Func<TReportObject, TOrderByProperty>> OrderBy { get; set; }

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x00102763 File Offset: 0x00100963
		public override QueryOrder QueryOrder
		{
			get
			{
				return QueryOrder.OrderBy;
			}
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x00102766 File Offset: 0x00100966
		public override IQueryable<TReportObject> GetQuery(IQueryable<TReportObject> query)
		{
			if (this.OrderBy == null)
			{
				return query;
			}
			if (this.Descending)
			{
				query = query.OrderByDescending(this.OrderBy);
			}
			else
			{
				query = query.OrderBy(this.OrderBy);
			}
			return query;
		}
	}
}
