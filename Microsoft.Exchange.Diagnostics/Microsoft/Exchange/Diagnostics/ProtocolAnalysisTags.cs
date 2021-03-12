using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200028A RID: 650
	public struct ProtocolAnalysisTags
	{
		// Token: 0x04001158 RID: 4440
		public const int Factory = 0;

		// Token: 0x04001159 RID: 4441
		public const int Database = 1;

		// Token: 0x0400115A RID: 4442
		public const int CalculateSrl = 2;

		// Token: 0x0400115B RID: 4443
		public const int OnMailFrom = 3;

		// Token: 0x0400115C RID: 4444
		public const int OnRcptTo = 4;

		// Token: 0x0400115D RID: 4445
		public const int OnEOD = 5;

		// Token: 0x0400115E RID: 4446
		public const int Reject = 6;

		// Token: 0x0400115F RID: 4447
		public const int Disconnect = 7;

		// Token: 0x04001160 RID: 4448
		public static Guid guid = new Guid("A0F3DC2A-7FD4-491E-C176-4857EAF2D7EF");
	}
}
