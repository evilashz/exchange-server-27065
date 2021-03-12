using System;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02000010 RID: 16
	internal static class TraceIDs
	{
		// Token: 0x04000053 RID: 83
		public const int ServiceLetStarted = 42415;

		// Token: 0x04000054 RID: 84
		public const int ServiceletStopped = 28310;

		// Token: 0x04000055 RID: 85
		public const int SpawningThreads = 19568;

		// Token: 0x04000056 RID: 86
		public const int WaitingForThreads = 28049;

		// Token: 0x04000057 RID: 87
		public const int TenantArbitrationMailboxFound = 25566;

		// Token: 0x04000058 RID: 88
		public const int NoTenantArbitrationMailboxFound = 36433;

		// Token: 0x04000059 RID: 89
		public const int AddingTenantToWorkerProcess = 28720;

		// Token: 0x0400005A RID: 90
		public const int StartingWorkerThread = 94668;

		// Token: 0x0400005B RID: 91
		public const int StartProcessingTenant = 54361;

		// Token: 0x0400005C RID: 92
		public const int StartProcessingRequest = 94597;

		// Token: 0x0400005D RID: 93
		public const int ServiceShuttingDown = 29459;

		// Token: 0x0400005E RID: 94
		public const int StartCollectingData = 28705;

		// Token: 0x0400005F RID: 95
		public const int StartSendingEmail = 47332;

		// Token: 0x04000060 RID: 96
		public const int ServiceletException = 21863;

		// Token: 0x04000061 RID: 97
		public const int WorkerException = 11881;

		// Token: 0x04000062 RID: 98
		public const int KnownWorkerException = 96633;

		// Token: 0x04000063 RID: 99
		public const int SearchItemNotFound = 20575;

		// Token: 0x04000064 RID: 100
		public const int NonMailboxRole = 83371;

		// Token: 0x04000065 RID: 101
		public const int FindLocalServerFailed = 67552;

		// Token: 0x04000066 RID: 102
		public const int PollIntervalRead = 13340;

		// Token: 0x04000067 RID: 103
		public const int ConfigurationError = 17790;
	}
}
