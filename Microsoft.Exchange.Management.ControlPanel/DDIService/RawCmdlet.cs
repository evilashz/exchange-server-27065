using System;
using System.Data;
using System.Management.Automation;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000158 RID: 344
	public class RawCmdlet : CmdletActivity
	{
		// Token: 0x060021A2 RID: 8610 RVA: 0x0006551C File Offset: 0x0006371C
		public RawCmdlet()
		{
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00065524 File Offset: 0x00063724
		protected RawCmdlet(RawCmdlet activity) : base(activity)
		{
			this.RawResultAction = activity.RawResultAction;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00065539 File Offset: 0x00063739
		public override Activity Clone()
		{
			return new RawCmdlet(this);
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x00065541 File Offset: 0x00063741
		// (set) Token: 0x060021A6 RID: 8614 RVA: 0x00065549 File Offset: 0x00063749
		[DDIValidCodeBehindMethod]
		public string RawResultAction { get; set; }

		// Token: 0x060021A7 RID: 8615 RVA: 0x00065554 File Offset: 0x00063754
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult runResult = new RunResult();
			PowerShellResults<PSObject> powerShellResults;
			base.ExecuteCmdlet(null, runResult, out powerShellResults, false);
			if (!runResult.ErrorOccur && powerShellResults.Output != null && null != codeBehind && !string.IsNullOrEmpty(this.RawResultAction) && !string.IsNullOrEmpty(base.DataObjectName))
			{
				store.UpdateDataObject(base.DataObjectName, powerShellResults);
				DDIHelper.Trace("RawResultAction: " + this.RawResultAction);
				codeBehind.GetMethod(this.RawResultAction).Invoke(null, new object[]
				{
					input,
					dataTable,
					store
				});
			}
			return runResult;
		}
	}
}
