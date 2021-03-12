using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000284 RID: 644
	public struct SenderIdTags
	{
		// Token: 0x0400113E RID: 4414
		public const int Validation = 0;

		// Token: 0x0400113F RID: 4415
		public const int Parsing = 1;

		// Token: 0x04001140 RID: 4416
		public const int MacroExpansion = 2;

		// Token: 0x04001141 RID: 4417
		public const int Agent = 3;

		// Token: 0x04001142 RID: 4418
		public const int Other = 4;

		// Token: 0x04001143 RID: 4419
		public static Guid guid = new Guid("AA6A0F4B-6EC1-472d-84BA-FDCB84F20449");
	}
}
