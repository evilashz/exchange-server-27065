using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020004A9 RID: 1193
	[TypeDependency("System.SZArrayHelper")]
	[__DynamicallyInvokable]
	public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x170008D5 RID: 2261
		[__DynamicallyInvokable]
		T this[int index]
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		// Token: 0x060039BA RID: 14778
		[__DynamicallyInvokable]
		int IndexOf(T item);

		// Token: 0x060039BB RID: 14779
		[__DynamicallyInvokable]
		void Insert(int index, T item);

		// Token: 0x060039BC RID: 14780
		[__DynamicallyInvokable]
		void RemoveAt(int index);
	}
}
