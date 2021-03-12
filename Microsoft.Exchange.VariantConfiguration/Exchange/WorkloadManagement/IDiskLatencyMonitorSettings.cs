using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000045 RID: 69
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IDiskLatencyMonitorSettings : ISettings
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000183 RID: 387
		TimeSpan FixedTimeAverageWindowBucket { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000184 RID: 388
		int FixedTimeAverageNumberOfBuckets { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000185 RID: 389
		TimeSpan ResourceHealthPollerInterval { get; }
	}
}
