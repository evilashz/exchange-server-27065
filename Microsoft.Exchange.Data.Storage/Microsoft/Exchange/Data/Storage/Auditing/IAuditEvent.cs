using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F26 RID: 3878
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAuditEvent
	{
		// Token: 0x1700234A RID: 9034
		// (get) Token: 0x06008554 RID: 34132
		Guid RecordId { get; }

		// Token: 0x1700234B RID: 9035
		// (get) Token: 0x06008555 RID: 34133
		string OrganizationId { get; }

		// Token: 0x1700234C RID: 9036
		// (get) Token: 0x06008556 RID: 34134
		Guid MailboxGuid { get; }

		// Token: 0x1700234D RID: 9037
		// (get) Token: 0x06008557 RID: 34135
		string OperationName { get; }

		// Token: 0x1700234E RID: 9038
		// (get) Token: 0x06008558 RID: 34136
		string LogonTypeName { get; }

		// Token: 0x1700234F RID: 9039
		// (get) Token: 0x06008559 RID: 34137
		OperationResult OperationSucceeded { get; }

		// Token: 0x17002350 RID: 9040
		// (get) Token: 0x0600855A RID: 34138
		bool ExternalAccess { get; }

		// Token: 0x0600855B RID: 34139
		IAuditLogRecord GetLogRecord();
	}
}
