using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F28 RID: 3880
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAuditLogRecord
	{
		// Token: 0x1700235B RID: 9051
		// (get) Token: 0x06008579 RID: 34169
		AuditLogRecordType RecordType { get; }

		// Token: 0x1700235C RID: 9052
		// (get) Token: 0x0600857A RID: 34170
		DateTime CreationTime { get; }

		// Token: 0x1700235D RID: 9053
		// (get) Token: 0x0600857B RID: 34171
		string Operation { get; }

		// Token: 0x1700235E RID: 9054
		// (get) Token: 0x0600857C RID: 34172
		string ObjectId { get; }

		// Token: 0x1700235F RID: 9055
		// (get) Token: 0x0600857D RID: 34173
		string UserId { get; }

		// Token: 0x0600857E RID: 34174
		IEnumerable<KeyValuePair<string, string>> GetDetails();
	}
}
