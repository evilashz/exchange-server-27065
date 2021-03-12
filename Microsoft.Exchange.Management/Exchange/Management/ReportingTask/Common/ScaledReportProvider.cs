using System;
using System.Linq;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006AD RID: 1709
	internal class ScaledReportProvider<TReportObject> : ReportProvider<TReportObject> where TReportObject : ReportObject
	{
		// Token: 0x06003C8D RID: 15501 RVA: 0x00101DAB File Offset: 0x000FFFAB
		public ScaledReportProvider(ITaskContext taskContext, IReportContextFactory reportContextFactory) : base(taskContext, reportContextFactory)
		{
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x00101DC0 File Offset: 0x000FFFC0
		protected override IQueryable<TReportObject> GetReportQuery(IReportContext reportContext)
		{
			IQueryable<TReportObject> queryable = base.GetReportQuery(reportContext);
			if (!DataMart.Instance.IsTableFunctionQueryDisabled)
			{
				base.LogSqlStatement(reportContext, queryable, 2);
				queryable = reportContext.GetScaledQuery<TReportObject>(queryable);
				QueryDecorator<TReportObject> queryDecorator = this.queryDecorators.Single((QueryDecorator<TReportObject> decorator) => decorator is OrderByDecorator<TReportObject, DateTime>);
				if (queryDecorator != null)
				{
					queryable = queryDecorator.GetQuery(queryable);
				}
			}
			return queryable;
		}
	}
}
