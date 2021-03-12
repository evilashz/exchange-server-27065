using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x0200046D RID: 1133
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface ICollection : IEnumerable
	{
		// Token: 0x06003788 RID: 14216
		[__DynamicallyInvokable]
		void CopyTo(Array array, int index);

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06003789 RID: 14217
		[__DynamicallyInvokable]
		int Count { [__DynamicallyInvokable] get; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600378A RID: 14218
		[__DynamicallyInvokable]
		object SyncRoot { [__DynamicallyInvokable] get; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600378B RID: 14219
		[__DynamicallyInvokable]
		bool IsSynchronized { [__DynamicallyInvokable] get; }
	}
}
