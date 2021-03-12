using System;
using System.Management.Automation.Host;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.ServiceHost.Common.Powershell
{
	// Token: 0x02000016 RID: 22
	internal class LocalRunspaceHostFactory : PSHostFactory
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003F8B File Offset: 0x0000218B
		public override PSHost CreatePSHost()
		{
			return new RunspaceHost();
		}
	}
}
