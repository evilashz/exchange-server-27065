using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005B4 RID: 1460
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum EventAttributes
	{
		// Token: 0x04001BDA RID: 7130
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001BDB RID: 7131
		[__DynamicallyInvokable]
		SpecialName = 512,
		// Token: 0x04001BDC RID: 7132
		ReservedMask = 1024,
		// Token: 0x04001BDD RID: 7133
		[__DynamicallyInvokable]
		RTSpecialName = 1024
	}
}
