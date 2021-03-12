using System;
using System.Data;
using System.Data.Common;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A6 RID: 1702
	internal class ReportContext : DisposeTrackableBase, IReportContext, IDisposable
	{
		// Token: 0x06003C51 RID: 15441 RVA: 0x00100FD1 File Offset: 0x000FF1D1
		public ReportContext(IDbConnection connection)
		{
			this.mappingSource = new MappingSourceWrapper(new AttributeMappingSource());
			this.dataContext = new ReportDataContext(connection, this.mappingSource);
			this.dataContext.ObjectTrackingEnabled = false;
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x00101007 File Offset: 0x000FF207
		public IQueryable<TReportObject> GetReports<TReportObject>() where TReportObject : ReportObject
		{
			return this.dataContext.GetTable<TReportObject>();
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x00101014 File Offset: 0x000FF214
		public IQueryable<TReportObject> GetScaledQuery<TReportObject>(IQueryable<TReportObject> query) where TReportObject : ReportObject
		{
			return this.dataContext.GetScaledQuery<TReportObject>(query);
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x00101024 File Offset: 0x000FF224
		public string GetSqlCommandText(IQueryable query)
		{
			DbCommand command = this.dataContext.GetCommand(query);
			StringBuilder stringBuilder = new StringBuilder(command.CommandText);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Parameters:");
			foreach (object obj in command.Parameters)
			{
				DbParameter dbParameter = (DbParameter)obj;
				stringBuilder.AppendFormat("{0}={1}", dbParameter.ParameterName, dbParameter.Value);
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x001010CC File Offset: 0x000FF2CC
		public void ChangeViewName(Type reportType, string viewName)
		{
			if (!string.IsNullOrEmpty(viewName))
			{
				this.mappingSource.AddMapping(reportType, viewName);
			}
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x001010E3 File Offset: 0x000FF2E3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ReportContext>(this);
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x001010EB File Offset: 0x000FF2EB
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.dataContext != null)
			{
				this.dataContext.Dispose();
				this.dataContext = null;
			}
		}

		// Token: 0x0400272F RID: 10031
		private readonly MappingSourceWrapper mappingSource;

		// Token: 0x04002730 RID: 10032
		private ReportDataContext dataContext;
	}
}
