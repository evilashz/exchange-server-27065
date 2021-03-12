using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200019A RID: 410
	public class WorkflowNotExistException : Exception
	{
		// Token: 0x060022FC RID: 8956 RVA: 0x000697A0 File Offset: 0x000679A0
		public WorkflowNotExistException(string workflow)
		{
			this.workflow = workflow;
		}

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x000697AF File Offset: 0x000679AF
		public string Workflow
		{
			get
			{
				return this.workflow;
			}
		}

		// Token: 0x04001DA9 RID: 7593
		private readonly string workflow;
	}
}
