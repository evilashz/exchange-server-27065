using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x0200093F RID: 2367
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPublicFolderMailboxLoggerBase
	{
		// Token: 0x0600583A RID: 22586
		void LogEvent(LogEventType eventType, string data);
	}
}
