using System;

namespace System
{
	// Token: 0x02000104 RID: 260
	[__DynamicallyInvokable]
	public interface IServiceProvider
	{
		// Token: 0x06000FCF RID: 4047
		[__DynamicallyInvokable]
		object GetService(Type serviceType);
	}
}
