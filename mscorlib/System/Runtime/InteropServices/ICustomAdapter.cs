using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000937 RID: 2359
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface ICustomAdapter
	{
		// Token: 0x06006109 RID: 24841
		[__DynamicallyInvokable]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetUnderlyingObject();
	}
}
