using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F3 RID: 1011
	[Cmdlet("Get", "ReportScheduleHistory")]
	[OutputType(new Type[]
	{
		typeof(ReportSchedule)
	})]
	public sealed class GetReportScheduleHistory : ReportScheduleBaseTask
	{
		// Token: 0x060023AF RID: 9135 RVA: 0x0008FB50 File Offset: 0x0008DD50
		public GetReportScheduleHistory() : base("GetReportScheduleHistory", "Microsoft.Exchange.Hygiene.ManagementHelper.ReportSchedule.GetReportScheduleHistoryHelper")
		{
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0008FB62 File Offset: 0x0008DD62
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x0008FB6A File Offset: 0x0008DD6A
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Guid? ScheduleID { get; set; }

		// Token: 0x060023B2 RID: 9138 RVA: 0x0008FB74 File Offset: 0x0008DD74
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

		// Token: 0x04001C78 RID: 7288
		private const string ComponentName = "GetReportScheduleHistory";
	}
}
