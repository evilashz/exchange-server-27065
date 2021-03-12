using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005ED RID: 1517
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum PropertyAttributes
	{
		// Token: 0x04001D48 RID: 7496
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001D49 RID: 7497
		[__DynamicallyInvokable]
		SpecialName = 512,
		// Token: 0x04001D4A RID: 7498
		ReservedMask = 62464,
		// Token: 0x04001D4B RID: 7499
		[__DynamicallyInvokable]
		RTSpecialName = 1024,
		// Token: 0x04001D4C RID: 7500
		[__DynamicallyInvokable]
		HasDefault = 4096,
		// Token: 0x04001D4D RID: 7501
		Reserved2 = 8192,
		// Token: 0x04001D4E RID: 7502
		Reserved3 = 16384,
		// Token: 0x04001D4F RID: 7503
		Reserved4 = 32768
	}
}
