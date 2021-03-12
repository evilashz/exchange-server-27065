using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020004A3 RID: 1187
	[TypeDependency("System.SZArrayHelper")]
	[__DynamicallyInvokable]
	public interface ICollection<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060039A4 RID: 14756
		[__DynamicallyInvokable]
		int Count { [__DynamicallyInvokable] get; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060039A5 RID: 14757
		[__DynamicallyInvokable]
		bool IsReadOnly { [__DynamicallyInvokable] get; }

		// Token: 0x060039A6 RID: 14758
		[__DynamicallyInvokable]
		void Add(T item);

		// Token: 0x060039A7 RID: 14759
		[__DynamicallyInvokable]
		void Clear();

		// Token: 0x060039A8 RID: 14760
		[__DynamicallyInvokable]
		bool Contains(T item);

		// Token: 0x060039A9 RID: 14761
		[__DynamicallyInvokable]
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x060039AA RID: 14762
		[__DynamicallyInvokable]
		bool Remove(T item);
	}
}
