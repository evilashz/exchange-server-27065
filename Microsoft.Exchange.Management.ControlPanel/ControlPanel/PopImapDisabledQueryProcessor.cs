using System;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000382 RID: 898
	internal class PopImapDisabledQueryProcessor : EcpCmdletQueryProcessor
	{
		// Token: 0x0600305F RID: 12383 RVA: 0x00093704 File Offset: 0x00091904
		internal override bool? IsInRoleCmdlet(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			RbacQuery rbacQuery = new RbacQuery("Get-CasMailbox");
			if (!rbacQuery.IsInRole(rbacConfiguration))
			{
				return new bool?(true);
			}
			return new bool?(!rbacConfiguration.ExecutingUserIsPopEnabled && !rbacConfiguration.ExecutingUserIsImapEnabled);
		}

		// Token: 0x04002362 RID: 9058
		internal const string RoleName = "PopImapDisabled";
	}
}
