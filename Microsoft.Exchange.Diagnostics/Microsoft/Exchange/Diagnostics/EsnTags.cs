using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200030F RID: 783
	public struct EsnTags
	{
		// Token: 0x040014E5 RID: 5349
		public const int General = 0;

		// Token: 0x040014E6 RID: 5350
		public const int Data = 1;

		// Token: 0x040014E7 RID: 5351
		public const int PreProcessor = 2;

		// Token: 0x040014E8 RID: 5352
		public const int Composer = 3;

		// Token: 0x040014E9 RID: 5353
		public const int PostProcessor = 4;

		// Token: 0x040014EA RID: 5354
		public const int MailSender = 5;

		// Token: 0x040014EB RID: 5355
		public const int FaultInjection = 6;

		// Token: 0x040014EC RID: 5356
		public static Guid guid = new Guid("A0D123B0-CF78-4BCA-AAC9-F892D98199F4");
	}
}
