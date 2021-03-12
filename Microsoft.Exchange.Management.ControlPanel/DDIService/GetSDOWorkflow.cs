using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000192 RID: 402
	public class GetSDOWorkflow : MetaDataIncludeWorkflow
	{
		// Token: 0x060022D9 RID: 8921 RVA: 0x00069374 File Offset: 0x00067574
		public GetSDOWorkflow()
		{
			base.Name = "GetForSDO";
			base.IncludeReadOnlyProperty = false;
			base.IncludeValidator = false;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00069395 File Offset: 0x00067595
		protected GetSDOWorkflow(GetSDOWorkflow workflow) : base(workflow)
		{
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0006939E File Offset: 0x0006759E
		public override Workflow Clone()
		{
			return new GetSDOWorkflow(this);
		}
	}
}
