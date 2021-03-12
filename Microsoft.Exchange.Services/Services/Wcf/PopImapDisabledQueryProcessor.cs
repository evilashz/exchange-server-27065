using System;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009C7 RID: 2503
	internal class PopImapDisabledQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x060046E1 RID: 18145 RVA: 0x000FC350 File Offset: 0x000FA550
		public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			RbacQuery rbacQuery = new RbacQuery("Get-CASMailbox");
			if (!rbacQuery.IsInRole(rbacConfiguration))
			{
				return new bool?(true);
			}
			return new bool?(!rbacConfiguration.ExecutingUserIsPopEnabled && !rbacConfiguration.ExecutingUserIsImapEnabled);
		}

		// Token: 0x040028B7 RID: 10423
		internal const string RoleName = "PopImapDisabled";
	}
}
