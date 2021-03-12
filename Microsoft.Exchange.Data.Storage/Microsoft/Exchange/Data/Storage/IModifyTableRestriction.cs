using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007AC RID: 1964
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IModifyTableRestriction
	{
		// Token: 0x060049F2 RID: 18930
		void Enforce(IModifyTable modifyTable, IEnumerable<ModifyTableOperation> changingEntries);
	}
}
