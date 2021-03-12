using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200018B RID: 395
	internal enum RunType : uint
	{
		// Token: 0x0400118F RID: 4495
		Invalid,
		// Token: 0x04001190 RID: 4496
		Special = 1073741824U,
		// Token: 0x04001191 RID: 4497
		Normal = 2147483648U,
		// Token: 0x04001192 RID: 4498
		Literal = 3221225472U,
		// Token: 0x04001193 RID: 4499
		Mask = 3221225472U
	}
}
