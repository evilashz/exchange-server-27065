using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000473 RID: 1139
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IEqualityComparer
	{
		// Token: 0x0600379F RID: 14239
		[__DynamicallyInvokable]
		bool Equals(object x, object y);

		// Token: 0x060037A0 RID: 14240
		[__DynamicallyInvokable]
		int GetHashCode(object obj);
	}
}
