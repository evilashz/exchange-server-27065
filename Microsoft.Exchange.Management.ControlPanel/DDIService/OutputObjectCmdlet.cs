using System;
using System.Data;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000130 RID: 304
	public abstract class OutputObjectCmdlet : CmdletActivity
	{
		// Token: 0x060020CD RID: 8397 RVA: 0x00063649 File Offset: 0x00061849
		public OutputObjectCmdlet()
		{
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x00063651 File Offset: 0x00061851
		protected OutputObjectCmdlet(OutputObjectCmdlet activity) : base(activity)
		{
			this.FillAllColumns = activity.FillAllColumns;
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x00063666 File Offset: 0x00061866
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x0006366E File Offset: 0x0006186E
		public bool FillAllColumns { get; set; }

		// Token: 0x060020D1 RID: 8401 RVA: 0x00063680 File Offset: 0x00061880
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult runResult = new RunResult();
			PowerShellResults<PSObject> powerShellResults;
			base.ExecuteCmdlet(null, runResult, out powerShellResults, false);
			if (!runResult.ErrorOccur && powerShellResults.Succeeded && !string.IsNullOrEmpty(base.DataObjectName))
			{
				runResult.DataObjectes.Add(base.DataObjectName);
				store.UpdateDataObject(base.DataObjectName, null);
				if (store.GetDataObjectType(base.DataObjectName) == typeof(object))
				{
					if (powerShellResults.Output != null)
					{
						store.UpdateDataObject(base.DataObjectName, (from c in powerShellResults.Output
						select c.BaseObject).ToList<object>());
					}
				}
				else if (powerShellResults.HasValue && powerShellResults.Value != null)
				{
					store.UpdateDataObject(base.DataObjectName, powerShellResults.Value.BaseObject);
				}
				updateTableDelegate(base.DataObjectName, this.FillAllColumns);
			}
			return runResult;
		}
	}
}
