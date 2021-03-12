using System;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar
{
	// Token: 0x02000006 RID: 6
	internal class ExDarWorkloadHost : DarWorkloadHost
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000025D6 File Offset: 0x000007D6
		public ExDarWorkloadHost(DarServiceProvider provider)
		{
			this.provider = provider;
		}

		// Token: 0x04000008 RID: 8
		private DarServiceProvider provider;
	}
}
