using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A37 RID: 2615
	public class TestProviderResult<TProvider> where TProvider : IPListProvider, new()
	{
		// Token: 0x040034B4 RID: 13492
		public TProvider Provider;

		// Token: 0x040034B5 RID: 13493
		public IPAddress[] ProviderResult;

		// Token: 0x040034B6 RID: 13494
		public bool Matched;
	}
}
