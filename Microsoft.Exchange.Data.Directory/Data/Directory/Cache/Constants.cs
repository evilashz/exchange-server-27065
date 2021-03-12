using System;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x0200009F RID: 159
	internal static class Constants
	{
		// Token: 0x040002E1 RID: 737
		internal const string DirectoryCacheEnableKey = "Directory Cache Enable";

		// Token: 0x040002E2 RID: 738
		internal const string ExcludedDirectoryCacheProcessesKey = "Excluded Directory Cache Processes";

		// Token: 0x040002E3 RID: 739
		internal const int CheckCacheKeyEveryNMilliseconds = 300000;

		// Token: 0x040002E4 RID: 740
		internal const int CheckCacheKeyEveryNMillisecondsInTest = 15000;

		// Token: 0x040002E5 RID: 741
		internal const int DefaultObjectTypeTimeout = 2147483646;

		// Token: 0x040002E6 RID: 742
		internal const int VolatileObjectsCacheTimeInNSeconds = 30;

		// Token: 0x040002E7 RID: 743
		internal const int DefaultNewObjectCacheTimeoutInMinutes = 15;

		// Token: 0x040002E8 RID: 744
		internal const int DefaultNewObjectInclusionThresholdInMinutes = 15;

		// Token: 0x040002E9 RID: 745
		internal const string DirectoryCacheNamedPipeURI = "net.pipe://localhost/DirectoryCache/service.svc";

		// Token: 0x040002EA RID: 746
		internal const int WCFTimeoutInSeconds = 15;

		// Token: 0x040002EB RID: 747
		internal const int MaxMessageRecievedSize = 10485760;

		// Token: 0x040002EC RID: 748
		internal const int MaxBufferSize = 10485760;
	}
}
