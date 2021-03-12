using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C1 RID: 1473
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public struct InterfaceMapping
	{
		// Token: 0x04001C14 RID: 7188
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type TargetType;

		// Token: 0x04001C15 RID: 7189
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type InterfaceType;

		// Token: 0x04001C16 RID: 7190
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public MethodInfo[] TargetMethods;

		// Token: 0x04001C17 RID: 7191
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public MethodInfo[] InterfaceMethods;
	}
}
