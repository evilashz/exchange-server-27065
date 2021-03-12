using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x0200022D RID: 557
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UserSessionLoggerFactory : ILoggerFactory
	{
		// Token: 0x060014FE RID: 5374 RVA: 0x00078418 File Offset: 0x00076618
		public ILogger CreateLogger(string userIdentity, IDiagnosticsSession diagnosticsSession)
		{
			return new UserSessionLogger(userIdentity, diagnosticsSession);
		}
	}
}
