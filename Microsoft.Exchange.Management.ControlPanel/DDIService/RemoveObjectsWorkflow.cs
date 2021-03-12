using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000199 RID: 409
	public class RemoveObjectsWorkflow : Workflow
	{
		// Token: 0x060022F9 RID: 8953 RVA: 0x00069771 File Offset: 0x00067971
		public RemoveObjectsWorkflow()
		{
			base.Name = "RemoveObjects";
			this.Output = string.Empty;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0006978F File Offset: 0x0006798F
		protected RemoveObjectsWorkflow(RemoveObjectsWorkflow workflow) : base(workflow)
		{
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x00069798 File Offset: 0x00067998
		public override Workflow Clone()
		{
			return new RemoveObjectsWorkflow(this);
		}
	}
}
