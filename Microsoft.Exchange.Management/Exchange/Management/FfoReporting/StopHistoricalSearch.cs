using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003F0 RID: 1008
	[Cmdlet("Stop", "HistoricalSearch")]
	public sealed class StopHistoricalSearch : HistoricalSearchBaseTask
	{
		// Token: 0x060023A0 RID: 9120 RVA: 0x0008F848 File Offset: 0x0008DA48
		public StopHistoricalSearch() : base("StopHistoricalSearch", "Microsoft.Exchange.Hygiene.ManagementHelper.HistoricalSearch.StopHistoricalSearchHelper")
		{
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x0008F85A File Offset: 0x0008DA5A
		// (set) Token: 0x060023A2 RID: 9122 RVA: 0x0008F862 File Offset: 0x0008DA62
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public Guid JobId { get; set; }

		// Token: 0x060023A3 RID: 9123 RVA: 0x0008F86C File Offset: 0x0008DA6C
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

		// Token: 0x04001C70 RID: 7280
		private const string ComponentName = "StopHistoricalSearch";
	}
}
