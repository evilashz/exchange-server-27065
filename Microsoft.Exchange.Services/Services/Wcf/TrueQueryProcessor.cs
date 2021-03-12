using System;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009C8 RID: 2504
	internal class TrueQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x060046E3 RID: 18147 RVA: 0x000FC39B File Offset: 0x000FA59B
		public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			return new bool?(true);
		}

		// Token: 0x040028B8 RID: 10424
		internal const string RoleName = "True";
	}
}
