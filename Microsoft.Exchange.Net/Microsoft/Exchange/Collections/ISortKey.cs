using System;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000033 RID: 51
	public interface ISortKey<TKey> where TKey : IComparable<TKey>
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600013C RID: 316
		TKey SortKey { get; }
	}
}
