using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000068 RID: 104
	internal interface IValueTupleInternal : ITuple
	{
		// Token: 0x060003DC RID: 988
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x060003DD RID: 989
		string ToStringEnd();
	}
}
