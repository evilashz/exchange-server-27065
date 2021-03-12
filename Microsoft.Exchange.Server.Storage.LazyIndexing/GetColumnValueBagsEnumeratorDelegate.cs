using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000015 RID: 21
	// (Invoke) Token: 0x06000094 RID: 148
	public delegate IEnumerable<IColumnValueBag> GetColumnValueBagsEnumeratorDelegate(IContextProvider contextProvider, IEnumerable<Column> requiredColumns, IInterruptControl interruptControl, out LogicalIndex baseViewIndex);
}
