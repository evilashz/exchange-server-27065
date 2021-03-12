using System;
using System.Management.Automation.Runspaces;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.ServiceHost.Common.Powershell
{
	// Token: 0x02000017 RID: 23
	internal class LocalSessionStateFactory : InitialSessionStateFactory
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00003F9C File Offset: 0x0000219C
		public override InitialSessionState CreateInitialSessionState()
		{
			InitialSessionState initialSessionState = InitialSessionState.CreateDefault();
			PSSnapInException ex;
			initialSessionState.ImportPSSnapIn("Microsoft.Exchange.Management.PowerShell.E2010", out ex);
			if (ex != null)
			{
				throw ex;
			}
			return initialSessionState;
		}
	}
}
