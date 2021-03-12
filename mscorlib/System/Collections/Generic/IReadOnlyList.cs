using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020004AB RID: 1195
	[TypeDependency("System.SZArrayHelper")]
	[__DynamicallyInvokable]
	public interface IReadOnlyList<out T> : IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x170008D7 RID: 2263
		[__DynamicallyInvokable]
		T this[int index]
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}
