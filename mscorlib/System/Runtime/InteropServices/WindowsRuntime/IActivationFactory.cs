using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B5 RID: 2485
	[Guid("00000035-0000-0000-C000-000000000046")]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IActivationFactory
	{
		// Token: 0x06006365 RID: 25445
		[__DynamicallyInvokable]
		object ActivateInstance();
	}
}
