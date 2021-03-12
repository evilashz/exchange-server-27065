using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000124 RID: 292
	internal class ExecutingWorkflowEventArgs : EventArgs
	{
		// Token: 0x0600207A RID: 8314 RVA: 0x00062437 File Offset: 0x00060637
		internal ExecutingWorkflowEventArgs(Workflow workflow)
		{
			this.ExecutingWorkflow = workflow;
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x00062446 File Offset: 0x00060646
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x0006244E File Offset: 0x0006064E
		internal Workflow ExecutingWorkflow { get; private set; }
	}
}
