using System;

namespace System.Reflection
{
	// Token: 0x020005CC RID: 1484
	[Flags]
	[Serializable]
	internal enum MethodSemanticsAttributes
	{
		// Token: 0x04001C71 RID: 7281
		Setter = 1,
		// Token: 0x04001C72 RID: 7282
		Getter = 2,
		// Token: 0x04001C73 RID: 7283
		Other = 4,
		// Token: 0x04001C74 RID: 7284
		AddOn = 8,
		// Token: 0x04001C75 RID: 7285
		RemoveOn = 16,
		// Token: 0x04001C76 RID: 7286
		Fire = 32
	}
}
