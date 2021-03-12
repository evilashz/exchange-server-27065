using System;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000384 RID: 900
	internal class IsDehydratedQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x06003066 RID: 12390 RVA: 0x00093873 File Offset: 0x00091A73
		public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			return new bool?(LocalSession.Current.IsDehydrated);
		}

		// Token: 0x0400236A RID: 9066
		internal const string IsDehydratedRoleName = "IsDehydrated";

		// Token: 0x0400236B RID: 9067
		internal const string IsHydratedRoleName = "IsHydrated";
	}
}
