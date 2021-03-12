using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200008B RID: 139
	public interface IBloomFilterDataProvider
	{
		// Token: 0x06000505 RID: 1285
		bool Check<T>(QueryFilter queryFilter);
	}
}
