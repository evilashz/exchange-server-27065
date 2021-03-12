using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D6 RID: 1494
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MemberTypes
	{
		// Token: 0x04001CA3 RID: 7331
		Constructor = 1,
		// Token: 0x04001CA4 RID: 7332
		Event = 2,
		// Token: 0x04001CA5 RID: 7333
		Field = 4,
		// Token: 0x04001CA6 RID: 7334
		Method = 8,
		// Token: 0x04001CA7 RID: 7335
		Property = 16,
		// Token: 0x04001CA8 RID: 7336
		TypeInfo = 32,
		// Token: 0x04001CA9 RID: 7337
		Custom = 64,
		// Token: 0x04001CAA RID: 7338
		NestedType = 128,
		// Token: 0x04001CAB RID: 7339
		All = 191
	}
}
