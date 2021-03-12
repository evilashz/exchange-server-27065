using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x02000092 RID: 146
	internal interface ICacheDirectorySession
	{
		// Token: 0x060007A8 RID: 1960
		void Insert(IConfigurable objectToSave, IEnumerable<PropertyDefinition> properties, List<Tuple<string, KeyType>> keys = null, int secondsTimeout = 2147483646, CacheItemPriority priority = CacheItemPriority.Default);
	}
}
