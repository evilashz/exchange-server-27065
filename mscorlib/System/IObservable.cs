using System;

namespace System
{
	// Token: 0x020000F2 RID: 242
	[__DynamicallyInvokable]
	public interface IObservable<out T>
	{
		// Token: 0x06000F04 RID: 3844
		[__DynamicallyInvokable]
		IDisposable Subscribe(IObserver<T> observer);
	}
}
