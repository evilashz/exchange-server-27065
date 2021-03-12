using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200018C RID: 396
	internal enum RunTextType : uint
	{
		// Token: 0x04001195 RID: 4501
		Unknown,
		// Token: 0x04001196 RID: 4502
		Space = 134217728U,
		// Token: 0x04001197 RID: 4503
		NewLine = 268435456U,
		// Token: 0x04001198 RID: 4504
		Tabulation = 402653184U,
		// Token: 0x04001199 RID: 4505
		UnusualWhitespace = 536870912U,
		// Token: 0x0400119A RID: 4506
		LastWhitespace = 536870912U,
		// Token: 0x0400119B RID: 4507
		Nbsp = 671088640U,
		// Token: 0x0400119C RID: 4508
		NonSpace = 805306368U,
		// Token: 0x0400119D RID: 4509
		LastText = 805306368U,
		// Token: 0x0400119E RID: 4510
		Last = 805306368U,
		// Token: 0x0400119F RID: 4511
		Mask = 939524096U
	}
}
