using System;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200037E RID: 894
	internal class ClosedCampusQueryProcessor : EcpCmdletQueryProcessor
	{
		// Token: 0x06003055 RID: 12373 RVA: 0x000934E0 File Offset: 0x000916E0
		internal override bool? IsInRoleCmdlet(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			RbacQuery rbacQuery = new RbacQuery("Get-SupervisionPolicy");
			if (!rbacQuery.IsInRole(rbacConfiguration))
			{
				return new bool?(false);
			}
			Supervision supervision = new Supervision();
			PowerShellResults<SupervisionStatus> @object = supervision.GetObject(null);
			if (@object.SucceededWithValue)
			{
				foreach (SupervisionStatus supervisionStatus in @object.Output)
				{
					if (supervisionStatus.ClosedCampusPolicyEnabled)
					{
						return new bool?(true);
					}
				}
				return new bool?(false);
			}
			base.LogCmdletError(@object, "ClosedCampus");
			return null;
		}

		// Token: 0x0400235C RID: 9052
		internal const string RoleName = "ClosedCampus";
	}
}
