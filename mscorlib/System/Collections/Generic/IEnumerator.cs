using System;

namespace System.Collections.Generic
{
	// Token: 0x020004A7 RID: 1191
	[__DynamicallyInvokable]
	public interface IEnumerator<out T> : IDisposable, IEnumerator
	{
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060039B5 RID: 14773
		[__DynamicallyInvokable]
		T Current { [__DynamicallyInvokable] get; }
	}
}
