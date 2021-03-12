using System;
using System.Linq;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200037F RID: 895
	internal class AcceptedManagedDomainsQueryProcessor : EcpCmdletQueryProcessor
	{
		// Token: 0x06003057 RID: 12375 RVA: 0x000935A4 File Offset: 0x000917A4
		internal override bool? IsInRoleCmdlet(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			AcceptedDomains acceptedDomains = new AcceptedDomains();
			PowerShellResults<AcceptedDomain> list = acceptedDomains.GetList(null, null);
			if (list.Succeeded)
			{
				return new bool?((from x in list.Output
				where x.AuthenticationType == AuthenticationType.Managed
				select x).Count<AcceptedDomain>() > 0);
			}
			base.LogCmdletError(list, "OrgHasManagedDomains");
			return new bool?(false);
		}

		// Token: 0x0400235D RID: 9053
		internal const string RoleName = "OrgHasManagedDomains";
	}
}
