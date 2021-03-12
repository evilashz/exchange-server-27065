using System;
using System.ServiceModel;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x02000015 RID: 21
	internal class ReportingWebServiceRunspaceCache : RbacRunspaceCache
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000032D0 File Offset: 0x000014D0
		protected override string GetSessionKey()
		{
			string str = string.Empty;
			if (OperationContext.Current != null)
			{
				str = OperationContext.Current.SessionId;
			}
			string str2 = RbacPrincipal.Current.CacheKeys[0];
			return "Exchange" + str + str2;
		}
	}
}
