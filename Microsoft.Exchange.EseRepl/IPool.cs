using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000014 RID: 20
	internal interface IPool<T> where T : class, IPoolableObject
	{
		// Token: 0x06000098 RID: 152
		bool TryReturnObject(T o);

		// Token: 0x06000099 RID: 153
		T TryGetObject();
	}
}
