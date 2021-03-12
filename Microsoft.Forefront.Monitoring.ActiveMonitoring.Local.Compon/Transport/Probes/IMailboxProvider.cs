using System;
using System.Collections.Generic;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x0200026F RID: 623
	internal interface IMailboxProvider
	{
		// Token: 0x06001488 RID: 5256
		MailboxSelectionResult TryGetMailboxToUse(out Guid mbxGuid, out Guid mdbGuid, out string emailAddress);

		// Token: 0x06001489 RID: 5257
		MailboxDatabaseSelectionResult GetAllMailboxDatabaseInfo(out ICollection<MailboxDatabaseInfo> mailboxDatabases);
	}
}
