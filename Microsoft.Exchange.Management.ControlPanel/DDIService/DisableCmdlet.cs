using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200012A RID: 298
	public class DisableCmdlet : PipelineCmdlet, IReadOnlyChecker
	{
		// Token: 0x060020A3 RID: 8355 RVA: 0x00062E7B File Offset: 0x0006107B
		public DisableCmdlet()
		{
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00062E83 File Offset: 0x00061083
		protected DisableCmdlet(DisableCmdlet activity) : base(activity)
		{
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00062E8C File Offset: 0x0006108C
		public override Activity Clone()
		{
			return new DisableCmdlet(this);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00062E94 File Offset: 0x00061094
		protected override string GetVerb()
		{
			return "Disable-";
		}
	}
}
