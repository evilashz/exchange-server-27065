using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008E6 RID: 2278
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ComInterfaceType
	{
		// Token: 0x040029AB RID: 10667
		[__DynamicallyInvokable]
		InterfaceIsDual,
		// Token: 0x040029AC RID: 10668
		[__DynamicallyInvokable]
		InterfaceIsIUnknown,
		// Token: 0x040029AD RID: 10669
		[__DynamicallyInvokable]
		InterfaceIsIDispatch,
		// Token: 0x040029AE RID: 10670
		[ComVisible(false)]
		[__DynamicallyInvokable]
		InterfaceIsIInspectable
	}
}
