using System;

namespace System.Threading
{
	// Token: 0x020004BB RID: 1211
	internal interface IAsyncLocalValueMap
	{
		// Token: 0x06003A65 RID: 14949
		bool TryGetValue(IAsyncLocal key, out object value);

		// Token: 0x06003A66 RID: 14950
		IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent);
	}
}
