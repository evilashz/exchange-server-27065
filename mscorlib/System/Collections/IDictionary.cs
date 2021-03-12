using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200046F RID: 1135
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IDictionary : ICollection, IEnumerable
	{
		// Token: 0x17000859 RID: 2137
		[__DynamicallyInvokable]
		object this[object key]
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600378F RID: 14223
		[__DynamicallyInvokable]
		ICollection Keys { [__DynamicallyInvokable] get; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06003790 RID: 14224
		[__DynamicallyInvokable]
		ICollection Values { [__DynamicallyInvokable] get; }

		// Token: 0x06003791 RID: 14225
		[__DynamicallyInvokable]
		bool Contains(object key);

		// Token: 0x06003792 RID: 14226
		[__DynamicallyInvokable]
		void Add(object key, object value);

		// Token: 0x06003793 RID: 14227
		[__DynamicallyInvokable]
		void Clear();

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06003794 RID: 14228
		[__DynamicallyInvokable]
		bool IsReadOnly { [__DynamicallyInvokable] get; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06003795 RID: 14229
		[__DynamicallyInvokable]
		bool IsFixedSize { [__DynamicallyInvokable] get; }

		// Token: 0x06003796 RID: 14230
		[__DynamicallyInvokable]
		IDictionaryEnumerator GetEnumerator();

		// Token: 0x06003797 RID: 14231
		[__DynamicallyInvokable]
		void Remove(object key);
	}
}
