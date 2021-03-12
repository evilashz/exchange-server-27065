using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000971 RID: 2417
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IResourceMonitorFactory
	{
		// Token: 0x0600599B RID: 22939
		IResourceMonitor Create(Guid teamMailboxMdbGuid);
	}
}
