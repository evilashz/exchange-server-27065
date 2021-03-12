using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005C7 RID: 1479
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ResourceLocation
	{
		// Token: 0x04001C21 RID: 7201
		[__DynamicallyInvokable]
		Embedded = 1,
		// Token: 0x04001C22 RID: 7202
		[__DynamicallyInvokable]
		ContainedInAnotherAssembly = 2,
		// Token: 0x04001C23 RID: 7203
		[__DynamicallyInvokable]
		ContainedInManifestFile = 4
	}
}
