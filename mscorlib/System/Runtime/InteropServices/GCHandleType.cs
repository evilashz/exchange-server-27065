using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091A RID: 2330
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum GCHandleType
	{
		// Token: 0x04002A86 RID: 10886
		[__DynamicallyInvokable]
		Weak,
		// Token: 0x04002A87 RID: 10887
		[__DynamicallyInvokable]
		WeakTrackResurrection,
		// Token: 0x04002A88 RID: 10888
		[__DynamicallyInvokable]
		Normal,
		// Token: 0x04002A89 RID: 10889
		[__DynamicallyInvokable]
		Pinned
	}
}
