using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorRunspaceProxy : IDisposable
	{
		// Token: 0x060000FF RID: 255
		Collection<T> RunPSCommand<T>(PSCommand command, out ErrorRecord error);

		// Token: 0x06000100 RID: 256
		T RunPSCommandSingleOrDefault<T>(PSCommand command, out ErrorRecord error);
	}
}
