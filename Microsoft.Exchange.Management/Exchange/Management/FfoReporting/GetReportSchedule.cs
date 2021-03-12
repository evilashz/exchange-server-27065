using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F2 RID: 1010
	[OutputType(new Type[]
	{
		typeof(ReportSchedule)
	})]
	[Cmdlet("Get", "ReportSchedule")]
	public sealed class GetReportSchedule : ReportScheduleBaseTask
	{
		// Token: 0x060023AB RID: 9131 RVA: 0x0008FAE1 File Offset: 0x0008DCE1
		public GetReportSchedule() : base("GetReportSchedule", "Microsoft.Exchange.Hygiene.ManagementHelper.ReportSchedule.GetReportScheduleHelper")
		{
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0008FAF3 File Offset: 0x0008DCF3
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x0008FAFB File Offset: 0x0008DCFB
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Guid? ScheduleID { get; set; }

		// Token: 0x060023AE RID: 9134 RVA: 0x0008FB04 File Offset: 0x0008DD04
		protected override void InternalValidate()
		{
			try
			{
				base.InternalValidate();
			}
			catch (InvalidExpressionException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (Exception exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x04001C76 RID: 7286
		private const string ComponentName = "GetReportSchedule";
	}
}
