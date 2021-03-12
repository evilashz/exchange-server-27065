using System;
using System.Web;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200038B RID: 907
	internal class EcpRunspaceCache : RbacRunspaceCache
	{
		// Token: 0x0600307E RID: 12414 RVA: 0x00093A9A File Offset: 0x00091C9A
		protected override string GetSessionKey()
		{
			return "Exchange" + HttpContext.Current.GetSessionID() + RbacPrincipal.Current.CacheKeys[0];
		}
	}
}
