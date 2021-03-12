using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004ED RID: 1261
	internal static class ThreadPoolGlobals
	{
		// Token: 0x04001945 RID: 6469
		public static uint tpQuantum = 30U;

		// Token: 0x04001946 RID: 6470
		public static int processorCount = Environment.ProcessorCount;

		// Token: 0x04001947 RID: 6471
		public static bool tpHosted = ThreadPool.IsThreadPoolHosted();

		// Token: 0x04001948 RID: 6472
		public static volatile bool vmTpInitialized;

		// Token: 0x04001949 RID: 6473
		public static bool enableWorkerTracking;

		// Token: 0x0400194A RID: 6474
		[SecurityCritical]
		public static ThreadPoolWorkQueue workQueue = new ThreadPoolWorkQueue();
	}
}
