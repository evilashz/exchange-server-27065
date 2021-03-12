using System;

namespace System.Collections
{
	// Token: 0x02000478 RID: 1144
	[__DynamicallyInvokable]
	public interface IStructuralEquatable
	{
		// Token: 0x060037DA RID: 14298
		[__DynamicallyInvokable]
		bool Equals(object other, IEqualityComparer comparer);

		// Token: 0x060037DB RID: 14299
		[__DynamicallyInvokable]
		int GetHashCode(IEqualityComparer comparer);
	}
}
