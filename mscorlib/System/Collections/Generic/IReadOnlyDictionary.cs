using System;

namespace System.Collections.Generic
{
	// Token: 0x020004AC RID: 1196
	[__DynamicallyInvokable]
	public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x060039BF RID: 14783
		[__DynamicallyInvokable]
		bool ContainsKey(TKey key);

		// Token: 0x060039C0 RID: 14784
		[__DynamicallyInvokable]
		bool TryGetValue(TKey key, out TValue value);

		// Token: 0x170008D8 RID: 2264
		[__DynamicallyInvokable]
		TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060039C2 RID: 14786
		[__DynamicallyInvokable]
		IEnumerable<TKey> Keys { [__DynamicallyInvokable] get; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060039C3 RID: 14787
		[__DynamicallyInvokable]
		IEnumerable<TValue> Values { [__DynamicallyInvokable] get; }
	}
}
