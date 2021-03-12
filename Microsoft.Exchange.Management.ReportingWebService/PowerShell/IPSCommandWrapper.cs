using System;
using System.Management.Automation.Runspaces;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x0200000E RID: 14
	internal interface IPSCommandWrapper
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57
		CommandCollection Commands { get; }

		// Token: 0x0600003A RID: 58
		PowerShellResults Invoke(RunspaceMediator runspaceMediator);

		// Token: 0x0600003B RID: 59
		IPSCommandWrapper AddCommand(string commandText);

		// Token: 0x0600003C RID: 60
		IPSCommandWrapper AddCommand(Command command);

		// Token: 0x0600003D RID: 61
		IPSCommandWrapper AddParameter(string name);

		// Token: 0x0600003E RID: 62
		IPSCommandWrapper AddParameter(string name, object value);
	}
}
