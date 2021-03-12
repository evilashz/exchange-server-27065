using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000213 RID: 531
	public struct MSExchangeSyncTags
	{
		// Token: 0x04000B75 RID: 2933
		public const int Requests = 0;

		// Token: 0x04000B76 RID: 2934
		public const int Wbxml = 1;

		// Token: 0x04000B77 RID: 2935
		public const int Xso = 2;

		// Token: 0x04000B78 RID: 2936
		public const int Algorithm = 3;

		// Token: 0x04000B79 RID: 2937
		public const int Protocol = 4;

		// Token: 0x04000B7A RID: 2938
		public const int Conversion = 5;

		// Token: 0x04000B7B RID: 2939
		public const int ThreadPool = 6;

		// Token: 0x04000B7C RID: 2940
		public const int RawBodyBytes = 7;

		// Token: 0x04000B7D RID: 2941
		public const int MethodEnterExit = 8;

		// Token: 0x04000B7E RID: 2942
		public const int TiUpgrade = 9;

		// Token: 0x04000B7F RID: 2943
		public const int Validation = 10;

		// Token: 0x04000B80 RID: 2944
		public const int PfdInitTrace = 11;

		// Token: 0x04000B81 RID: 2945
		public const int CorruptItem = 12;

		// Token: 0x04000B82 RID: 2946
		public const int Threading = 13;

		// Token: 0x04000B83 RID: 2947
		public const int FaultInjection = 14;

		// Token: 0x04000B84 RID: 2948
		public const int Body = 15;

		// Token: 0x04000B85 RID: 2949
		public const int Diagnostics = 16;

		// Token: 0x04000B86 RID: 2950
		public static Guid guid = new Guid("5e88fb2c-0a36-41f2-a710-c911bfe18e44");
	}
}
