using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000197 RID: 407
	public class SetObjectWorkflow : Workflow, ICallGetAfterExecuteWorkflow
	{
		// Token: 0x060022F1 RID: 8945 RVA: 0x00069718 File Offset: 0x00067918
		public SetObjectWorkflow()
		{
			base.Name = "SetObject";
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0006972B File Offset: 0x0006792B
		protected SetObjectWorkflow(SetObjectWorkflow workflow) : base(workflow)
		{
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00069734 File Offset: 0x00067934
		public override Workflow Clone()
		{
			return new SetObjectWorkflow(this);
		}

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0006973C File Offset: 0x0006793C
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x00069744 File Offset: 0x00067944
		public bool IgnoreGetObject { get; set; }
	}
}
