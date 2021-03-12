using System;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x0200000C RID: 12
	internal interface IPSCommandResolver
	{
		// Token: 0x06000031 RID: 49
		ReadOnlyCollection<PSTypeName> GetOutputType(string commandName);
	}
}
