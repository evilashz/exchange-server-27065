using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F39 RID: 3897
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAuditingOpticsLoggerInstance
	{
		// Token: 0x060085EC RID: 34284
		void InternalLogRow(List<KeyValuePair<string, object>> customData);
	}
}
