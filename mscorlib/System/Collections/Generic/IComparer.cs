using System;

namespace System.Collections.Generic
{
	// Token: 0x020004A4 RID: 1188
	[__DynamicallyInvokable]
	public interface IComparer<in T>
	{
		// Token: 0x060039AB RID: 14763
		[__DynamicallyInvokable]
		int Compare(T x, T y);
	}
}
