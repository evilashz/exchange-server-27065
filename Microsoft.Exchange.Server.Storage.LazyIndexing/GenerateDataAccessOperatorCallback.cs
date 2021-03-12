using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000014 RID: 20
	// (Invoke) Token: 0x06000090 RID: 144
	public delegate SimpleQueryOperator.SimpleQueryOperatorDefinition GenerateDataAccessOperatorCallback(Context context, LogicalIndex logicalIndex, IList<Column> columnsToFetch, out LogicalIndex baseViewIndex);
}
