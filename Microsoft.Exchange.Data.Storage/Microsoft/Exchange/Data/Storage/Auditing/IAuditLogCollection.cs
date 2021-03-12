using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F43 RID: 3907
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAuditLogCollection
	{
		// Token: 0x0600863F RID: 34367
		IEnumerable<IAuditLog> GetAuditLogs();

		// Token: 0x06008640 RID: 34368
		bool FindLog(DateTime timestamp, bool createIfNotExists, out IAuditLog auditLog);
	}
}
