using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000EF RID: 239
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IFormatProvider
	{
		// Token: 0x06000EFE RID: 3838
		[__DynamicallyInvokable]
		object GetFormat(Type formatType);
	}
}
