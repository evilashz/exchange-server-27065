using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000283 RID: 643
	public struct ProtocolFilterTags
	{
		// Token: 0x0400113A RID: 4410
		public const int SenderFilterAgent = 0;

		// Token: 0x0400113B RID: 4411
		public const int RecipientFilterAgent = 1;

		// Token: 0x0400113C RID: 4412
		public const int Other = 2;

		// Token: 0x0400113D RID: 4413
		public static Guid guid = new Guid("0C5B72B3-290E-4c06-BE9D-D4727DF5FC0D");
	}
}
