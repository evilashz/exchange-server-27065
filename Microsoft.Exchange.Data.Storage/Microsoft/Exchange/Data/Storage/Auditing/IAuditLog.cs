using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F3D RID: 3901
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAuditLog
	{
		// Token: 0x1700238D RID: 9101
		// (get) Token: 0x0600861B RID: 34331
		DateTime EstimatedLogStartTime { get; }

		// Token: 0x1700238E RID: 9102
		// (get) Token: 0x0600861C RID: 34332
		DateTime EstimatedLogEndTime { get; }

		// Token: 0x1700238F RID: 9103
		// (get) Token: 0x0600861D RID: 34333
		bool IsAsynchronous { get; }

		// Token: 0x0600861E RID: 34334
		IAuditQueryContext<TFilter> CreateAuditQueryContext<TFilter>();

		// Token: 0x0600861F RID: 34335
		int WriteAuditRecord(IAuditLogRecord auditRecord);
	}
}
