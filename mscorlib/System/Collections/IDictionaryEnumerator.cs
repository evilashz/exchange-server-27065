using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x02000470 RID: 1136
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IDictionaryEnumerator : IEnumerator
	{
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06003798 RID: 14232
		[__DynamicallyInvokable]
		object Key { [__DynamicallyInvokable] get; }

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06003799 RID: 14233
		[__DynamicallyInvokable]
		object Value { [__DynamicallyInvokable] get; }

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600379A RID: 14234
		[__DynamicallyInvokable]
		DictionaryEntry Entry { [__DynamicallyInvokable] get; }
	}
}
