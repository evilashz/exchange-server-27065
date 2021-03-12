using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000035 RID: 53
	public class EOPTask : Task
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000CD15 File Offset: 0x0000AF15
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000CD1D File Offset: 0x0000AF1D
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter Organization { get; set; }

		// Token: 0x0600027E RID: 638 RVA: 0x0000CD28 File Offset: 0x0000AF28
		protected void ThrowAndLogTaskError(Exception e)
		{
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_FfoReportingTaskFailure, new string[]
			{
				e.ToString()
			});
			this.ThrowTaskError(e);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000CD57 File Offset: 0x0000AF57
		protected void ThrowTaskError(Exception e)
		{
			base.ThrowTerminatingError(e, ErrorCategory.NotSpecified, null);
		}
	}
}
