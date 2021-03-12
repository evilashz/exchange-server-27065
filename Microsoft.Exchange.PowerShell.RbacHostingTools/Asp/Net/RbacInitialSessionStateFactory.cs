using System;
using System.Management.Automation.Runspaces;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net
{
	// Token: 0x0200000C RID: 12
	public class RbacInitialSessionStateFactory : InitialSessionStateSectionFactory
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000034C0 File Offset: 0x000016C0
		public override InitialSessionState GetInitialSessionState()
		{
			RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
			return rbacPrincipal.RbacConfiguration.CreateInitialSessionState();
		}
	}
}
