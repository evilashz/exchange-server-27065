using System;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E0 RID: 1760
	public abstract class ScaledTenantReportBase<TReportObject> : TenantReportBase<TReportObject> where TReportObject : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06003E4B RID: 15947 RVA: 0x001047ED File Offset: 0x001029ED
		protected override DataMartType DataMartType
		{
			get
			{
				return DataMartType.TenantsScaled;
			}
		}
	}
}
