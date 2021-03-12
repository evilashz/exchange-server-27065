using System;

namespace System.Collections.Generic
{
	// Token: 0x020004A5 RID: 1189
	[__DynamicallyInvokable]
	public interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x170008D1 RID: 2257
		[__DynamicallyInvokable]
		TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060039AE RID: 14766
		[__DynamicallyInvokable]
		ICollection<TKey> Keys { [__DynamicallyInvokable] get; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060039AF RID: 14767
		[__DynamicallyInvokable]
		ICollection<TValue> Values { [__DynamicallyInvokable] get; }

		// Token: 0x060039B0 RID: 14768
		[__DynamicallyInvokable]
		bool ContainsKey(TKey key);

		// Token: 0x060039B1 RID: 14769
		[__DynamicallyInvokable]
		void Add(TKey key, TValue value);

		// Token: 0x060039B2 RID: 14770
		[__DynamicallyInvokable]
		bool Remove(TKey key);

		// Token: 0x060039B3 RID: 14771
		[__DynamicallyInvokable]
		bool TryGetValue(TKey key, out TValue value);
	}
}
