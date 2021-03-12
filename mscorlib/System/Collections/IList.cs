using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000475 RID: 1141
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IList : ICollection, IEnumerable
	{
		// Token: 0x17000862 RID: 2146
		[__DynamicallyInvokable]
		object this[int index]
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		// Token: 0x060037A4 RID: 14244
		[__DynamicallyInvokable]
		int Add(object value);

		// Token: 0x060037A5 RID: 14245
		[__DynamicallyInvokable]
		bool Contains(object value);

		// Token: 0x060037A6 RID: 14246
		[__DynamicallyInvokable]
		void Clear();

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060037A7 RID: 14247
		[__DynamicallyInvokable]
		bool IsReadOnly { [__DynamicallyInvokable] get; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060037A8 RID: 14248
		[__DynamicallyInvokable]
		bool IsFixedSize { [__DynamicallyInvokable] get; }

		// Token: 0x060037A9 RID: 14249
		[__DynamicallyInvokable]
		int IndexOf(object value);

		// Token: 0x060037AA RID: 14250
		[__DynamicallyInvokable]
		void Insert(int index, object value);

		// Token: 0x060037AB RID: 14251
		[__DynamicallyInvokable]
		void Remove(object value);

		// Token: 0x060037AC RID: 14252
		[__DynamicallyInvokable]
		void RemoveAt(int index);
	}
}
