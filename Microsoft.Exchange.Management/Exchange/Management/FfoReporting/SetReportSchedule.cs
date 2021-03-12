using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003FD RID: 1021
	[Cmdlet("Set", "ReportSchedule")]
	[OutputType(new Type[]
	{
		typeof(ReportSchedule)
	})]
	public sealed class SetReportSchedule : ReportScheduleBaseTask
	{
		// Token: 0x0600240B RID: 9227 RVA: 0x00090078 File Offset: 0x0008E278
		public SetReportSchedule() : base("SetReportSchedule", "Microsoft.Exchange.Hygiene.ManagementHelper.ReportSchedule.SetReportScheduleHelper")
		{
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x0009008A File Offset: 0x0008E28A
		// (set) Token: 0x0600240D RID: 9229 RVA: 0x00090092 File Offset: 0x0008E292
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Guid? ScheduleID { get; set; }

		// Token: 0x0600240E RID: 9230 RVA: 0x0009009C File Offset: 0x0008E29C
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

		// Token: 0x04001CCA RID: 7370
		private const string ComponentName = "SetReportSchedule";
	}
}
