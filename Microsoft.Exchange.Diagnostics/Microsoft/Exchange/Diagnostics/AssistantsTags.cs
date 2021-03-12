using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000240 RID: 576
	public struct AssistantsTags
	{
		// Token: 0x04000F08 RID: 3848
		public const int AssistantsRpcServer = 0;

		// Token: 0x04000F09 RID: 3849
		public const int DatabaseInfo = 1;

		// Token: 0x04000F0A RID: 3850
		public const int DatabaseManager = 2;

		// Token: 0x04000F0B RID: 3851
		public const int ErrorHandler = 3;

		// Token: 0x04000F0C RID: 3852
		public const int EventAccess = 4;

		// Token: 0x04000F0D RID: 3853
		public const int EventController = 5;

		// Token: 0x04000F0E RID: 3854
		public const int EventDispatcher = 6;

		// Token: 0x04000F0F RID: 3855
		public const int EventBasedAssistantCollection = 7;

		// Token: 0x04000F10 RID: 3856
		public const int TimeBasedAssistantController = 8;

		// Token: 0x04000F11 RID: 3857
		public const int TimeBasedDatabaseDriver = 9;

		// Token: 0x04000F12 RID: 3858
		public const int TimeBasedDatabaseJob = 10;

		// Token: 0x04000F13 RID: 3859
		public const int TimeBasedDatabaseWindowJob = 11;

		// Token: 0x04000F14 RID: 3860
		public const int TimeBasedDatabaseDemandJob = 12;

		// Token: 0x04000F15 RID: 3861
		public const int TimeBasedDriverManager = 13;

		// Token: 0x04000F16 RID: 3862
		public const int OnlineDatabase = 14;

		// Token: 0x04000F17 RID: 3863
		public const int PoisonControl = 15;

		// Token: 0x04000F18 RID: 3864
		public const int Throttle = 16;

		// Token: 0x04000F19 RID: 3865
		public const int PFD = 17;

		// Token: 0x04000F1A RID: 3866
		public const int Governor = 18;

		// Token: 0x04000F1B RID: 3867
		public const int QueueProcessor = 19;

		// Token: 0x04000F1C RID: 3868
		public const int FaultInjection = 20;

		// Token: 0x04000F1D RID: 3869
		public const int ProbeTimeBasedAssistant = 21;

		// Token: 0x04000F1E RID: 3870
		public static Guid guid = new Guid("EDC33045-05FB-4abb-A608-AEE572BC3C5F");
	}
}
