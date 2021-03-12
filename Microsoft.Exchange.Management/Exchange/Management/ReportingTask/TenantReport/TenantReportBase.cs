using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006CC RID: 1740
	public abstract class TenantReportBase<TReportObject> : DateReportTaskBase<TReportObject> where TReportObject : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x06003DCC RID: 15820 RVA: 0x0010344E File Offset: 0x0010164E
		protected TenantReportBase()
		{
			this.tenantDecorator = new TenantDecorator<TReportObject>(base.TaskContext);
			this.tenantDecorator.IsPipeline = true;
			base.AddQueryDecorator(this.tenantDecorator);
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06003DCD RID: 15821 RVA: 0x0010347F File Offset: 0x0010167F
		// (set) Token: 0x06003DCE RID: 15822 RVA: 0x00103496 File Offset: 0x00101696
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06003DCF RID: 15823 RVA: 0x001034A9 File Offset: 0x001016A9
		protected override DataMartType DataMartType
		{
			get
			{
				return DataMartType.Tenants;
			}
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x001034AC File Offset: 0x001016AC
		protected override void ProcessPipelineParameter()
		{
			base.ProcessPipelineParameter();
			Guid? tenantExternalDirectoryId;
			this.tenantDecorator.TenantGuid = ADHelper.ResolveOrganizationGuid(this.Organization, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, out tenantExternalDirectoryId);
			this.tenantDecorator.TenantExternalDirectoryId = tenantExternalDirectoryId;
		}

		// Token: 0x040027C8 RID: 10184
		private const string OrganizationKey = "Organization";

		// Token: 0x040027C9 RID: 10185
		private readonly TenantDecorator<TReportObject> tenantDecorator;
	}
}
