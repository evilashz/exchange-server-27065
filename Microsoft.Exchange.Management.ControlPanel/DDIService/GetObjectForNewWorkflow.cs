using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000190 RID: 400
	public class GetObjectForNewWorkflow : MetaDataIncludeWorkflow
	{
		// Token: 0x060022D3 RID: 8915 RVA: 0x0006932C File Offset: 0x0006752C
		public GetObjectForNewWorkflow()
		{
			base.Name = "GetObjectForNew";
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0006933F File Offset: 0x0006753F
		protected GetObjectForNewWorkflow(GetObjectForNewWorkflow workflow) : base(workflow)
		{
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00069348 File Offset: 0x00067548
		public override Workflow Clone()
		{
			return new GetObjectForNewWorkflow(this);
		}
	}
}
