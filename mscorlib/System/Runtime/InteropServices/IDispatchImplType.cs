using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008F2 RID: 2290
	[Obsolete("The IDispatchImplAttribute is deprecated.", false)]
	[ComVisible(true)]
	[Serializable]
	public enum IDispatchImplType
	{
		// Token: 0x040029BC RID: 10684
		SystemDefinedImpl,
		// Token: 0x040029BD RID: 10685
		InternalImpl,
		// Token: 0x040029BE RID: 10686
		CompatibleImpl
	}
}
