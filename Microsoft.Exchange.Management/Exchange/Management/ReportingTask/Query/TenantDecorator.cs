using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask.Query
{
	// Token: 0x020006BB RID: 1723
	internal class TenantDecorator<TReportObject> : QueryDecorator<TReportObject> where TReportObject : class, ITenantColumn
	{
		// Token: 0x06003CD7 RID: 15575 RVA: 0x001029B4 File Offset: 0x00100BB4
		public TenantDecorator(ITaskContext taskContext) : base(taskContext)
		{
			base.IsEnforced = true;
			this.reportTypesUseExchangeTenantGuid = new ConcurrentBag<Type>
			{
				typeof(ConnectionByClientTypeDetailReport),
				typeof(ConnectionByClientTypeReport),
				typeof(GroupActivityReport),
				typeof(MailboxActivityReport),
				typeof(MailboxUsageDetailReport),
				typeof(MailboxUsageReport),
				typeof(StaleMailboxDetailReport),
				typeof(StaleMailboxReport)
			};
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x00102A5C File Offset: 0x00100C5C
		// (set) Token: 0x06003CD9 RID: 15577 RVA: 0x00102A64 File Offset: 0x00100C64
		public Guid? TenantGuid { get; set; }

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06003CDA RID: 15578 RVA: 0x00102A6D File Offset: 0x00100C6D
		// (set) Token: 0x06003CDB RID: 15579 RVA: 0x00102A75 File Offset: 0x00100C75
		public Guid? TenantExternalDirectoryId { get; set; }

		// Token: 0x06003CDC RID: 15580 RVA: 0x00102A88 File Offset: 0x00100C88
		public override IQueryable<TReportObject> GetQuery(IQueryable<TReportObject> query)
		{
			Guid? tenantGuid;
			Guid? tenantExternalDirectoryId;
			if (base.TaskContext.IsCurrentOrganizationForestWide)
			{
				tenantGuid = this.TenantGuid;
				tenantExternalDirectoryId = this.TenantExternalDirectoryId;
			}
			else
			{
				tenantGuid = new Guid?(base.TaskContext.CurrentOrganizationGuid);
				tenantExternalDirectoryId = new Guid?(base.TaskContext.CurrentOrganizationExternalDirectoryId);
			}
			Type typeFromHandle = typeof(TReportObject);
			if (this.reportTypesUseExchangeTenantGuid.Contains(typeFromHandle))
			{
				query = from report in query
				where (Guid?)report.TenantGuid == tenantGuid
				select report;
			}
			else
			{
				query = from report in query
				where (Guid?)report.TenantGuid == tenantExternalDirectoryId
				select report;
			}
			return query;
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x00102C24 File Offset: 0x00100E24
		public override void Validate()
		{
			base.Validate();
			if (base.TaskContext.IsCurrentOrganizationForestWide && this.TenantGuid == null)
			{
				base.TaskContext.WriteError(new ReportingException(Strings.OrganizationNotSpecified), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x04002756 RID: 10070
		private ConcurrentBag<Type> reportTypesUseExchangeTenantGuid;
	}
}
