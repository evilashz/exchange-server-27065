using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000193 RID: 403
	public class GetObjectForListWorkflow : MetaDataIncludeWorkflow
	{
		// Token: 0x060022DC RID: 8924 RVA: 0x000693A6 File Offset: 0x000675A6
		public GetObjectForListWorkflow()
		{
			base.Name = "GetObjectForList";
			base.IncludeReadOnlyProperty = false;
			base.IncludeNotAccessProperty = false;
			base.IncludeValidator = false;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000693CE File Offset: 0x000675CE
		protected GetObjectForListWorkflow(GetObjectForListWorkflow workflow) : base(workflow)
		{
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000693D7 File Offset: 0x000675D7
		public override Workflow Clone()
		{
			return new GetObjectForListWorkflow(this);
		}
	}
}
