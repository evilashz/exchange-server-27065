using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200008B RID: 139
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CloudArchiveProcessor : RemoteArchiveProcessorBase
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x00028B46 File Offset: 0x00026D46
		public CloudArchiveProcessor(MailboxSession mailboxSession, ADUser user, StatisticsLogEntry statisticsLogEntry, bool isTestMode) : base(mailboxSession, user, statisticsLogEntry, true, isTestMode)
		{
		}
	}
}
