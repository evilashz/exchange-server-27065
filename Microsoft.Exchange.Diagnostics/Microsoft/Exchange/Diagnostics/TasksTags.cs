using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200026E RID: 622
	public struct TasksTags
	{
		// Token: 0x0400106B RID: 4203
		public const int Trace = 0;

		// Token: 0x0400106C RID: 4204
		public const int Log = 1;

		// Token: 0x0400106D RID: 4205
		public const int Error = 2;

		// Token: 0x0400106E RID: 4206
		public const int Event = 3;

		// Token: 0x0400106F RID: 4207
		public const int EnterExit = 4;

		// Token: 0x04001070 RID: 4208
		public const int FaultInjection = 5;

		// Token: 0x04001071 RID: 4209
		public static Guid guid = new Guid("1e254b9e-d663-4138-8183-e5e4b077f8d3");
	}
}
