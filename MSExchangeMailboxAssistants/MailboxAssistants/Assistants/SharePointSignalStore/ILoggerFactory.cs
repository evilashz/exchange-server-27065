using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000223 RID: 547
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ILoggerFactory
	{
		// Token: 0x060014B9 RID: 5305
		ILogger CreateLogger(string identity, IDiagnosticsSession diagnosticsSession);
	}
}
