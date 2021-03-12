using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200012B RID: 299
	public class EnableCmdlet : PipelineCmdlet, IReadOnlyChecker
	{
		// Token: 0x060020A7 RID: 8359 RVA: 0x00062E9B File Offset: 0x0006109B
		public EnableCmdlet()
		{
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00062EA3 File Offset: 0x000610A3
		protected EnableCmdlet(EnableCmdlet activity) : base(activity)
		{
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00062EAC File Offset: 0x000610AC
		public override Activity Clone()
		{
			return new EnableCmdlet(this);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00062EB4 File Offset: 0x000610B4
		protected override string GetVerb()
		{
			return "Enable-";
		}
	}
}
