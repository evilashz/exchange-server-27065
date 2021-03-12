using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000092 RID: 146
	public interface IStoreDatabaseQueryTarget<T> : IStoreQueryTargetBase<T>
	{
		// Token: 0x06000647 RID: 1607
		IEnumerable<T> GetRows(IConnectionProvider connectionProvider, object[] parameters);
	}
}
