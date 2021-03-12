using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000111 RID: 273
	public class AddCmdlet : CmdletActivity, IReadOnlyChecker
	{
		// Token: 0x06001FB7 RID: 8119 RVA: 0x0005F91D File Offset: 0x0005DB1D
		public AddCmdlet()
		{
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0005F925 File Offset: 0x0005DB25
		protected AddCmdlet(AddCmdlet activity) : base(activity)
		{
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0005F92E File Offset: 0x0005DB2E
		public override Activity Clone()
		{
			return new AddCmdlet(this);
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0005F936 File Offset: 0x0005DB36
		protected override string GetVerb()
		{
			return "Add-";
		}
	}
}
