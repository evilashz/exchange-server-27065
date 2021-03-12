using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F4 RID: 1012
	[Cmdlet("Get", "ReportScheduleList")]
	[OutputType(new Type[]
	{
		typeof(ReportSchedule)
	})]
	public sealed class GetReportScheduleList : ReportScheduleBaseTask
	{
		// Token: 0x060023B3 RID: 9139 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
		public GetReportScheduleList() : base("GetReportScheduleList", "Microsoft.Exchange.Hygiene.ManagementHelper.ReportSchedule.GetReportScheduleListHelper")
		{
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x0008FBD2 File Offset: 0x0008DDD2
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x0008FBDA File Offset: 0x0008DDDA
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Guid? ScheduleID { get; set; }

		// Token: 0x060023B6 RID: 9142 RVA: 0x0008FBE4 File Offset: 0x0008DDE4
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

		// Token: 0x04001C7A RID: 7290
		private const string ComponentName = "GetReportScheduleList";
	}
}
