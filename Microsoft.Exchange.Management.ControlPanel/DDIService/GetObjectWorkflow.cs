using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000191 RID: 401
	public class GetObjectWorkflow : MetaDataIncludeWorkflow
	{
		// Token: 0x060022D6 RID: 8918 RVA: 0x00069350 File Offset: 0x00067550
		public GetObjectWorkflow()
		{
			base.Name = "GetObject";
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00069363 File Offset: 0x00067563
		protected GetObjectWorkflow(GetObjectWorkflow workflow) : base(workflow)
		{
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0006936C File Offset: 0x0006756C
		public override Workflow Clone()
		{
			return new GetObjectWorkflow(this);
		}
	}
}
