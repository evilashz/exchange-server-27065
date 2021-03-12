using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200001A RID: 26
	[Flags]
	public enum AccessMask
	{
		// Token: 0x04000072 RID: 114
		Open = 0,
		// Token: 0x04000073 RID: 115
		CreateChild = 1,
		// Token: 0x04000074 RID: 116
		DeleteChild = 2,
		// Token: 0x04000075 RID: 117
		List = 4,
		// Token: 0x04000076 RID: 118
		Self = 8,
		// Token: 0x04000077 RID: 119
		ReadProp = 16,
		// Token: 0x04000078 RID: 120
		WriteProp = 32,
		// Token: 0x04000079 RID: 121
		DeleteTree = 64,
		// Token: 0x0400007A RID: 122
		ListObject = 128,
		// Token: 0x0400007B RID: 123
		ControlAccess = 256,
		// Token: 0x0400007C RID: 124
		MaximumAllowed = 33554432,
		// Token: 0x0400007D RID: 125
		GenericRead = -2147483648
	}
}
