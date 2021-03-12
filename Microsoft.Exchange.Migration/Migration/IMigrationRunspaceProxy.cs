using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationRunspaceProxy : IDisposable
	{
		// Token: 0x06000024 RID: 36
		T RunPSCommand<T>(PSCommand command, out ErrorRecord error);
	}
}
