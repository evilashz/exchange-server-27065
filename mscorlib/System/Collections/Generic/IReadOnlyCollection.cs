using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020004AA RID: 1194
	[TypeDependency("System.SZArrayHelper")]
	[__DynamicallyInvokable]
	public interface IReadOnlyCollection<out T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060039BD RID: 14781
		[__DynamicallyInvokable]
		int Count { [__DynamicallyInvokable] get; }
	}
}
