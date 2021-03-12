using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F6 RID: 1014
	[Cmdlet("Remove", "ReportSchedule")]
	public sealed class RemoveReportSchedule : ReportScheduleBaseTask
	{
		// Token: 0x060023DE RID: 9182 RVA: 0x0008FEAC File Offset: 0x0008E0AC
		public RemoveReportSchedule() : base("RemoveReportSchedule", "Microsoft.Exchange.Hygiene.ManagementHelper.ReportSchedule.RemoveReportScheduleHelper")
		{
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x0008FEBE File Offset: 0x0008E0BE
		// (set) Token: 0x060023E0 RID: 9184 RVA: 0x0008FEC6 File Offset: 0x0008E0C6
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public Guid ScheduleID { get; set; }

		// Token: 0x060023E1 RID: 9185 RVA: 0x0008FED0 File Offset: 0x0008E0D0
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

		// Token: 0x04001C90 RID: 7312
		private const string ComponentName = "RemoveReportSchedule";
	}
}
