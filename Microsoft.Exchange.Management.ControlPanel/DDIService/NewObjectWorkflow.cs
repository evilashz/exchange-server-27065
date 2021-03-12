using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000198 RID: 408
	public class NewObjectWorkflow : Workflow
	{
		// Token: 0x060022F6 RID: 8950 RVA: 0x0006974D File Offset: 0x0006794D
		public NewObjectWorkflow()
		{
			base.Name = "NewObject";
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x00069760 File Offset: 0x00067960
		protected NewObjectWorkflow(NewObjectWorkflow workflow) : base(workflow)
		{
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x00069769 File Offset: 0x00067969
		public override Workflow Clone()
		{
			return new NewObjectWorkflow(this);
		}
	}
}
