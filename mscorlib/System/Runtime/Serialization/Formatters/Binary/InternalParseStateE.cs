using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000746 RID: 1862
	[Serializable]
	internal enum InternalParseStateE
	{
		// Token: 0x04002481 RID: 9345
		Initial,
		// Token: 0x04002482 RID: 9346
		Object,
		// Token: 0x04002483 RID: 9347
		Member,
		// Token: 0x04002484 RID: 9348
		MemberChild
	}
}
