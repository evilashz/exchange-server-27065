using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x02000699 RID: 1689
	public abstract class DateReportTaskBase<TReportObject> : ReportingTaskBase<TReportObject> where TReportObject : ReportObject, IDateColumn
	{
		// Token: 0x06003BFD RID: 15357 RVA: 0x001009AC File Offset: 0x000FEBAC
		protected DateReportTaskBase()
		{
			this.dateDecorator = new DateDecorator<TReportObject>(base.TaskContext);
			base.AddQueryDecorator(this.dateDecorator);
			base.AddQueryDecorator(new OrderByDecorator<TReportObject, DateTime>(base.TaskContext)
			{
				OrderBy = ((TReportObject report) => report.Date),
				Descending = true
			});
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06003BFE RID: 15358 RVA: 0x00100A37 File Offset: 0x000FEC37
		// (set) Token: 0x06003BFF RID: 15359 RVA: 0x00100A44 File Offset: 0x000FEC44
		[Parameter(Mandatory = false)]
		public DateTime? StartDate
		{
			get
			{
				return this.dateDecorator.StartDate;
			}
			set
			{
				this.dateDecorator.StartDate = value;
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x00100A52 File Offset: 0x000FEC52
		// (set) Token: 0x06003C01 RID: 15361 RVA: 0x00100A5F File Offset: 0x000FEC5F
		[Parameter(Mandatory = false)]
		public DateTime? EndDate
		{
			get
			{
				return this.dateDecorator.EndDate;
			}
			set
			{
				this.dateDecorator.EndDate = value;
			}
		}

		// Token: 0x04002712 RID: 10002
		private readonly DateDecorator<TReportObject> dateDecorator;
	}
}
