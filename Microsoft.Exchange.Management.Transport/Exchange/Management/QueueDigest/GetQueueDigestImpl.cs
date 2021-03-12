using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x02000069 RID: 105
	internal abstract class GetQueueDigestImpl
	{
		// Token: 0x060003BA RID: 954
		public abstract void ResolveForForest();

		// Token: 0x060003BB RID: 955
		public abstract void ResolveDag(DatabaseAvailabilityGroup dag);

		// Token: 0x060003BC RID: 956
		public abstract void ResolveAdSite(ADSite adSite);

		// Token: 0x060003BD RID: 957
		public abstract void ResolveServer(Server server);

		// Token: 0x060003BE RID: 958
		public abstract void ProcessRecord();
	}
}
