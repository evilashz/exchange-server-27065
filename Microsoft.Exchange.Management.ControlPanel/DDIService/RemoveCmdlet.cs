using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200015A RID: 346
	public class RemoveCmdlet : PipelineCmdlet, IReadOnlyChecker
	{
		// Token: 0x060021AB RID: 8619 RVA: 0x00065611 File Offset: 0x00063811
		public RemoveCmdlet()
		{
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x00065619 File Offset: 0x00063819
		protected RemoveCmdlet(RemoveCmdlet activity) : base(activity)
		{
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00065622 File Offset: 0x00063822
		public override Activity Clone()
		{
			return new RemoveCmdlet(this);
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0006562A File Offset: 0x0006382A
		protected override string GetVerb()
		{
			return "Remove-";
		}
	}
}
