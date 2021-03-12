using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x0200000D RID: 13
	internal interface IRollingCountConfig : ICountedConfig
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000040 RID: 64
		TimeSpan WindowInterval { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000041 RID: 65
		TimeSpan WindowBucketSize { get; }
	}
}
