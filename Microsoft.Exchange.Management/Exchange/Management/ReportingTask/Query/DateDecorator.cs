using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.Query
{
	// Token: 0x020006B3 RID: 1715
	internal class DateDecorator<TReportObject> : QueryDecorator<TReportObject> where TReportObject : class, IDateColumn
	{
		// Token: 0x06003CA9 RID: 15529 RVA: 0x00101FD7 File Offset: 0x001001D7
		public DateDecorator(ITaskContext taskContext) : base(taskContext)
		{
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06003CAA RID: 15530 RVA: 0x00101FE0 File Offset: 0x001001E0
		// (set) Token: 0x06003CAB RID: 15531 RVA: 0x00101FE8 File Offset: 0x001001E8
		public DateTime? StartDate { get; set; }

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x00101FF1 File Offset: 0x001001F1
		// (set) Token: 0x06003CAD RID: 15533 RVA: 0x00101FF9 File Offset: 0x001001F9
		public DateTime? EndDate { get; set; }

		// Token: 0x06003CAE RID: 15534 RVA: 0x00102004 File Offset: 0x00100204
		public override IQueryable<TReportObject> GetQuery(IQueryable<TReportObject> query)
		{
			if (this.StartDate != null)
			{
				query = from report in query
				where report.Date >= this.StartDate.Value
				select report;
			}
			if (this.EndDate != null)
			{
				query = from report in query
				where report.Date <= this.EndDate.Value
				select report;
			}
			return query;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x0010217C File Offset: 0x0010037C
		public override void Validate()
		{
			if (this.StartDate != null && (this.StartDate < DateDecorator<TReportObject>.MinDateTime || this.StartDate > DateDecorator<TReportObject>.MaxDateTime))
			{
				base.TaskContext.WriteError(new InvalidDateValueException(this.StartDate.Value, DateDecorator<TReportObject>.MinDateTime, DateDecorator<TReportObject>.MaxDateTime), ExchangeErrorCategory.Client, null);
			}
			if (this.EndDate != null && (this.EndDate < DateDecorator<TReportObject>.MinDateTime || this.EndDate > DateDecorator<TReportObject>.MaxDateTime))
			{
				base.TaskContext.WriteError(new InvalidDateValueException(this.EndDate.Value, DateDecorator<TReportObject>.MinDateTime, DateDecorator<TReportObject>.MaxDateTime), ExchangeErrorCategory.Client, null);
			}
			if (this.StartDate != null && this.EndDate != null && this.StartDate > this.EndDate)
			{
				base.TaskContext.WriteError(new InvalidDateParameterException(this.StartDate.Value, this.EndDate.Value), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x0400274A RID: 10058
		public static readonly DateTime MinDateTime = new DateTime(1753, 1, 1);

		// Token: 0x0400274B RID: 10059
		public static readonly DateTime MaxDateTime = new DateTime(9999, 12, 31);
	}
}
