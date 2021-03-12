using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000194 RID: 404
	public class BulkEditWorkflow : Workflow
	{
		// Token: 0x060022DF RID: 8927 RVA: 0x000693DF File Offset: 0x000675DF
		public BulkEditWorkflow()
		{
			base.Name = "BulkEdit";
			base.AsyncMode = AsyncMode.AsynchronousOnly;
			base.AsyncRunning = true;
			base.ProgressCalculator = typeof(BulkEditProgressCalculator);
		}

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x00069410 File Offset: 0x00067610
		// (set) Token: 0x060022E1 RID: 8929 RVA: 0x00069417 File Offset: 0x00067617
		public override string Output
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x00069428 File Offset: 0x00067628
		public override Workflow Clone()
		{
			BulkEditWorkflow bulkEditWorkflow = new BulkEditWorkflow();
			bulkEditWorkflow.Name = base.Name;
			bulkEditWorkflow.Activities = new Collection<Activity>((from c in base.Activities
			select c.Clone()).ToList<Activity>());
			return bulkEditWorkflow;
		}
	}
}
