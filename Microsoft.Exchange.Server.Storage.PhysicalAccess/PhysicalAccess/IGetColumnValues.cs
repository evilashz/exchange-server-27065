using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200002F RID: 47
	internal interface IGetColumnValues
	{
		// Token: 0x0600026D RID: 621
		object[] GetColumnValues(IEnumerable<Column> columns);
	}
}
