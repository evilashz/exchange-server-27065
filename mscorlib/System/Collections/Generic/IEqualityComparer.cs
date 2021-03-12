using System;

namespace System.Collections.Generic
{
	// Token: 0x020004A8 RID: 1192
	[__DynamicallyInvokable]
	public interface IEqualityComparer<in T>
	{
		// Token: 0x060039B6 RID: 14774
		[__DynamicallyInvokable]
		bool Equals(T x, T y);

		// Token: 0x060039B7 RID: 14775
		[__DynamicallyInvokable]
		int GetHashCode(T obj);
	}
}
