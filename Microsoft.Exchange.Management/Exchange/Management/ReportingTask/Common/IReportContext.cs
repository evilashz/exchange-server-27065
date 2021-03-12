using System;
using System.Linq;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x0200069D RID: 1693
	internal interface IReportContext : IDisposable
	{
		// Token: 0x06003C02 RID: 15362
		IQueryable<TReportObject> GetReports<TReportObject>() where TReportObject : ReportObject;

		// Token: 0x06003C03 RID: 15363
		IQueryable<TReportObject> GetScaledQuery<TReportObject>(IQueryable<TReportObject> query) where TReportObject : ReportObject;

		// Token: 0x06003C04 RID: 15364
		string GetSqlCommandText(IQueryable query);

		// Token: 0x06003C05 RID: 15365
		void ChangeViewName(Type reportType, string viewName);
	}
}
