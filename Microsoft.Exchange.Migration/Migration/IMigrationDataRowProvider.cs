using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationDataRowProvider
	{
		// Token: 0x0600001E RID: 30
		IEnumerable<IMigrationDataRow> GetNextBatchItem(string cursorPosition, int maxCountHint);
	}
}
