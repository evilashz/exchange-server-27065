using System;
using System.Linq;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000380 RID: 896
	internal class AcceptedFederatedDomainsQueryProcessor : EcpCmdletQueryProcessor
	{
		// Token: 0x0600305A RID: 12378 RVA: 0x00093640 File Offset: 0x00091840
		internal override bool? IsInRoleCmdlet(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			AcceptedDomains acceptedDomains = new AcceptedDomains();
			PowerShellResults<AcceptedDomain> list = acceptedDomains.GetList(null, null);
			if (list.Succeeded)
			{
				return new bool?((from x in list.Output
				where x.AuthenticationType == AuthenticationType.Federated
				select x).Count<AcceptedDomain>() > 0);
			}
			base.LogCmdletError(list, "OrgHasFederatedDomains");
			return new bool?(false);
		}

		// Token: 0x0400235F RID: 9055
		internal const string RoleName = "OrgHasFederatedDomains";
	}
}
