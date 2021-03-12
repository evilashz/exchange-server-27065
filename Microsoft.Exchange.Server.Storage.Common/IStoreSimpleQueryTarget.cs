using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000047 RID: 71
	public interface IStoreSimpleQueryTarget<T> : IStoreQueryTargetBase<T>
	{
		// Token: 0x0600049B RID: 1179
		IEnumerable<T> GetRows(object[] parameters);
	}
}
