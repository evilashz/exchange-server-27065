using System;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000385 RID: 901
	internal class SoftDeletedFeatureStatusQueryProcessor : EcpCmdletQueryProcessor
	{
		// Token: 0x06003068 RID: 12392 RVA: 0x0009388C File Offset: 0x00091A8C
		internal override bool? IsInRoleCmdlet(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			return new bool?(LocalSession.Current.IsSoftDeletedFeatureEnabled);
		}

		// Token: 0x0400236C RID: 9068
		internal const string RoleName = "SoftDeletedFeatureEnabled";
	}
}
