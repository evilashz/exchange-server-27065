using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200014D RID: 333
	public class NewCmdlet : OutputObjectCmdlet, IReadOnlyChecker
	{
		// Token: 0x06002153 RID: 8531 RVA: 0x00064620 File Offset: 0x00062820
		public NewCmdlet()
		{
			base.IdentityVariable = string.Empty;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00064633 File Offset: 0x00062833
		protected NewCmdlet(NewCmdlet activity) : base(activity)
		{
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0006463C File Offset: 0x0006283C
		public override Activity Clone()
		{
			return new NewCmdlet(this);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00064644 File Offset: 0x00062844
		protected override string GetVerb()
		{
			return "New-";
		}
	}
}
