using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000474 RID: 1140
	[Obsolete("Please use IEqualityComparer instead.")]
	[ComVisible(true)]
	public interface IHashCodeProvider
	{
		// Token: 0x060037A1 RID: 14241
		int GetHashCode(object obj);
	}
}
