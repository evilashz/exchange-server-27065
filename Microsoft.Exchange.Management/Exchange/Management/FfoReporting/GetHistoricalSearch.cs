using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003E9 RID: 1001
	[Cmdlet("Get", "HistoricalSearch")]
	[OutputType(new Type[]
	{
		typeof(HistoricalSearch)
	})]
	public sealed class GetHistoricalSearch : HistoricalSearchBaseTask
	{
		// Token: 0x06002350 RID: 9040 RVA: 0x0008F3C9 File Offset: 0x0008D5C9
		public GetHistoricalSearch() : base("GetHistoricalSeach", "Microsoft.Exchange.Hygiene.ManagementHelper.HistoricalSearch.GetHistoricalSearchHelper")
		{
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x0008F3DB File Offset: 0x0008D5DB
		// (set) Token: 0x06002352 RID: 9042 RVA: 0x0008F3E3 File Offset: 0x0008D5E3
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Guid? JobId { get; set; }

		// Token: 0x06002353 RID: 9043 RVA: 0x0008F3EC File Offset: 0x0008D5EC
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

		// Token: 0x04001C32 RID: 7218
		private const string ComponentName = "GetHistoricalSeach";
	}
}
