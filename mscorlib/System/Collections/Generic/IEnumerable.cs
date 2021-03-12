using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020004A6 RID: 1190
	[TypeDependency("System.SZArrayHelper")]
	[__DynamicallyInvokable]
	public interface IEnumerable<out T> : IEnumerable
	{
		// Token: 0x060039B4 RID: 14772
		[__DynamicallyInvokable]
		IEnumerator<T> GetEnumerator();
	}
}
