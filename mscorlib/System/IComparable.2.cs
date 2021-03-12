using System;

namespace System
{
	// Token: 0x02000059 RID: 89
	[__DynamicallyInvokable]
	public interface IComparable<in T>
	{
		// Token: 0x06000333 RID: 819
		[__DynamicallyInvokable]
		int CompareTo(T other);
	}
}
