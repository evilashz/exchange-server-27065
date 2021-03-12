using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000235 RID: 565
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SessionStateCommandEntryWithMetadata
	{
		// Token: 0x06001417 RID: 5143 RVA: 0x000489D3 File Offset: 0x00046BD3
		internal SessionStateCommandEntryWithMetadata(SessionStateCommandEntry sessionStateCommandEntry, CommandMetadata commandMetadata)
		{
			this.SessionStateCommandEntry = sessionStateCommandEntry;
			this.CommandMetadata = commandMetadata;
		}

		// Token: 0x040005AA RID: 1450
		internal readonly SessionStateCommandEntry SessionStateCommandEntry;

		// Token: 0x040005AB RID: 1451
		internal readonly CommandMetadata CommandMetadata;
	}
}
